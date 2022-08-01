using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private string userName = "User";
    public string UserName
    {
        get
        {
            return userName;
        }
        set
        {
            userName = value;
            PlayerPrefs.SetString("UserName", value);
        }
    }

    public CustomSettings AvatarData{
        get
        {
            return AvatarSaveSystem.LoadCustomSettings();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            CheckForUserNameInPrefs();
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void CheckForUserNameInPrefs()
    {
        if (PlayerPrefs.HasKey("UserName"))
        {
            userName = PlayerPrefs.GetString("UserName");
        } else
        {
            userName = "User";
        }
    }
}
