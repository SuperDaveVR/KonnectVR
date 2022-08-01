using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinObject : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;

    private GameObject objectPin;

    private InputAction pin;

    private bool willPin;


    // Start is called before the first frame update
    void Start()
    {
        pin = actionAsset.FindActionMap("2D Camera").FindAction("Pin");
        pin.performed += keyPressed;
    }

    // Update is called once per frame
    void Update()
    {


    }


    public void pinObject()
    {

        if (willPin)
        {
            objectPin.GetComponent<Rigidbody>().isKinematic = true;
            objectPin = null;
        }
        else
        {
            objectPin.GetComponent<Rigidbody>().isKinematic = false;
        }

    }

    private void keyPressed(InputAction.CallbackContext context)
    {
        if (willPin)
        {
            willPin = false;
        }
        else
        {
            willPin = true;
        }
    }

    public void setObject()
    {
        willPin = false;
        objectPin = gameObject;

    }

}
