using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class XRInteractableDynamicAttacher : MonoBehaviour
{
    private XRGrabInteractable interactable;
    
    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        interactable.onSelectEntered.AddListener(handleSelectEntered);
    }

    private void handleSelectEntered(XRBaseInteractor interactor)
    {
        //Move attach transform to the position of the interactable        
        if (interactable.attachTransform)
        {
            interactor.attachTransform.position = interactable.attachTransform.position;
            interactor.attachTransform.rotation = interactable.attachTransform.rotation;
        }
        else //No attach transform specified
        {
            interactor.attachTransform.position = interactable.transform.position;
            interactor.attachTransform.rotation = interactable.transform.rotation;
        }
    }
}
