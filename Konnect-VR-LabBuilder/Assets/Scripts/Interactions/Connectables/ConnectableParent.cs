/*
 * @author Marty Fitzer
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KonnectVR.Interactions.Connectables
{
    [RequireComponent(typeof(GrabInteractable))]
    [RequireComponent(typeof(Rigidbody))]
    public class ConnectableParent : MonoBehaviour
    {
        internal GrabInteractable grabInteractable { get; private set; }

        internal Rigidbody rb { get; private set; }

        internal RigidbodyLimiter rbLimiter { get; private set; }

        /// <summary>
        /// Was the rigidbody kinematic on start?
        /// </summary>
        internal bool initKinematic { get; private set; } = false;

        private Connectable[] connectables;

        private struct ConnectionInfo
        {
            public Connectable connector;
            public UnityAction<Interactor> connectorInteractionHandler;

            public Connectable receiver;
            public UnityAction<Interactor> receiverInteractionHandler;
        }

        private uint connectionIdCounter = 0;
        private Dictionary<uint, ConnectionInfo> connectionsInfo;

        private void Awake()
        {
            connectionsInfo = new Dictionary<uint, ConnectionInfo>();
            connectables = GetComponentsInChildren<Connectable>();
            foreach (Connectable connectable in connectables)
            {
                connectable.connectableParent = this;
                connectable.OnConnected += handleConnection;
            }

            rb = GetComponent<Rigidbody>();
            initKinematic = rb.isKinematic;
        }

        private void Start()
        {
            grabInteractable = GetComponent<GrabInteractable>();

            rbLimiter = gameObject.AddComponent<RigidbodyLimiter>();
            rbLimiter.LimitVelocity = true;
            rbLimiter.MaxVelocityMagnitude = 0.1f;
            rbLimiter.LimitAngularVelocity = true;
            rbLimiter.MaxAngularVelocityMagnitude = 0.1f;
            rbLimiter.enabled = false;
        }

        private void handleConnection(Connectable connectable, Connection connection)
        {
            if (connection.Type == Connection.ConnectionType.Connector)
            {
                Connectable connector = connectable;
                Connectable receiver = connection.OtherConnectable;

                uint connectionId = connectionIdCounter++;

                UnityAction<Interactor> receiverInteractionHandler = (interactor) =>
                {
                    handleReceiverParentGrabbed(connectionId);
                };

                UnityAction<Interactor> connectorInteractionHandler = (interactor) =>
                {
                    handleConnectorParentGrabbed(connectionId);
                };

                receiver.connectableParent.grabInteractable.OnInteractionStarted.AddListener(receiverInteractionHandler);
                connector.connectableParent.grabInteractable.OnInteractionStarted.AddListener(connectorInteractionHandler);

                ConnectionInfo info = new ConnectionInfo()
                {
                    connector = connector,
                    connectorInteractionHandler = connectorInteractionHandler,
                    receiver = receiver,
                    receiverInteractionHandler = receiverInteractionHandler
                };

                connectionsInfo.Add(connectionId, info);
            }
        }

        private void handleReceiverParentGrabbed(uint connectionId)
        {
            if (connectionsInfo.ContainsKey(connectionId))
            {
                ConnectionInfo info = connectionsInfo[connectionId];

                //Both connector parents are interacting, start processing possible disconnection
                if (info.connector.connectableParent.grabInteractable.interacting)
                {
                    processDisconnection(connectionId, info);
                }
            }
        }

        private void handleConnectorParentGrabbed(uint connectionId)
        {
            if (connectionsInfo.ContainsKey(connectionId))
            {
                ConnectionInfo info = connectionsInfo[connectionId];

                //Both connector parents are interacting, start processing possible disconnection
                if (info.receiver.connectableParent.grabInteractable.interacting)
                {
                    processDisconnection(connectionId, info);
                }
            }
        }

        private void processDisconnection(uint connectionId, ConnectionInfo info)
        {
            info.connector.disconnect(info.receiver);
            info.receiver.disconnect(info.connector);

            //Remove ConnectionInfo from
            connectionsInfo.Remove(connectionId);

            info.receiver.connectableParent.grabInteractable.OnInteractionStarted.RemoveListener(info.receiverInteractionHandler);
            info.connector.connectableParent.grabInteractable.OnInteractionStarted.RemoveListener(info.connectorInteractionHandler);
        }
    }
}