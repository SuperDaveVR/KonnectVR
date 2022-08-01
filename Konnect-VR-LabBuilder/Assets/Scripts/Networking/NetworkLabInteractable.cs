using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ALoadFromAssetBundle))]
public class NetworkLabInteractable : NetworkBehaviour
{
    [SerializeField] GameObject TrackerPrefab;
    private NetworkObjectTracker tracker;

    public NetworkVariable<ulong> InteractingClientId = new NetworkVariable<ulong>();

    //Asset Loader Variable
    private NetworkVariable<ForceNetworkSerializeByMemcpy<FixedString64Bytes>> assetBundleName = new NetworkVariable<ForceNetworkSerializeByMemcpy<FixedString64Bytes>>();
    private NetworkVariable<ForceNetworkSerializeByMemcpy<FixedString64Bytes>> assetName = new NetworkVariable<ForceNetworkSerializeByMemcpy<FixedString64Bytes>>();
    public NetworkVariable<int> LayerNum = new NetworkVariable<int>();
    [SerializeField] private ALoadFromAssetBundle assetLoader;

    //Graphic Object Transform and Rigidbody

    public NetworkVariable<bool> NetGravity = new NetworkVariable<bool>(
        default,
        NetworkVariableBase.DefaultReadPerm, // Everyone
        NetworkVariableWritePermission.Server);

    public NetworkVariable<bool> NetKinematic = new NetworkVariable<bool>(
        default,
        NetworkVariableBase.DefaultReadPerm, // Everyone
        NetworkVariableWritePermission.Server);

    public NetworkVariable<bool> NetDefaultGravity = new NetworkVariable<bool>(
        default,
        NetworkVariableBase.DefaultReadPerm, // Everyone
        NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> NetDefaultKinematic = new NetworkVariable<bool>(
        default,
        NetworkVariableBase.DefaultReadPerm, // Everyone
        NetworkVariableWritePermission.Server);

    private NetworkVariable<ulong> trackerId = new NetworkVariable<ulong>(
        default,
        NetworkVariableBase.DefaultReadPerm, // Everyone
        NetworkVariableWritePermission.Server);

    //Checks if ownership coroutine is running
    private IEnumerator co;
    private bool CR_IsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
        ClientDisconnectSetup();
    }
    
    private void Setup()
    {
        if (!assetLoader)
            assetLoader = this.gameObject.GetComponent<ALoadFromAssetBundle>();

        if (IsHost || IsServer)
        {
            assetBundleName.Value = new FixedString64Bytes(assetLoader.AssetBundleName);
            assetName.Value = new FixedString64Bytes(assetLoader.AssetName);
        }
        else if (NetworkManager.Singleton.IsConnectedClient)
        {
            assetLoader.AssetBundleName = assetBundleName.Value.Value.ToString();
            assetLoader.AssetName = assetName.Value.Value.ToString();
            assetLoader.Reload();
            tracker = NetworkManager.Singleton.SpawnManager.SpawnedObjects[trackerId.Value].gameObject.GetComponent<NetworkObjectTracker>();
            tracker.SetTrackedTarget(assetLoader);
        }
    }

