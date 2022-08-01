using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    //name of connector group the connector is part of
    public string connectGroup;

    //Correct snap zone
    public GameObject correctConnectZone;

    //max ammount of connections
    public int maxConnections;

    //makes the attachment sphere visible during runtime
    public bool showSphere;

    //allow incorrect connections
    public bool allowIncorrect;

    //the connection is breakable
    public bool breakable;

    //hinge joint can bend when created
    public bool canBend;

    //Connections that will cause failure
    public List<GameObject> FailConnections = new List<GameObject>();

    //all connect zones connected to this 
    private List<GameObject> Connections = new List<GameObject>();

    //This connection zone
    private GameObject thisConnectZone;

    //material for when connection is correct
    public Material CorrectMaterial;

    //Default Material for connection
    public Material NeutralMaterial;

    //material for when connection is incorrect
    public Material IncorrectMaterial;

    private void Start()
    {
        thisConnectZone = this.gameObject;

        //prevents spheres from rendering
        if (!showSphere)
            thisConnectZone.GetComponent<Renderer>().enabled = false;

    }

    //Checks if connection zones have connected
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Connector>() != null 
            && correctConnectZone != null)
        {
            if((other.gameObject.GetComponent<Connector>().connectGroup == thisConnectZone.GetComponent<Connector>().connectGroup)
                && Connections.Count < maxConnections)
            {
                if (other == correctConnectZone.GetComponent<Collider>())
                {
                    CreateHinge(other.gameObject);
                    OnCorrectConnect(other.gameObject);
                }
                else if (allowIncorrect == true)
                {
                    CreateHinge(other.gameObject);
                    OnIncorrectConnect(other.gameObject);
                }
            }
            
        }
    }

    //Creates a hinge between two snap zones
    private void CreateHinge(GameObject zoneToConnect)
    {
        if (!Connections.Contains(zoneToConnect))
        {
            Debug.Log("Should be connecting");
            HingeJoint createdJoint = thisConnectZone.transform.parent.gameObject.AddComponent<HingeJoint>();
            createdJoint.connectedBody = zoneToConnect.transform.parent.gameObject.GetComponent<Rigidbody>();
            Connections.Add(zoneToConnect);
            if (canBend == false)
                createdJoint.useLimits = true;
            if (breakable)
                createdJoint.breakTorque = 100;
        }
    }

    //Tracks a connect snap
    private void OnCorrectConnect(GameObject ConnectedZone)
    {
        //sequence interaction event
        thisConnectZone.GetComponent<Renderer>().material = CorrectMaterial;
        ConnectedZone.GetComponent<Renderer>().material = CorrectMaterial;

    }
    
    //Tracks an incorrect snap
    private void OnIncorrectConnect(GameObject ConnectedZone)
    {
        if (FailConnections.Contains(ConnectedZone)) ;
            //trigger fail condition event
        thisConnectZone.GetComponent<Renderer>().material = IncorrectMaterial;
        ConnectedZone.GetComponent<Renderer>().material = IncorrectMaterial;
        //any other actions for incorrect connection
    }

    //Joint is broken
    void OnJointBreak(float breakForce)
    {
        
        //remove connection from connections list
        
    }

    public void RemoveConnection(GameObject RemovedBody)
    {
        thisConnectZone.GetComponent<Renderer>().material = NeutralMaterial;
        RemovedBody.GetComponent<Renderer>().material = NeutralMaterial;
        Connections.Remove(RemovedBody);
    }
}
