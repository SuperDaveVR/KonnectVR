using System.Collections.Generic;
using UnityEngine;

public class AssetBundleLoader
{
    private static Dictionary<string, AssetBundle> loadedAssetBundleMap = new Dictionary<string, AssetBundle>();
    private string assetBundleName;
    private string filePath;

    public AssetBundle AssetBundleLoad(string assetBundleName, string filePath)
    {
        this.assetBundleName = assetBundleName;
        this.filePath = filePath;
        return AssetBundle;
    }

    private static AssetBundle GetAssetBundle(string assetBundleName)
    {
        foreach (var kv in loadedAssetBundleMap)
            if (kv.Key == assetBundleName)
                return kv.Value;
        return null;
    }

    private AssetBundle AssetBundle
    {
        get
        {
            string assetBundleName = this.assetBundleName;
            AssetBundle bundle = GetAssetBundle(assetBundleName);

            if (bundle == null)
            {
                bundle = AssetBundle.LoadFromFile(filePath);
                loadedAssetBundleMap[assetBundleName] = bundle;
            }
            return bundle;
        }
    }
}
