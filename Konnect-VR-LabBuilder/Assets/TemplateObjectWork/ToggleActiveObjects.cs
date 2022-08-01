using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActiveObjects : MonoBehaviour, iToggle
{
    [SerializeField] private List<GameObject> toggleableObjects;
    private int activeMode = 0;

    public void toggle()
    {
        var objectToDeactivate = toggleableObjects[activeMode];
        activeMode++;

        if (activeMode >= toggleableObjects.Count)
        {
            activeMode = 0;
        }

        var objectToActivate = toggleableObjects[activeMode];

        objectToDeactivate.SetActive(false);
        objectToActivate.SetActive(true);
    }
}
