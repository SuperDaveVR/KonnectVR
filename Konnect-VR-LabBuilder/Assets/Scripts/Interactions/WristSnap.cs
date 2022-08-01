using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristSnap : MonoBehaviour
{
    public GameObject tablet;
    private Rigidbody tabletPhysics;

    // Start is called before the first frame update
    void Start()
    {
        if (tablet != null)
            tabletPhysics = tablet.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == tablet)
        {
            tabletPhysics.isKinematic = false;
            //tabletPhysics.useGravity = false;
        }
            
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == tablet)
        {
            tabletPhysics.isKinematic = true;
            tabletPhysics.useGravity = false;
        }
            
    }

    public void MakeFloat()
    {
        tabletPhysics.isKinematic = true;
    }
}
