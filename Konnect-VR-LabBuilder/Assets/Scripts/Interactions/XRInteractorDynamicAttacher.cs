using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractor))]
public class XRInteractorDynamicAttacher : MonoBehaviour
{
    private XRBaseInteractor interactor;
    
    private void Start()
    {
        interactor = GetComponent<XRBaseInteractor>();
        interactor.onSelectEntered.AddListener(handleSelectEntered);
    }

    private void handleSelectEntered(XRBaseInteractable interactable)
    {
        //Move attach transform to the position of the interactable
        interactor.attachTransform.position = interactable.transform.position;
        interactor.attachTransform.rotation = interactable.transform.rotation;
    }
}
