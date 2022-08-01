using System;

[Serializable]
public class PlacedObjectSaveData
{
    //Name
    public string name;

    //Location Data
    public float locX, locY, locZ;

    //Rotation Data
    public float rotX, rotY, rotZ;

    //Scale Data
    public float scaleX, scaleY, scaleZ;

    //AssetBundle Data
    public string assetBundleName;

    //AssetName Data
    public string assetName;

    //Asset Bundle Reference
    //Unknown if will be used. Anticipating needing a reference to the asset bundle to load mesh and material.
    //public string assetBundleName
}