    private void ClientDisconnectSetup()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if (IsHost || IsServer)
            {
                if (id == InteractingClientId.Value)
                {
                    Debug.Log("Disconnected Client! ID:" + id);
                    ReleaseServerRpc();

                    Rigidbody rb = assetLoader.GraphicObject.GetComponent<Rigidbody>();
                    rb.useGravity = NetGravity.Value;
                    rb.isKinematic = NetKinematic.Value;
                }
            }
        };
    }

    public override void OnNetworkSpawn()
    {
        NetGravity.OnValueChanged += OnGravityChanged;
        NetKinematic.OnValueChanged += OnKinematicChanged;

        InteractingClientId.OnValueChanged += OnInteractorChange;
    }

    public override void OnNetworkDespawn()
    {
        NetGravity.OnValueChanged -= OnGravityChanged;
        NetKinematic.OnValueChanged -= OnKinematicChanged;

        InteractingClientId.OnValueChanged -= OnInteractorChange;
    }

    public void SetupRigidbody()
    {
        Rigidbody rb = assetLoader.GraphicObject.GetComponent<Rigidbody>();
        if (IsHost || IsServer)
        {
            NetGravity.Value = rb.useGravity;
            NetKinematic.Value = rb.isKinematic;

            NetDefaultGravity.Value = rb.useGravity;
            NetDefaultKinematic.Value = rb.isKinematic;
        }
        else
        {

            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    private void OnGravityChanged(bool previous, bool current)
    {
        Rigidbody rb = assetLoader.GraphicObject.GetComponent<Rigidbody>();
        Debug.Log("Owned by: " + OwnerClientId);
        if (IsOwner)
        {
            rb.useGravity = NetGravity.Value;
        } else
        {
            rb.useGravity = false;
        }
    }

    private void OnKinematicChanged(bool previous, bool current)
    {
        Rigidbody rb = assetLoader.GraphicObject.GetComponent<Rigidbody>();

        if (rb && IsOwner)
        {
            rb.isKinematic = NetKinematic.Value;
        } else
        {
            rb.isKinematic = true;
        }
    }

    private void OnInteractorChange(ulong previous, ulong current)
    {
        XRGrabInteractable xrGrab = assetLoader.GraphicObject.GetComponent<XRGrabInteractable>();
        ulong clientId = NetworkManager.Singleton.LocalClientId;
        if (InteractingClientId.Value != clientId && xrGrab.isSelected)
        {
            //force drop
            xrGrab.interactionManager.CancelInteractableSelection(xrGrab);
        }
    }

    private void OnXRGrabInteraction(SelectEnterEventArgs args)
    {
        RequestOwnershipServerRpc();
        if (CR_IsRunning)
        {
            StopCoroutine(co);
            CR_IsRunning = false;
        }
    }

    private void OnXRGrabEnd(SelectExitEventArgs args)
    {
        Rigidbody rb = assetLoader.GraphicObject.GetComponent<Rigidbody>();
        rb.useGravity = NetDefaultGravity.Value;
        rb.isKinematic = NetDefaultKinematic.Value;
        co = EndOwnership();
        StartCoroutine(co);
        //ReleaseServerRpc();
    }

    public void AddXRListener(XRGrabInteractable xrGrab)
    {
        xrGrab.selectEntered.AddListener(OnXRGrabInteraction);
        xrGrab.selectExited.AddListener(OnXRGrabEnd);
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestOwnershipServerRpc(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        GetComponent<NetworkObject>().ChangeOwnership(clientId);

        //Actions to Take After Ownership Given
        InteractingClientId.Value = clientId;
        RigidbodyServerRpc(!NetDefaultGravity.Value, !NetDefaultKinematic.Value);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ReleaseServerRpc(ServerRpcParams serverRpcParams = default)
    {
        InteractingClientId.Value = 0;

        //Place any changes that need to be made before removing ownership.
        GetComponent<NetworkObject>().RemoveOwnership();
        RigidbodyServerRpc(NetDefaultGravity.Value, NetDefaultKinematic.Value);
    }

    [ServerRpc(RequireOwnership = false)]
    public void RigidbodyServerRpc(bool gravity, bool kinematic)
    {
        NetGravity.Value = gravity;
        NetKinematic.Value = kinematic;
    }

    public void LoadObjData(PlacedObjectSaveData data)
    {
        Debug.Log("Attempting Spawn");
        this.gameObject.GetComponent<NetworkObject>().Spawn();

        SpawnTracker();
    }

    public void SpawnTracker()
    {
        Debug.Log("Spawning Tracker");
        GameObject trackerObj = Instantiate(TrackerPrefab, Vector3.zero, Quaternion.identity, transform);
        tracker = trackerObj.GetComponent<NetworkObjectTracker>();
        tracker.SetTrackedTarget(assetLoader);
        trackerObj.GetComponent<NetworkObject>().Spawn();
        trackerId.Value = trackerObj.GetComponent<NetworkObject>().NetworkObjectId;
        tracker.SetTrackerServerRpc(this.GetComponent<NetworkObject>().NetworkObjectId);
    }

    private IEnumerator EndOwnership()
    {
        CR_IsRunning = true;
        yield return new WaitForSeconds(5);

        ulong clientId = NetworkManager.Singleton.LocalClientId;
        if (InteractingClientId.Value == clientId)
            ReleaseServerRpc();
        Debug.Log("End Ownership Complete!");
    }
}