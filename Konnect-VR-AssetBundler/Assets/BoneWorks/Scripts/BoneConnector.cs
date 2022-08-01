using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneConnector : MonoBehaviour
{
    public GameObject collider1;
    public GameObject collider2;

    //collider1 trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other == collider2.GetComponent<Collider>())
            CreateHinge();
    }

    //Creates a hinge joint
    private void CreateHinge()
    {
        if (collider1.transform.parent.gameObject.GetComponent<HingeJoint>() == null)
        {
            collider1.transform.parent.gameObject.AddComponent<HingeJoint>();
            collider1.transform.parent.gameObject.GetComponent<HingeJoint>().connectedBody = collider2.transform.parent.gameObject.GetComponent<Rigidbody>();
            //collider1.transform.parent.gameObject.GetComponent<HingeJoint>().anchor = collider1.transform.position;
        }
    }
}
