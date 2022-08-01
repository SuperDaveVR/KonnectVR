using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LabComponentScreen : TabletScreenHandler
{
    [SerializeField] private GameObject ContentField;
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private bool LoadFromOnlineSource;
    [Tooltip("Only needed if loading from online source")]
    [SerializeField] private string OnlinePath; 

    // Start is called before the first frame update
    void Start()
    {
        Reload();

    }

    public void Reload()
    {
        Clear();
        LoadButtons();
    }

    private void Clear()
    {
        Debug.Log("Deleting from " + ContentField.name);
        foreach (Transform child in ContentField.transform)
        {
            Debug.Log("Deleting " + child.name);
            Destroy(child.gameObject);
        }
    }

    private void LoadButtons()
    {
        GameObject newButton;

        if (LoadFromOnlineSource)
        {
            //TODO: Online server loading
        }
        else
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");

            foreach (string f in Directory.GetFiles(filePath))
            {
                string extension = Path.GetExtension(f);
                if (extension == "" || extension == null)
                {
                    string fileName = Path.GetFileName(f);
                    Debug.Log("File name: " + fileName);

                    string bundleFilePath = System.IO.Path.Combine(filePath, fileName);

                    AssetBundleLoader loader = new AssetBundleLoader();
                    AssetBundle assetBundle = loader.AssetBundleLoad(fileName, bundleFilePath);

                    Debug.Log(assetBundle.GetAllAssetNames());

                    GameObject[] assetObjs = assetBundle.LoadAllAssets<GameObject>();

                    foreach (GameObject assetObj in assetObjs)
                    {
                        Debug.Log("Creating button for " + f);
                        newButton = Instantiate(ButtonPrefab, ContentField.transform);
                        PlaceObject placeObjectComp = newButton.GetComponent<PlaceObject>();
                        placeObjectComp.AssetBundleName = fileName;
                        placeObjectComp.DefaultName = assetObj.name;
                        placeObjectComp.HasBundle = true;
                        newButton.transform.Find("Text (TMP)").GetComponent<TMPro.TMP_Text>().text = assetObj.name;
                    }
                }
            }
        }
    }
}