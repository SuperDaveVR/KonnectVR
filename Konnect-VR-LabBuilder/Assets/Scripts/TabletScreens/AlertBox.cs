using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBox : MonoBehaviour
{
    [SerializeField] GameObject AlertTextObj;
    GameObject triggeringObj;
    bool closeOnOK = false;

    public void triggerAlert(string alertText, GameObject triggeringObj, bool closeOnOK)
    {
        this.gameObject.SetActive(true);
        AlertTextObj.GetComponent<TMPro.TMP_Text>().text = alertText;
        this.triggeringObj = triggeringObj;
        this.closeOnOK = closeOnOK;
    }

    public void OKButton()
    {
        this.gameObject.SetActive(false);
        
        if (closeOnOK)
        {
            //triggeringObj.SetActive(false);
            triggeringObj.GetComponent<TabletScreenHandler>().TabletMenuHandler.GoToSaved();
        }
    }
}
