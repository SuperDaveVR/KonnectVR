using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ErrorManager : MonoBehaviour
{
    public static ErrorManager Instance;

    public ErrorScreen ErrorScreen
    {
        get;
        set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ThrowError(string message, bool displayOnScreen, TMPro.TMP_Text displayBox = null)
    {
        Debug.Log("Error:" + message);

        if (displayOnScreen)
        {
            DisplayOnScreen(message);
        } else if (displayBox != null)
        {
            DisplayInBox(message, displayBox);
        }
    }

    public void ThrowError(string message, bool displayOnScreen, Text displayBox)
    {
        Debug.Log("Error:" + message);

        if (displayOnScreen)
        {
            DisplayOnScreen(message);
        }
        else if (displayBox != null)
        {
            DisplayInBox(message, displayBox);
        }
    }

    private void DisplayOnScreen(string message)
    {
        if (ErrorScreen != null)
        {
            ErrorScreen.gameObject.SetActive(true);
            ErrorScreen.DisplayError(message);
        }
    }

    private void DisplayInBox(string message, TMPro.TMP_Text displayBox)
    {
        displayBox.text = message;
    }

    private void DisplayInBox(string message, Text displayBox)
    {
        displayBox.text = message;
    }

    public void OKButton()
    {
        //TODO: Code for clearing displayed error messages and closing the error message box.
    }
}
