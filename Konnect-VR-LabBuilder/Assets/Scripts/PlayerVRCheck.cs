using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerVRCheck : MonoBehaviour
{
    [SerializeField] private List<GameObject> VROnlyObjects;
    [SerializeField] private bool SinglePlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        if (SinglePlayer)
            HeadsetSetup();
    }

    private void HeadsetSetup()
    {
        VROnlyObjectsVisible(HeadsetManager.Instance.VRHeadset);
    }

    public void VROnlyObjectsVisible(bool hasHeadset) {
        foreach (GameObject obj in VROnlyObjects)
        {
            obj.SetActive(hasHeadset);
        }
    }
}
