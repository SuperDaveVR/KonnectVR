using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALoadFromAssetBundle : MonoBehaviour
{
    [SerializeField] private string assetBundleName = "testbundle";
    [SerializeField] private string assetName = "Cube";
    [SerializeField] protected GameObject graphicObject;
    [SerializeField] private int LayerNum = 31;

    [SerializeField] private string ConnectorTag = "Connector";

    public string AssetBundleName
    {
        get
        {
            return assetBundleName;
        }
        set
        {
            assetBundleName = value;
        }
    }

    public string AssetName
    {
        get
        {
            return assetName;
        }

        set
        {
            assetName = value;
        }
    }

    public GameObject GraphicObject
    {
        get
        {
            return graphicObject;
        }
        set
        {
            graphicObject = value;
        }
    }

    void Start()
    {
        Reload();
    }

    public void Reload()
    {
        StartCoroutine(LoadAsset(assetBundleName, assetName));
    }

    IEnumerator LoadAsset(string assetBundleName, string objectNameToLoad)
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");
        filePath = System.IO.Path.Combine(filePath, assetBundleName);

        //Check Loaded Bundles
        var loadedBundles = AssetBundle.GetAllLoadedAssetBundles();

        //Load AssetBundle
        //var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);
        //yield return assetBundleCreateRequest;

        //AssetBundle assetBundle = assetBundleCreateRequest.assetBundle;

        AssetBundleLoader loader = new AssetBundleLoader();
        AssetBundle assetBundle = loader.AssetBundleLoad(assetBundleName, filePath);

        //Load Materials
        var materials = assetBundle.LoadAllAssets<Material>();
        foreach (Material m in materials)
        {
            var shaderName = m.shader.name;
            var newShader = Shader.Find(shaderName);
            if (newShader != null)
            {
                m.shader = newShader;
            }
            else
            {
                Debug.LogWarning("unable to refresh shader: " + shaderName + " in material " + m.name);
            }
        }

        //Load the Asset
        AssetBundleRequest asset = assetBundle.LoadAssetAsync<GameObject>(objectNameToLoad);
        yield return asset;

        //Destroy default object
        Destroy(GraphicObject);

        //Retrieve the object
        GameObject loadedAsset = asset.asset as GameObject;

        //Set layer
        SetLayer(loadedAsset);

        //Do something with the loaded loadedAsset  object (Load to RawImage for example) 
        var loadedObj = Instantiate(loadedAsset, gameObject.transform);
        //CheckChildrenTags(loadedObj);
        AddComponents(loadedObj);

        //Unload assetBundle
        //assetBundle.Unload(false);
    }

    public abstract void AddComponents(GameObject loadedObj);

    //Set Layer
    private void SetLayer(GameObject loadedObj)
    {
        loadedObj.layer = LayerNum;
    }

    //Check the Tags of Children Objects
    private void CheckChildrenTags(GameObject loadedObj)
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {
            if (child.tag == ConnectorTag)
            {
                child.gameObject.AddComponent<Connector>();
            }
        }

    }
}
