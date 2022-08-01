using KonnectVR.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRotateTransformToggle : MonoBehaviour, iToggle
{
    private ParentTransformOnXRGrab grabComponent;
    private RotateOnXRDrag rotateComponent;
    private int state = 1;

    // Start is called before the first frame update
    void Start()
    {
        grabComponent = this.GetComponent<ParentTransformOnXRGrab>();
        rotateComponent = this.GetComponent<RotateOnXRDrag>();
    }

    public void toggle()
    {
        switch (state)
        {
            case 1:     //Pressed when in rotate state
                grabComponent.enabled = false;
                rotateComponent.enabled = true;
                state++;
                break;

            case 2:     //Pressed when in translate state
                grabComponent.enabled = false;
                rotateComponent.enabled = false;
                state++;
                break;

            case 3:    //Pressed when in  default movement state
                grabComponent.enabled = true;
                rotateComponent.enabled = false;
                state = 1;
                break;

        }

    }
}
