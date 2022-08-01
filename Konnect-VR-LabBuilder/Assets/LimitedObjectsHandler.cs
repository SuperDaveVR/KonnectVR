using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedObjectsHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> VROnlyObjects;
    [SerializeField] private List<GameObject> TwoDOnlyObjects;

    private bool HeadsetActive;

    private void Start()
    {
        HeadsetActive = HeadsetManager.Instance.VRHeadset;

        Handle2DOnlyObjects();
        HandleVROnlyObjects();
    }

    private void Handle2DOnlyObjects()
    {
        foreach(GameObject obj in TwoDOnlyObjects)
        {
            obj.SetActive(!HeadsetActive);
        }
    }

    private void HandleVROnlyObjects()
    {
        foreach (GameObject obj in VROnlyObjects)
        {
            obj.SetActive(HeadsetActive);
        }
    }
}
