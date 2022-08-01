using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cam;

    public enum LookVector
    {
        Left,
        Right,
        Forward,
        Backward,
        Up,
        Down,
    }

    [SerializeField]
    private LookVector lookVector = LookVector.Forward;

    [SerializeField, Tooltip("Rotation that will be applied to the transform's rotation before rotating to look at the camera.")]
    private Vector3 rotationOffset = Vector3.zero;

    private void Start()
    {
        cam = Camera.main?.transform;
    }

    private void Update()
    {
        if (cam)
        {
            Vector3 toCam = cam.position - transform.position;
            toCam.y = 0; //Flatten the vector to only rotation around the y-axis
            Quaternion toCamRot = Quaternion.FromToRotation(getLookDirection(), toCam);
            transform.rotation = toCamRot * Quaternion.Euler(rotationOffset); //Apply rotation offset after looking at the camera            
        }
    }

    private Vector3 getLookDirection()
    {
        switch (lookVector)
        {
            case LookVector.Left:
                return Vector3.left;
            case LookVector.Right:
                return Vector3.right;
            case LookVector.Forward:
                return Vector3.forward;
            case LookVector.Backward:
                return Vector3.back;
            case LookVector.Up:
                return Vector3.up;
            case LookVector.Down:
                return Vector3.down;
            default:
                return Vector3.forward;
        }
    }
}
