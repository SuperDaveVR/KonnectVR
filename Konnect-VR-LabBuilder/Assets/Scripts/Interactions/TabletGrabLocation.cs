using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KonnectVR.Interactions;
using UnityEngine.XR.Interaction.Toolkit;

public class TabletGrabLocation : MonoBehaviour
{
    public XRBaseInteractor leftRayInteractor;
    public XRBaseInteractor rightRayInteracor;

    public GameObject attachTransform;

    XRGrabInteractable TabletScript;

    private void Start()
    {
        TabletScript = GetComponent<XRGrabInteractable>();
    }
    
    public void CheckXRBaseInteractor(XRBaseInteractor invoked)
    {
        if (invoked == leftRayInteractor)
            TabletScript.attachTransform = attachTransform.transform;
        else if (invoked == rightRayInteracor)
            TabletScript.attachTransform = attachTransform.transform;
        else
            TabletScript.attachTransform = null;
    }
}
