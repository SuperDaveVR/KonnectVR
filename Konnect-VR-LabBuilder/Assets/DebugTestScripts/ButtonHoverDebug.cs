using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverDebug : MonoBehaviour
{
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(eventData.currentInputModule.name);
    }
}
