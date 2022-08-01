using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RoomCodeText : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text TextBox;
    void Awake()
    {
        if (ConnectionManager.Instance != null && (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient))
        {
            TextBox.text = "Room Code: " + ConnectionManager.Instance.JoinCode;
        }
    }
}
