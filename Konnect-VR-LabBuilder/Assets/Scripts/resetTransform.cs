using System.Collections;
using System.Collections.Generic;
using KonnectVR.Interactions;
using UnityEngine;

public class resetTransform : MonoBehaviour
{
    private double timer;
    private Vector3 startingPos;
    private Quaternion startingRot;
    private Transform currentTransform;  
    private Rigidbody rb;
    private GrabInteractable grabScript;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        startingPos = GetComponent<Transform>().position;
        currentTransform = GetComponent<Transform>();
        startingRot = GetComponent<Transform>().rotation;
        rb = GetComponent<Rigidbody>();
        grabScript = GetComponent<GrabInteractable>();



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatePosition()
    {
        timer = 0;
        startingPos = GetComponent<Transform>().position;
        currentTransform = GetComponent<Transform>();
        startingRot = GetComponent<Transform>().rotation;
        rb = GetComponent<Rigidbody>();
        grabScript = GetComponent<GrabInteractable>();
    }

    public void resetPosition()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
        currentTransform.position = startingPos;
        currentTransform.rotation = startingRot;


        /*
         timer = 0;
        currentTransform = GetComponent<Transform>();

        if (grabScript.isSelected)
        {
            timer = 0;
        }

        if (currentTransform.position != startingPos)
        {
            timer += Time.deltaTime;
        }

        if (timer > 1)
        {

            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            currentTransform.position = startingPos;
            currentTransform.rotation = startingRot;

            timer = 0;
        }
        */
    }
}
