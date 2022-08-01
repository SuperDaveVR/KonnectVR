using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField UserName;
    [SerializeField] private TMP_InputField RoomCode;
    [SerializeField] private TMP_Text ErrorDisplay;

    public void Connect(string sceneName)
    {
        if (ValidName() && ValidCode()) {
            ConnectionManager.Instance.UserName = UserName.text;
            ConnectionManager.Instance.JoinCode = RoomCode.text;
            LoadingSceneManager.Instance.ClientLoadSceneFromManager(sceneName);
        }
    }

    private bool ValidName()
    {
        bool isValid = true;

        if (string.IsNullOrWhiteSpace(UserName.text))
        {
            isValid = false;
            string message = "Username cannot be blank!";
            ErrorManager.Instance.ThrowError(message, false, ErrorDisplay);
        }

        return isValid;
    }

    private bool ValidCode()
    {
        bool isValid = true;

        string roomCodeStr = RoomCode.text;

        if (string.IsNullOrWhiteSpace(roomCodeStr) || roomCodeStr.Length != 6)
        {
            isValid = false;
            string message = "Room code must be 6 characters long!";
            ErrorManager.Instance.ThrowError(message, false, ErrorDisplay);
        }

        return isValid;
    }
}
