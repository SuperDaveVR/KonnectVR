using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
public class NetworkPlayerCleanup : NetworkBehaviour
{
    public NetworkVariable<ulong> PlayerId = new NetworkVariable<ulong>();

    private void Start()
    {
        GetPlayerId();
    }

    private void GetPlayerId()
    {
        if (IsHost || IsServer)
        {
            ulong playerID = this.gameObject.GetComponent<NetworkObject>().OwnerClientId;
            PlayerId.Value = playerID;
        }
    }

    private new void OnDestroy()
    {

        ReturnHome();
    }

    private void ReturnHome()
    {
        if (IsOwner)
            LoadingSceneManager.Instance.ReturnToHome();
    }
}
