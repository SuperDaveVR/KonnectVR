using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserNameInputDefault : MonoBehaviour
{
    private TMPro.TMP_InputField inputField;

    private void Awake()
    {
        inputField = gameObject.GetComponent<TMPro.TMP_InputField>();
        FromPlayerManager();
    }

    private void FromPlayerManager()
    {
        if (PlayerManager.Instance != null)
        {
            inputField.text = PlayerManager.Instance.UserName;
        }
    }

    public void ChangeUserName()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.UserName = inputField.text;
        }
    }
}
