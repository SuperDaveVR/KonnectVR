using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkPlayerHandler : NetworkBehaviour
{
    [SerializeField] private List<GameObject> ObjectsToActivate;
    [SerializeField] private List<MonoBehaviour> MonoComponentsToActivate;

    [SerializeField] private PlayerVRCheck vrCheck;

    public NetworkVariable<bool> hasHeadset = new NetworkVariable<bool>();

    public NetworkVariable<ForceNetworkSerializeByMemcpy<FixedString64Bytes>> NetUserName = new NetworkVariable<ForceNetworkSerializeByMemcpy<FixedString64Bytes>>();
    private TextMeshPro NameTag;

    [SerializeField] private Avatar avatar;
    public NetworkVariable<NetworkCustomSettings> NetAvatarSettings = new NetworkVariable<NetworkCustomSettings>();

    // Start is called before the first frame update
    void Start()
    {
        if (IsOwner)
        {
            HandleHeadset();
        } 

        if (IsOwner && !IsHost)
        {
            OwnerActivation();
        }
        vrCheck.VROnlyObjectsVisible(hasHeadset.Value);
        GetAvatarData();
        UserNameHandler();
    }

    private void HandleHeadset()
    {
        Debug.Log("Checking for Headset");
        bool vrHeadset = HeadsetManager.Instance.VRHeadset;
        VRHeadsetServerRpc(vrHeadset);
        vrCheck.VROnlyObjectsVisible(vrHeadset);
    }

    private void OwnerActivation()
    {
        foreach (GameObject obj in ObjectsToActivate)
        {
            obj.SetActive(true);
        }
        
        foreach (MonoBehaviour mono in MonoComponentsToActivate)
        {
            mono.enabled = true;
        }
    }

    private void UserNameHandler()
    {
        if (IsOwner)
        {
            FixedString64Bytes userName = new FixedString64Bytes(PlayerManager.Instance.UserName);
            UserNameServerRpc(userName);
        } else
        {
            if (NameTag != null)
                NameTag.text = NetUserName.Value.Value.ToString();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        hasHeadset.OnValueChanged += OnHeadsetChange;
        NetUserName.OnValueChanged += OnUserNameChange;
        NetAvatarSettings.OnValueChanged += OnAvatarChange;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        hasHeadset.OnValueChanged -= OnHeadsetChange;
        NetUserName.OnValueChanged -= OnUserNameChange;
        NetAvatarSettings.OnValueChanged -= OnAvatarChange;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsOwner)
        {
            OwnerActivation();
        }
        Debug.Log(scene.name);
    }

    private void OnHeadsetChange(bool previous, bool current)
    {
        vrCheck.VROnlyObjectsVisible(hasHeadset.Value);
    }

    private void OnUserNameChange(ForceNetworkSerializeByMemcpy<FixedString64Bytes> previous, ForceNetworkSerializeByMemcpy<FixedString64Bytes> current)
    {
        if (NameTag != null)
            NameTag.text = NetUserName.Value.Value.ToString();
    }

    [ServerRpc]
    public void VRHeadsetServerRpc(bool vrHeadset)
    {
        hasHeadset.Value = vrHeadset;
    }

    [ServerRpc]
    public void UserNameServerRpc(ForceNetworkSerializeByMemcpy<FixedString64Bytes> userName)
    {
        NetUserName.Value = userName;
    }

    public void NetworkNameTag(TextMeshPro nameTag)
    {
        NameTag = nameTag;
    }

    private void GetAvatarData()
    {
        if (IsOwner)
        {
            NetworkCustomSettings networkCustomSettings = new NetworkCustomSettings(PlayerManager.Instance.AvatarData);
            ApplyAvatarData(networkCustomSettings);
            AvatarDataServerRpc(networkCustomSettings);
        } else
        {
            ApplyAvatarData(NetAvatarSettings.Value);
        }
    }

    [ServerRpc]
    public void AvatarDataServerRpc(NetworkCustomSettings networkCustomSettings)
    {
        NetAvatarSettings.Value = networkCustomSettings;
    }

    private void OnAvatarChange(NetworkCustomSettings previous, NetworkCustomSettings current)
    {
        if (!IsOwner)
        {
            ApplyAvatarData(NetAvatarSettings.Value);
        }
    }

    private void ApplyAvatarData(NetworkCustomSettings networkCustomSettings)
    {
        CustomSettings customSettings = new CustomSettings(networkCustomSettings);
        avatar.ApplyAvatarSettings(customSettings);
        Debug.Log(NetUserName.Value.Value.ToString() + " Avatar is Loaded");
    }
}
