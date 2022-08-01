using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveablePlacedObject : MonoBehaviour
{
    [SerializeField] private GameObject positionDataObject;
    [SerializeField] private GameObject rotationDataObject;
    [SerializeField] private GameObject scaleDataObject;
    [SerializeField] private GameObject graphicDataObject;
    [SerializeField] private GameObject assetBundleObject;

    public GameObject RotationScaleDataObject {
        set
        {
            rotationDataObject = value;
        }
    }

    public GameObject GraphicDataObject
    {
        set
        {
            graphicDataObject = value;
        }
    }

    public PlacedObjectSaveData GetSaveableData()
    {
        PlacedObjectSaveData saveData = new PlacedObjectSaveData();
        saveData.name = this.gameObject.name;
        saveData = SetPositionData(saveData);
        saveData = SetRotationData(saveData);
        saveData = SetScaleData(saveData);
        saveData = SetAssetBundleData(saveData);
        saveData = SetAssetNameData(saveData);
        return saveData;
    }

    private PlacedObjectSaveData SetPositionData(PlacedObjectSaveData data)
    {
        Vector3 position = positionDataObject.transform.position;
        data.locX = position.x;
        data.locY = position.y;
        data.locZ = position.z;

        return data;
    }

    private PlacedObjectSaveData SetRotationData(PlacedObjectSaveData data)
    {
        Quaternion rotation = rotationDataObject.transform.rotation;
        data.rotX = rotation.x;
        data.rotY = rotation.y;
        data.rotZ = rotation.z;

        return data;
    }

    private PlacedObjectSaveData SetScaleData(PlacedObjectSaveData data)
    {
        Vector3 scale = scaleDataObject.transform.lossyScale;
        data.scaleX = scale.x;
        data.scaleY = scale.y;
        data.scaleZ = scale.z;

        return data;
    }

    private PlacedObjectSaveData SetAssetBundleData(PlacedObjectSaveData data)
    {
        data.assetBundleName = assetBundleObject.GetComponent<ALoadFromAssetBundle>().AssetBundleName;
        return data;
    }

    private PlacedObjectSaveData SetAssetNameData(PlacedObjectSaveData data)
    {
        data.assetName = assetBundleObject.GetComponent<ALoadFromAssetBundle>().AssetName;
        return data;
    }

    public void LoadData(PlacedObjectSaveData data)
    {
        NetworkLabInteractable networkObj = this.gameObject.GetComponent<NetworkLabInteractable>();

        if (networkObj == null)
        {
            this.gameObject.name = data.name;
            LoadPosition(data);
            LoadRotation(data);
            LoadScale(data);
            LoadAssetBundle(data);
            LoadAssetName(data);
        } else
        {
            this.gameObject.name = data.name;
            LoadPosition(data);
            LoadRotation(data);
            LoadScale(data);
            LoadAssetBundle(data);
            LoadAssetName(data);
            networkObj.LoadObjData(data);
        }
    }

    private void LoadPosition(PlacedObjectSaveData data)
    {
        Vector3 position = new Vector3(data.locX, data.locY, data.locZ);
        positionDataObject.transform.position = position;
    }

    private void LoadRotation(PlacedObjectSaveData data)
    {
        Vector3 rotation = new Vector3(data.rotX, data.rotY, data.rotZ);
        rotationDataObject.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    private void LoadScale(PlacedObjectSaveData data)
    {
        Vector3 scale = new Vector3(data.scaleX, data.scaleY, data.scaleZ);
        scaleDataObject.transform.localScale = scale;
    }

    private void LoadAssetBundle(PlacedObjectSaveData data)
    {
        assetBundleObject.GetComponent<ALoadFromAssetBundle>().AssetBundleName = data.assetBundleName;
    }

    private void LoadAssetName(PlacedObjectSaveData data)
    {

        assetBundleObject.GetComponent<ALoadFromAssetBundle>().AssetName = data.assetName;
        assetBundleObject.GetComponent<ALoadFromAssetBundle>().Reload();
    }
}
