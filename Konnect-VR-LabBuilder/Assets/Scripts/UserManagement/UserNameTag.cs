using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace KonnectVR.UserManagement
{
    [RequireComponent(typeof(TextMeshPro))]
    public class UserNameTag : MonoBehaviour
    {
        private TextMeshPro nameTag;
        [SerializeField]private NetworkPlayerHandler networkPlayer;

        [SerializeField]
        private bool isOnline = false;

        private void Awake()
        {
            nameTag = GetComponent<TextMeshPro>();

            isOnline = CheckIfOnline();

            if (!isOnline)
            {
                if (PlayerManager.Instance)
                    updateNameTag(PlayerManager.Instance.UserName);
            }
            else
            {
                NetworkUserName();
            }
        }

        private bool CheckIfOnline()
        {
            if (networkPlayer != null)
            {
                return true;
            } 

            return false;
        }

        private void updateNameTag(string userId)
        {
            nameTag.text = userId;
        }

        private void NetworkUserName()
        {
            networkPlayer.NetworkNameTag(nameTag);
        }
    }
}