using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    static private int NEUTRAL = 0;
    static private int ROTATE = 1;
    static private int TRANSLATE = 2;

    public float mouseSensitivity = 0.1f;
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private GameObject movement;
    public GameObject leftHand;
    public GameObject rightHand;
    float xRotation = -0f;
    private InputAction SwitchFocus;
    private InputAction Rotate;
    private InputAction Translate;
    private InputAction yAxis;
    private InputAction xAxis;
    private InputAction hold;

    private bool hasFocus = false;
    private int state = 0;

    public GameObject heldObject;
    private GameObject anchorObject;
    public float RotationSpeed = 0.5f;
    public float TranslationSpeed = 0.002f;




    // Start is called before the first frame update
    void Start()
    {
        SwitchFocus = actionAsset.FindActionMap("2D Camera").FindAction("Mouse Control");
        SwitchFocus.performed += controlMouse;

        Rotate = actionAsset.FindActionMap("2D Camera").FindAction("Rotate Object");
        Rotate.performed += rotateObject;

        Translate = actionAsset.FindActionMap("2D Camera").FindAction("Translate Object");
        Translate.performed += translateObject;

        yAxis = actionAsset.FindActionMap("2D Camera").FindAction("Mouse Y");
        yAxis.performed += OnRotationY;

        xAxis = actionAsset.FindActionMap("2D Camera").FindAction("Mouse X");
        xAxis.performed += OnRotationX;

        hold = actionAsset.FindActionMap("2D Camera").FindAction("Move Hold");
        hold.performed += moveHolder;


        anchorObject = GameObject.Find("LeftHand Controller/Ray Offset/Ray Interactor/[Ray Interactor] Attach");
    }

    private void OnDestroy()
    {
        SwitchFocus.performed -= controlMouse;
        Rotate.performed -= rotateObject;
        Translate.performed -= translateObject;
        yAxis.performed -= OnRotationY;
        xAxis.performed -= OnRotationX;
        hold.performed -= moveHolder;
    }

    // Update is called once per frame
    void Update()
    {


    }



    private void controlMouse(InputAction.CallbackContext context)
    {
        if (hasFocus)
        {
            movement.SetActive(false);
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            Cursor.lockState = UnityEngine.CursorLockMode.None;
            hasFocus = false;
        }
        else
        {
            state = NEUTRAL;
            movement.SetActive(true);

            leftHand.SetActive(true);
            rightHand.SetActive(true);
            Cursor.lockState = UnityEngine.CursorLockMode.Locked;
            hasFocus = true;
        }
    }

    private void moveHolder(InputAction.CallbackContext context)
    {
        if (hasFocus)
        {
            StartCoroutine(Wait());

        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.01f);

        //set the held object to a set location instead of in front of the screen
        //location can be changed by changing the values in the vector
        if (heldObject != null)
            heldObject.transform.localPosition = new Vector3(0.1f, -0.08f, 0.05f);
    }

    private void rotateObject(InputAction.CallbackContext context)
    {
        if (hasFocus)
        {
            switch (state)
            {
                case 1:     //Pressed when in rotate state
                    movement.SetActive(true);
                    state = NEUTRAL;
                    break;

                case 2:     //Pressed when in translate state
                    movement.SetActive(false);
                    state = ROTATE;
                    break;

                default:    //Pressed when in  default movement state
                    if (heldObject != null)
                    {
                        movement.SetActive(false);
                        state = ROTATE;
                    }
                    break;

            }
        }
    }

    private void translateObject(InputAction.CallbackContext context)
    {
        if (hasFocus)
        {
            switch (state)
            {
                case 1:     //Pressed when in rotate state
                    movement.SetActive(false);
                    state = TRANSLATE;
                    break;

                case 2:     //Pressed when in translate state
                    movement.SetActive(true);
                    state = NEUTRAL;
                    break;

                default:    //Pressed when in  default movement state
                    if (heldObject != null)
                    {
                        movement.SetActive(false);
                        state = TRANSLATE;
                    }
                    break;

            }
        }
    }


    private void OnRotationY(InputAction.CallbackContext Context)
    {


        if (hasFocus && state == NEUTRAL)
        {
            float mouseY = Context.ReadValue<float>() * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        if (state == ROTATE)
        {
            if (heldObject != null)
            {
                float rotateY = Context.ReadValue<float>() * RotationSpeed;

                heldObject.transform.Rotate(rotateY, 0, 0);
            } else
            {
                state = NEUTRAL;
                movement.SetActive(true);
            }
        }

        if (state == TRANSLATE)
        {
            if (heldObject != null)
            {
                float moveY = Context.ReadValue<float>() * TranslationSpeed;

                heldObject.transform.Translate(0, moveY, 0, Camera.main.transform);
            } else
            {
                state = NEUTRAL;
                movement.SetActive(true);
            }
        }
    }

    private void OnRotationX(InputAction.CallbackContext Context)
    {

        if (state == ROTATE)
        {
            if (heldObject != null)
            {
                float rotateX = Context.ReadValue<float>() * RotationSpeed;

                heldObject.transform.Rotate(0, rotateX, 0);
            } else
            {
                state = NEUTRAL;
                movement.SetActive(true);
            }

        }
        if (state == TRANSLATE)
        {
            if (heldObject != null)
            {
                float moveX = Context.ReadValue<float>() * TranslationSpeed;

                heldObject.transform.Translate(moveX, 0, 0, Camera.main.transform);
            } else
            {
                state = NEUTRAL;
                movement.SetActive(true);
            }
        }
    }


    public void setHeldObject()
    {
        heldObject = anchorObject;
    }

    public void removeHeldObject()
    {
        heldObject = null;
    }

}
