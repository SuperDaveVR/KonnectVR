using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(NetworkLabInteractable))]
public class NetworkLoadFromAssetBundle : LabLoadFromAssetBundle
{
    public override void AddComponents(GameObject loadedObj)
    {
        base.AddComponents(loadedObj);

        HandleNetworkInteractable(loadedObj);

        //base.Reload();
    }

    private void HandleNetworkInteractable(GameObject loadedObj)
    {
        var xrGrab = loadedObj.GetComponent<XRGrabInteractable>();
        var networkInteractable = GetComponent<NetworkLabInteractable>();

        if (xrGrab)
            networkInteractable.AddXRListener(xrGrab);

        networkInteractable.SetupRigidbody();
    }
}
