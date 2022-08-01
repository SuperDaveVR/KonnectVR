using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorDebug : MonoBehaviour
{
    public void TestInteractorHover(UnityEngine.XR.Interaction.Toolkit.HoverEnterEventArgs hoverEnterEvent)
    {
        Debug.Log(hoverEnterEvent.interactable.name);
    }
}
