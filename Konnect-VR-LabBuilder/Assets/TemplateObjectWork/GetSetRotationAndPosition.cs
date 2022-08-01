using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSetRotationAndPosition : MonoBehaviour
{
    [SerializeField] private GameObject rotationObject;
    [SerializeField] private GameObject positionObject;

    public GameObject RotationObject
    {
        set
        {
            rotationObject = value;
        }
    } 
    private void Update()
    {
        //Debug.Log(getRotation());
        //Debug.Log(getPosition());
    }

    public Quaternion getRotation()
    {
        return rotationObject.transform.rotation;
    }

    public void setRotation(Quaternion newRotation)
    {
        rotationObject.transform.rotation = newRotation;
    }

    public Vector3 getPosition()
    {
        return positionObject.transform.position;
    }

    public void setPosition(Vector3 newPosition)
    {
        positionObject.transform.position = newPosition;
    }
    
    public Vector3 getScale()
    {
        return positionObject.transform.localScale;
    }

    public void setScale(Vector3 newScale)
    {
        positionObject.transform.localScale = newScale;
    }
    
}
