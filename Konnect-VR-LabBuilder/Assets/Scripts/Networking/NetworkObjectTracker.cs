using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkObjectTracker : NetworkBehaviour
{
    private bool start = false;

    [SerializeField] private GameObject TrackedObject;
    private ALoadFromAssetBundle AssetLoader;

    //Tracked Object ID
    public NetworkVariable<ulong> NetTrackedID = new NetworkVariable<ulong>(
        default,
        NetworkVariableBase.DefaultReadPerm, // Everyone
        NetworkVariableWritePermission.Server);

    public NetworkVariable<TrackedObjectData> TrackedData = new NetworkVariable<TrackedObjectData>(
        default,
        NetworkVariableBase.DefaultReadPerm, // Everyone
        NetworkVariableWritePermission.Server);

    private void Start()
    {
    }

    void FixedUpdate()
    {
        if (start && TrackedObject)
        {
            if (Owner())
            {
                OwnerTracking();
            }
            else
            {
                ClientTracking();
            }
        } else if (!TrackedObject)
        {
            if (AssetLoader)
                SetTrackedTarget();
        }
    }

    public void SetTrackedTarget(ALoadFromAssetBundle assetLoader = null)
    {
        if (IsClient && !AssetLoader)
        {
            AssetLoader = NetworkManager.SpawnManager.SpawnedObjects[NetTrackedID.Value].gameObject.GetComponent<ALoadFromAssetBundle>();
        } else if (!AssetLoader)
        {
            AssetLoader = assetLoader;
        }

        TrackedObject = AssetLoader.GraphicObject;

        if (TrackedObject != null)
        {
            start = true;
        }
    }

    private void OwnerTracking()
    {
        TrackedObjectData tracking = new TrackedObjectData();
        tracking.position = TrackedObject.transform.position;
        tracking.rotation = TrackedObject.transform.rotation;
        tracking.velocity = TrackedObject.GetComponent<Rigidbody>().velocity;
        tracking.angularVelocity = TrackedObject.GetComponent<Rigidbody>().angularVelocity;

        if (IsHost || IsServer)
        {
            SetToTrackedData(tracking);
            TrackedData.Value = tracking;
        } else
        {
            RequestTrackingServerRpc(tracking);
        }
    }

    private void SetToTrackedData(TrackedObjectData tracking)
    {
        TrackedObject.transform.position = tracking.position;
        TrackedObject.transform.rotation = tracking.rotation;

        if (!(IsHost || IsServer))
        {
            TrackedObject.GetComponent<Rigidbody>().velocity = tracking.velocity;
            TrackedObject.GetComponent<Rigidbody>().angularVelocity = tracking.angularVelocity;
        }
    }

    private void ClientTracking()
    {
        TrackedObject.transform.position = TrackedData.Value.position;
        TrackedObject.transform.rotation = TrackedData.Value.rotation;
    }

    private bool Owner()
    {
        bool isOwner;
        isOwner = NetworkManager.SpawnManager.SpawnedObjects[NetTrackedID.Value].gameObject.GetComponent<NetworkObject>().IsOwner;

        return isOwner;
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestTrackingServerRpc(TrackedObjectData data)
    {
        TrackedData.Value = data;
    }

    [ServerRpc]
    public void SetTrackerServerRpc(ulong trackedID)
    {
        NetTrackedID.Value = trackedID;
    }
}
