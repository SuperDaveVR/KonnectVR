using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadsetManager : MonoBehaviour
{
    public static HeadsetManager Instance;
    public bool VRHeadset
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            VRHeadset = CheckForHeadset();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public bool CheckForHeadset()
    {
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
        {
            if (xrDisplay.running)
            {
                return true;
            }
        }
        return false;
    }
}
