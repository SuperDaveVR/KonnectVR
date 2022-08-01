using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private string defaultName;
    [SerializeField] private string resourcePath;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject parentObject;

    [SerializeField] private bool hasBundle;
    [SerializeField] private string assetBundleName;

    public string DefaultName
    {
        set
        {
            defaultName = value;
        }
    }

    public string AssetBundleName
    {
        set
        {
            assetBundleName = value;
        }
    }

    public bool HasBundle
    {
        set
        {
            hasBundle = value;
        }
    }

    private void Awake()
    {
        if (defaultName == null || defaultName == "")
        {
            defaultName = "PlacedObject";
        }
        if (playerObject == null)
        {
            playerObject = GameObject.Find("XR Rig");
        }

        if (resourcePath == null || resourcePath == "")
        {
            resourcePath = "Prefabs/TemplateLabPlacementObject";
        }

        if (parentObject == null)
        {
            parentObject = GameObject.Find("PlacedObjects");
        }
    }

    public void placeObject(Vector3 placeLocation, Quaternion placeRotation)
    {
        GameObject placedObject = Instantiate(Resources.Load(resourcePath), placeLocation, placeRotation, parentObject.transform) as GameObject;
        placedObject.name = defaultName;

        if (hasBundle)
        {
            ALoadFromAssetBundle assetLoader = placedObject.GetComponent<ALoadFromAssetBundle>();
            assetLoader.AssetBundleName = assetBundleName;
            assetLoader.AssetName = defaultName;
            assetLoader.Reload();
        }

        PlacedObjectsHandler placedObjectsHandler = parentObject.GetComponent<PlacedObjectsHandler>();
        placedObjectsHandler.AddObject(placedObject);
    }

    public void placeInFrontOfPlayer()
    {
        Vector3 placeLocation = (playerObject.transform.forward * 0.5f) + playerObject.transform.position;
        Quaternion placeRotation = Quaternion.Euler(0, 0, 0);

        placeObject(placeLocation, placeRotation);
    }
}
