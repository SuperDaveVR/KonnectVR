using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorScreen : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text ErrorMessageBox;
    
    public void DisplayError(string message)
    {
        ErrorMessageBox.text = message;
    }
    public void OKButton()
    {
        this.gameObject.SetActive(false);
    } 
}
