using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetBundle : MonoBehaviour
{
    AssetBundle myLoadedAssetbundle;
    public string path;
    public string objectName;

    // Start is called before the first frame update
    void Start()
    {
        LoadMyAssetBundle(path);
        InstantiateObjectFromBundle(objectName);
    }

    void LoadMyAssetBundle(string bundleUrl)
    {
        myLoadedAssetbundle = AssetBundle.LoadFromFile(bundleUrl);

        Debug.Log(myLoadedAssetbundle == null ? " Failed to load AssetBundle" : " Assetbundle successfully loaded");
    }

    void InstantiateObjectFromBundle(string assetName)
    {
        var prefab = myLoadedAssetbundle.LoadAsset(assetName);
        Instantiate(prefab);
    }
}
