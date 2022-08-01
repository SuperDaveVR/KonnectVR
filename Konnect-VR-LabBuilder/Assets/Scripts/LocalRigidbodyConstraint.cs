using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LocalRigidbodyConstraint : MonoBehaviour
{
    [HideInInspector]
    public bool freezeXPosition
    {
        get
        {
            return freezeXPos;
        }
        set
        {
            if (value && !freezeXPos) //Initiate freeze on x position
                frozenXPosition = transform.localPosition.x;

            freezeXPos = value;
        }
    }
    [HideInInspector, SerializeField]
    private bool freezeXPos = false;
    private float frozenXPosition = 0f;

    [HideInInspector]
    public bool freezeYPosition
    {
        get
        {
            return freezeYPos;
        }
        set
        {
            if (value && !freezeYPos) //Initiate freeze on y position
                frozenYPosition = transform.localPosition.y;

            freezeYPos = value;
        }
    }
    [HideInInspector, SerializeField]
    private bool freezeYPos = false;
    private float frozenYPosition = 0f;

    [HideInInspector]
    public bool freezeZPosition
    {
        get
        {
            return freezeZPos;
        }
        set
        {
            if (value && !freezeXPos) //Initiate freeze on z position
                frozenZPosition = transform.localPosition.z;

            freezeZPos = value;
        }
    }
    [HideInInspector, SerializeField]
    private bool freezeZPos = false;
    private float frozenZPosition = 0f;

    [HideInInspector]
    public bool freezeXRotation
    {
        get
        {
            return freezeXRot;
        }
        set
        {
            if (value && !freezeXRot) //Initiate freeze on x rotation
                frozenXRotation = transform.localEulerAngles.x;

            freezeXRot = value;
        }
    }
    [HideInInspector, SerializeField]
    private bool freezeXRot = false;
    private float frozenXRotation = 0f;

    [HideInInspector]
    public bool freezeYRotation
    {
        get
        {
            return freezeYRot;
        }
        set
        {
            if (value && !freezeYRot) //Initiate freeze on y rotation
                frozenYRotation = transform.localEulerAngles.y;

            freezeYRot = value;
        }
    }
    [HideInInspector, SerializeField]
    private bool freezeYRot = false;
    private float frozenYRotation = 0f;

    [HideInInspector]
    public bool freezeZRotation
    {
        get
        {
            return freezeZRot;
        }
        set
        {
            if (value && !freezeZRot) //Initiate freeze on z rotation
                frozenZRotation = transform.localEulerAngles.z;

            freezeZRot = value;
        }
    }
    [HideInInspector, SerializeField]
    private bool freezeZRot = false;
    private float frozenZRotation = 0f;

    #region Editor Variables

    [HideInInspector]
    public bool showConstraints = false;

    #endregion Editor Variables

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        frozenXPosition = transform.localPosition.x;
        frozenYPosition = transform.localPosition.y;
        frozenZPosition = transform.localPosition.z;
        frozenXRotation = transform.localEulerAngles.x;
        frozenYRotation = transform.localEulerAngles.y;
        frozenZRotation = transform.localEulerAngles.z;
    }

    #region Transform Constraint

    private void constrainPosition()
    {        
        Vector3 localPos = transform.localPosition;
        float xLocalPos = freezeXPosition ? frozenXPosition : localPos.x;
        float yLocalPos = freezeYPosition ? frozenYPosition : localPos.y;
        float zLocalPos = freezeZPosition ? frozenZPosition : localPos.z;
        transform.localPosition = new Vector3(xLocalPos, yLocalPos, zLocalPos);
    }

    private void constrainRotation()
    {
        Vector3 localRot = transform.localEulerAngles;
        float xLocalRot = freezeXRotation ? frozenXRotation : localRot.x;
        float yLocalRot = freezeYRotation ? frozenYRotation : localRot.y;
        float zLocalRot = freezeZRotation ? frozenZRotation : localRot.z;

        transform.localEulerAngles = new Vector3(xLocalRot, yLocalRot, zLocalRot);
    }

    #endregion Transform Constraint

    #region Rigidbody Constraint

    private void constrainVelocity()
    {
        Vector3 worldVelocity = rb.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(worldVelocity);

        float xVelocity = freezeXPosition ? 0f : localVelocity.x;
        float yVelocity = freezeYPosition ? 0f : localVelocity.y;
        float zVelocity = freezeZPosition ? 0f : localVelocity.z;
        Vector3 constrainedLocalVelocity = new Vector3(xVelocity, yVelocity, zVelocity);

        //Set the velocity equal to constrainedLocalVelocity in world space
        rb.velocity = transform.TransformDirection(constrainedLocalVelocity);
    }

    private void constrainAngularVelocity()
    {
        Vector3 worldAngularVelocity = rb.angularVelocity;
        Vector3 localAngularVelocity = transform.InverseTransformDirection(worldAngularVelocity);

        float xAngularVelocity = freezeXRotation ? 0f : localAngularVelocity.x;
        float yAngularVelocity = freezeYRotation ? 0f : localAngularVelocity.y;
        float zAngularVelocity = freezeZRotation ? 0f : localAngularVelocity.z;
        Vector3 constrainedLocalAngularVelocity = new Vector3(xAngularVelocity, yAngularVelocity, zAngularVelocity);

        //Set the angular velocity equal to constrainedLocalVelocity in world space
        rb.angularVelocity = transform.TransformDirection(constrainedLocalAngularVelocity);
    }

    #endregion Rigidbody Constraint

    private void FixedUpdate()
    {
        constrainVelocity();
        constrainAngularVelocity();
    }

    private void LateUpdate()
    {
        constrainPosition();
        constrainRotation();
    }
}
