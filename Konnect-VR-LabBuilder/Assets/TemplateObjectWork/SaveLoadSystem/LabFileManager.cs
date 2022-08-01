using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabFileManager : AFileManager
{
    [Header("Set to name of scene you want loader to load you into when a lab is loaded or 'this' if you want to stay in the same scene")]
    [SerializeField] private string sceneToLoad = "this";
    [SerializeField] private FileSelectionScreen fileSelectionScreen;
    [SerializeField] private string labName = "SavedLab";
    [SerializeField] private string compatibleExtensions = "*.dat";
    [SerializeField] private string defaultExtension = ".dat";

    private void Start()
    {
        if (fileSelectionScreen == null)
        {
            fileSelectionScreen = GameObject.FindGameObjectWithTag("FileSelectScreen").gameObject.GetComponent<FileSelectionScreen>();
        }
        //PlayerPrefs.DeleteAll();
        Debug.Log("Lab Loader Key exists:" + PlayerPrefs.HasKey("load_filePath"));
        Debug.Log("Lab File Manager Started!");
        if (PlayerPrefs.HasKey("load_filePath")) {
            Debug.Log("Loading: " + PlayerPrefs.GetString("load_filePath"));
            string filePath = PlayerPrefs.GetString("load_filePath");
            Load(filePath);
        }
    }

    public string SceneToLoad
    {
        set
        {
            sceneToLoad = value;
        }
    }

    public override void Load(string filePath)
    {
        if (sceneToLoad == "this")
        {
            base.saveSerial.LoadLab(filePath);
            labName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            if (PlayerPrefs.HasKey("load_filePath"))
            {
                PlayerPrefs.DeleteKey("load_filePath");
                Debug.Log("Lab Loader Key (should be false)" + PlayerPrefs.HasKey("load_filePath"));
            }
        }
        else if(LoadingSceneManager.Instance)
        {
            PlayerPrefs.SetString("load_filePath", filePath);
            LoadingSceneManager.Instance.SceneToLoad = sceneToLoad;
            UnityEngine.SceneManagement.SceneManager.LoadScene(LoadingSceneManager.Instance.LoadingScreen);
        }
        else
        {
            PlayerPrefs.SetString("load_filePath", filePath);
            Debug.Log("Player Prefs set: " + PlayerPrefs.GetString("load_filePath"));
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
        }
    }

    public override void Save(string filePath)
    {
        base.saveSerial.SaveLab(filePath);
    }

    public void ActivateFileSelectionScreen(string mode)
    {
        string name = labName;
        fileSelectionScreen.Activate(this, mode, compatibleExtensions, defaultExtension, name);
    }

    public void ActivateFileSelectionScreen(string mode, string screenName)
    {
        string name = labName;
        fileSelectionScreen.Activate(this, mode, compatibleExtensions, defaultExtension, name, screenName);
    }
}
