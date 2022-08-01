using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class rotation : MonoBehaviour
{
    public GameObject rotateObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Brings the top down
    public void RotateDown()
    {
        rotateObject.transform.Rotate(90f, 0f, 0f);
    }

    //Brings the bottom up
    public void RotateUp()
    {
        rotateObject.transform.Rotate(-90f, 0f, 0f);
    }
}
