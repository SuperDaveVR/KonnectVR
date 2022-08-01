/*
 * @author Marty Fitzer
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KonnectVR.Interactions.Connectables
{
    [System.Serializable]
    public class ConnectableEvent : UnityEvent<Connectable> { }

    [System.Serializable]
    public class Connection
    {
        [SerializeField]
        private Connectable otherConnectable;
        public Connectable OtherConnectable
        {
            get => otherConnectable;
        }

        /// <summary>
        /// Connector: Connects the joint.
        /// Receiver: Is connected to the joint.
        /// </summary>
        public enum ConnectionType { Connector, Receiver }

        [SerializeField, Tooltip("Determines which connectable connects the joint. Connector: This connectable connects the joint. Receiver: Other connectable connects the joint.")]
        private ConnectionType connectionType = ConnectionType.Connector;
        public ConnectionType Type
        {
            get => connectionType;
            internal set => connectionType = value;
        }

        [SerializeField, Tooltip("Only use this if connection type is Receiver.")]
        private Joint connectionJoint;
        public Joint ConnectionJoint
        {
            get => connectionJoint;
            internal set => connectionJoint = value;
        }

        internal bool enabled { get; set; } = true;

        internal bool connected { get; set; } = false;

        [SerializeField]
        private UnityEvent onConnected;
        public UnityEvent OnConnected
        {
            get => onConnected;
        }

        [SerializeField]
        private UnityEvent onDisconnected;
        public UnityEvent OnDisconnected
        {
            get => onDisconnected;
        }
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(MeshRenderer))]
    public class Connectable : MonoBehaviour
    {
        [SerializeField, Tooltip("Specifies which connector are allowed to connect and which joint will be used for the connection.")]
        private List<Connection> allowedConnections;

        private uint activeConnections = 0;

        [SerializeField, Tooltip("Determines how long the connectables will be forcibly stabilized after a connection is made.")]
        private float jointStabilizationPeriod = 0.1f;

        [SerializeField, Tooltip("Determines how long the forces acting on the rigidbody limited after a connection is made.")]
        private float forceStabilizationPeriod = 1f;

        internal bool shouldStabilizeConnection { get; set; } = true;

        [SerializeField, Tooltip("How long in seconds after disconnecting from a connectable before it can reconnect.")]
        private float reconnectDelay = 1f;

        internal ConnectableParent connectableParent { get; set; }

        internal Rigidbody rb { get; private set; }

        /// <summary>
        /// Rigidbody used as a container for joints that aren't currently being used when it is not connected to another connectable.
        /// </summary>
        private Rigidbody disabledJointContainer;

        /// <summary>
        /// Fixed joint that is created between the connectable child and parent during a connection to make the parent follow the child.
        /// </summary>
        private FixedJoint parentConnectionJoint;

        /// <summary>
        /// Initial local position of the connectable.
        /// </summary>
        private Vector3 initLocalPos;

        /// <summary>
        /// Initial local rotation of the connectable.
        /// </summary>
        private Quaternion initLocalRot;

        private MeshRenderer rend;

        private const string colorPropName = "_RimColor";
        private Color defaultColor;

        [Header("Feedback")]

        [SerializeField]
        private Color connectionSuccessColor = Color.green;

        [SerializeField]
        private Color connectionFailureColor = Color.red;

        [SerializeField, Tooltip("The duration in seconds that the connection failure color will be active following a failed connection.")]
        private float failureFeedbackDuration = 3f;

        private uint failedConnections = 0;

        /// <summary>
        /// Fired when this connectable connects to another connectable as the connector.
        /// Param1: Connectable
        /// Param2: Connection
        /// </summary>
        internal UnityAction<Connectable, Connection> OnConnected;

        /// <summary>
        /// Fired when the connection physics have been stabilized.
        /// </summary>
        internal UnityAction OnJointStabilizationComplete;

        /// <summary>
        /// Fired when this connectable disconnects from another connectable as the connector.
        /// Param1: Connectable
        /// Param2: Connection
        /// </summary>
        internal UnityAction<Connectable, Connection> OnDisconnected;

        [SerializeField]
        private ConnectableEvent onConnectionFailed;
        public ConnectableEvent OnConnectionFailed
        {
            get => onConnectionFailed;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = true; //Forces connectable to follow connectable parent's rigidbody movement

            disabledJointContainer = new GameObject("Disabled Joint Container").AddComponent<Rigidbody>();
            disabledJointContainer.transform.parent = transform;
            disabledJointContainer.transform.localPosition = Vector3.zero;
            disabledJointContainer.transform.localRotation = Quaternion.identity;
            disabledJointContainer.isKinematic = true;
            disabledJointContainer.gameObject.SetActive(false);

            initializeConnections();

            initLocalPos = transform.localPosition;
            initLocalRot = transform.localRotation;

            rend = GetComponent<MeshRenderer>();
            defaultColor = rend.material.GetColor(colorPropName);
        }

        private void initializeConnections()
        {
            List<Connection> invalidConnections = new List<Connection>();

            foreach (Connection connection in allowedConnections)
            {
                //Make sure the connection is allowed on the other connectable too
                if (connection.OtherConnectable.isAllowed(this, out Connection otherConnection))
                {
                    if (connection.Type != otherConnection.Type) //There is one Connector and one Receiver
                    {
                        if (connection.Type == Connection.ConnectionType.Connector)
                        {
                            if (connection.ConnectionJoint) //Joint is set, configure for later connection
                            {
                                connection.ConnectionJoint.enablePreprocessing = false;  //Makes joint more stable
                                connection.ConnectionJoint.autoConfigureConnectedAnchor = false; //Prevents anchor from being manipulated

                                //Disables the connection joint until a connection is made
                                disableConnectionJoint(connection);
                            }
                            else //Connection joint not set on the connector, invalid configuration
                            {
                                invalidConnections.Add(connection);

                                Debug.LogError("Invalid connection configuration. The connector in the connection between " + connectableParent.name + " and " + connection.OtherConnectable.connectableParent.name + " does not have a configured connection joint.");
                            }
                        }
                        else //Connection.ConnectionType.Receiver
                        {
                            if (connection.ConnectionJoint)
                            {
                                //Should not have a joint, destroy it
                                Destroy(connection.ConnectionJoint);
                            }
                        }
                    }
                    else //There is either two Connectors or two Receivers, this is not allowed
                    {
                        if (connection.ConnectionJoint)
                            Destroy(connection.ConnectionJoint);

                        invalidConnections.Add(connection);

                        Debug.LogError("Invalid connection configuration. Both connectables in the connection between " + connectableParent.name + " and " + connection.OtherConnectable.connectableParent.name + " are set to the same Connection Type, this is not allowed.");
                    }
                }
                else //Invalid connection, connection not allowed on both connectables
                {
                    if (connection.ConnectionJoint)
                        Destroy(connection.ConnectionJoint);

                    invalidConnections.Add(connection);

                    Debug.LogError("Invalid connection configuration. The connection between " + connectableParent.name + " and " + connection.OtherConnectable.connectableParent.name + " is not allowed by both connectables.");
                }
            }

            //Remove all invalid connections from allowed connections
            foreach (Connection invalidConnection in invalidConnections)
            {

                allowedConnections.Remove(invalidConnection);
            }
        }

        #region Connection

        private void OnTriggerEnter(Collider other)
        {
            Connectable otherConnector = other.GetComponent<Connectable>();
            if (otherConnector) //Collider is a connector
            {
                connect(otherConnector);
            }
        }

        public void connect(Connectable otherConnectable)
        {
            if (isAllowed(otherConnectable, out Connection connection))
            {
                //Not already connected and enabled
                if (!connection.connected && connection.enabled)
                {
                    //This is the connector, must create joint
                    if (connection.Type == Connection.ConnectionType.Connector)
                    {
                        //Determine rotation needed to snap the receiver into the correct rotation
                        Quaternion snapRotation = transform.rotation * Quaternion.Inverse(otherConnectable.transform.rotation);
                        otherConnectable.connectableParent.transform.rotation = snapRotation * otherConnectable.connectableParent.transform.rotation;

                        //Determine how far to translate to snap the receiver into the correct position
                        Vector3 snapTranslation = transform.position - otherConnectable.transform.position;
                        otherConnectable.connectableParent.transform.position = otherConnectable.connectableParent.transform.position + snapTranslation;

                        //Both connectables are being grabbed
                        if (otherConnectable.connectableParent.grabInteractable.grabbed && connectableParent.grabInteractable.grabbed)
                        {
                            //Stop the interaction on receiver of the connectable grab interactables to avoid breaking the physics joints
                            otherConnectable.connectableParent.grabInteractable.forceStopInteraction(otherConnectable.connectableParent.grabInteractable.interactingInteractor);
                        }

                        //Make rigidbodies non kinematic to allow joint to work properly
                        rb.isKinematic = false;
                        otherConnectable.rb.isKinematic = false;

                        stabilizeConnection();
                        otherConnectable.stabilizeConnection();

                        //Set connected body
                        enableConnectionJoint(connection);
                        connection.ConnectionJoint.connectedBody = otherConnectable.rb;
                    }

                    //Create joint between connectable and parent to maintain position and rotation relative to the parent
                    if (activeConnections == 0) //Only do this once for all active connections
                    {
                        parentConnectionJoint = connectableParent.gameObject.AddComponent<FixedJoint>();
                        parentConnectionJoint.enablePreprocessing = false; //Makes joint more stable, but less firm
                        parentConnectionJoint.connectedBody = rb;
                    }

                    activeConnections++;
                    connection.connected = true;

                    provideConnectionFeedback();

                    OnConnected?.Invoke(this, connection);
                    connection.OnConnected.Invoke();

                    Debug.Log("[" + connection.Type + "] Connected " + connectableParent.name + " to " + otherConnectable.connectableParent.name);
                }
            }
            else //Connection failed, not allowed
            {
                failedConnections++;

                provideFailureFeedback();

                OnConnectionFailed.Invoke(otherConnectable);

                //Debug.Log("Connection cannot be made, connection not allowed between " + connectableParent.name + " and " + otherConnectable.connectableParent.name);
            }
        }

        private void stabilizeConnection()
        {
            if (shouldStabilizeConnection)
            {
                //Stabilize the connection by making the connectable parent kinematic for a specified amount of time
                connectableParent.rb.isKinematic = true;
                UnityAction jointStabilizationComplete = () =>
                {
                    connectableParent.rb.isKinematic = connectableParent.initKinematic;

                    OnJointStabilizationComplete?.Invoke();
                };
                StartCoroutine(runDelayedCallback(jointStabilizationPeriod, jointStabilizationComplete));

                //Stabilize rigidbody movements by enabling rigidbody limiters on the connectable parent for a specified amount of time
                connectableParent.rbLimiter.enabled = true;
                UnityAction forceStabilizationComplete = () =>
                {
                    connectableParent.rbLimiter.enabled = false;
                };
                StartCoroutine(runDelayedCallback(forceStabilizationPeriod, forceStabilizationComplete));
            }
        }

        public void disconnect(Connectable otherConnectable)
        {
            if (isConnected(otherConnectable, out Connection connection))
            {
                Debug.Log("[" + connection.Type + "] Disconnecting " + name + " from " + otherConnectable.name);

                //This is the connector, must reset receiver to kinematic
                if (connection.Type == Connection.ConnectionType.Connector)
                {
                    rb.isKinematic = true;
                    otherConnectable.rb.isKinematic = true;
                    disableConnectionJoint(connection);
                }

                activeConnections--;

                if (activeConnections == 0)
                {
                    //Destroy the parent connection joint to prevent it from fixing to a point in space
                    Destroy(parentConnectionJoint);

                    //Reset to initial local position and rotation to account for changes the joint may have caused
                    transform.localPosition = initLocalPos;
                    transform.localRotation = initLocalRot;
                }

                connection.connected = false;
                connection.enabled = false; //Disable connections for the specified reconnect delay

                //Create a delay before the connection will allow reconnection
                UnityAction reconnectCallback = () =>
                {
                    connection.enabled = true;
                };
                StartCoroutine(runDelayedCallback(reconnectDelay, reconnectCallback));

                provideDisconnnectionFeedback();

                OnDisconnected?.Invoke(this, connection);
                connection.OnDisconnected.Invoke();
            }
            else
            {
                Debug.Log("Cannot disconnect, " + name + " and " + otherConnectable.name + " are not connected.");
            }
        }

        public bool isConnected(Connectable otherConnectable)
        {
            return isConnected(otherConnectable, out Connection connection);
        }

        private bool isConnected(Connectable otherConnectable, out Connection connection)
        {
            //Connection is allowed check if it is connected
            if (isAllowed(otherConnectable, out Connection allowedConnection) && allowedConnection.connected)
            {
                connection = allowedConnection;
                return true;
            }

            //Connection not allowed
            connection = null;
            return false;
        }

        private bool isAllowed(Connectable otherConnectable, out Connection connection)
        {
            foreach (Connection allowedConnection in allowedConnections)
            {
                if (allowedConnection.OtherConnectable == otherConnectable)
                {
                    connection = allowedConnection;
                    return true;
                }
            }

            connection = null;
            return false;
        }

        #region Joint Management

        private void enableConnectionJoint(Connection connection)
        {
            //Transfer joint back to connectable from disabledJointContainer
            connection.ConnectionJoint = connection.ConnectionJoint.transferJoint(gameObject);
        }

        private void disableConnectionJoint(Connection connection)
        {
            //Transfer joint from connectable to disabledJointContiner
            connection.ConnectionJoint = connection.ConnectionJoint.transferJoint(disabledJointContainer.gameObject);
        }

        #endregion Joint Management

        #endregion Connection

        #region Feedback

        private void provideConnectionFeedback()
        {
            //Disable renderer to prevent unstable connectable from being seen
            rend.enabled = false;

            UnityAction enableRenderer = () =>
            {
                //Reenable renderer once the connection is stabilized
                rend.enabled = true;
            };
            StartCoroutine(runDelayedCallback(jointStabilizationPeriod, enableRenderer));

            //Give feedback to user, change color
            rend.material.SetColor(colorPropName, connectionSuccessColor);
        }

        private void provideFailureFeedback()
        {
            if (activeConnections == 0) //There is an active connection, don't change from green to red
            {
                //Give feedback to user, change color for specified amount of time
                rend.material.SetColor(colorPropName, connectionFailureColor);

                uint failureIndex = failedConnections;
                UnityAction failureFeedbackReset = () =>
                {
                    resetFailureFeedback(failureIndex);
                };
                StartCoroutine(runDelayedCallback(failureFeedbackDuration, failureFeedbackReset));
            }
        }

        private void resetFailureFeedback(uint failureIndex)
        {
            if (activeConnections == 0) //There is an active connection, don't change from green to red
            {
                //Has not been a new failure, reset feedback
                //Doing this check to avoid reset that was triggered by a later failure
                if (failureIndex == failedConnections)
                    rend.material.SetColor(colorPropName, defaultColor);
            }
        }

        private void provideDisconnnectionFeedback()
        {
            if (activeConnections == 0) //Still an active connection, don't change the color
            {
                //Reset to default color
                rend.material.SetColor(colorPropName, defaultColor);
            }
        }

        #endregion Feedback

        private IEnumerator runDelayedCallback(float delay, UnityAction callback)
        {
            yield return new WaitForSeconds(delay);
            callback.Invoke();
        }
    }
}