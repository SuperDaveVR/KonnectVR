using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FileSelectionScreen : TabletScreenHandler
{
    [SerializeField] private string FilePath;
    [SerializeField] private string fileNameToSave = "Test";
    [SerializeField] private string saveExtension = ".xlsb";
    [SerializeField] private TMPro.TextMeshProUGUI FilePathText;
    [SerializeField] private GameObject ContentField;
    [SerializeField] private GameObject FolderButtonPrefab;
    [SerializeField] private GameObject FileButtonPrefab;
    [SerializeField] public string SelectedFile;
    [SerializeField] private string extensions;

    [SerializeField] private AFileManager fileManager;
    [SerializeField] private GameObject[] loadButtons;
    [SerializeField] private GameObject[] saveButtons;

    [SerializeField] private AlertBox AlertScreen;
    [SerializeField] private FileNameInput fileNameInput;

    [SerializeField] private string mode;

    private string PreviousScreen;
    public string Extensions
    {
        set
        {
            extensions = value;
            Reload();
        }
    }

    public string FileNameToSave
    {
        set
        {
            fileNameToSave = value;
        }
    }

    public string SaveExtension
    {
        set
        {
            saveExtension = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FilePath = Application.persistentDataPath;
        //fileManager = new DebugFileManager();
        Reload();
    }

    void Reload()
    {
        Clear();
        if (!String.IsNullOrEmpty(FilePath))
        {
            SetFilePathField();
            LoadFolders();
            LoadFiles();
            SelectedFile = "";
        }
    }

    public void Activate(AFileManager fileManager, string mode, string extensions, string saveExtension, string name)
    {

        this.fileManager = fileManager;
        if (base.TabletMenuHandler)
            base.TabletMenuHandler.GoToScreen(base.ScreenName);
        else
            this.gameObject.SetActive(true);
        this.extensions = extensions;
        this.saveExtension = saveExtension;
        this.fileNameToSave = name;

        if (mode.ToLower() == "save")
        {
            SaveMode();
        }
        else if (mode.ToLower() == "load")
        {
            LoadMode();
        }
    }

    public void Activate(AFileManager fileManager, string mode, string extensions, string saveExtension, string name, string previousScreen)
    {
        PreviousScreen = previousScreen;

        Activate(fileManager, mode, extensions, saveExtension, name);
    }

    public void LoadMode()
    {
        mode = "load";
        Debug.Log("Load Mode");
        foreach (GameObject button in loadButtons)
        {
            Debug.Log("Activating " + button.name);
            button.SetActive(true);
        }

        foreach (GameObject button in saveButtons)
        {
            Debug.Log("Deactivating " + button.name);
            button.SetActive(false);
        }

        Reload();
    }

    public void SaveMode()
    {
        mode = "save";
        Debug.Log("Save Mode");
        foreach (GameObject button in loadButtons)
        {
            Debug.Log("Deactivating " + button.name);
            button.SetActive(false);
        }

        foreach (GameObject button in saveButtons)
        {
            Debug.Log("Activating " + button.name);
            button.SetActive(true);
        }

        Reload();
    }

    public void SetFileManager(AFileManager fileManager)
    {
        this.fileManager = fileManager;
    }

    public void SetFilePathField() {
        FilePathText.text = FilePath;
    }

    public void GoToPath(string newPath)
    {
        if (newPath != null)
        {
            FilePath = newPath;
            Reload();
        }
    }

    public void OneLevelUp()
    {
        string newPath = Path.GetFullPath(FilePath + "/../");
        
        if (Directory.Exists(newPath))
        {
            FilePath = newPath;
            Reload();
        }
    }

    private void LoadFolders()
    {
        string[] folders = Directory.GetDirectories(FilePath);

        GameObject newButton;
        foreach (string f in folders)
        {
            Debug.Log("Creating button for " + f);
            newButton = Instantiate(FolderButtonPrefab, ContentField.transform);
            newButton.transform.Find("Text").GetComponent<Text>().text = Path.GetFileName(f);

            FolderButton folderButton = newButton.transform.GetComponent<FolderButton>();
            folderButton.FileSelectScreen = this;
            folderButton.FilePath = f;
        }
    }

    private void LoadFiles()
    {
        GameObject newButton;

        foreach (string f in Directory.GetFiles(FilePath, "*.*").Where(f => extensions.Contains(Path.GetExtension(f).ToLower())))
        {
            Debug.Log("Creating button for " + f);
            newButton = Instantiate(FileButtonPrefab, ContentField.transform);
            newButton.transform.Find("Text").GetComponent<Text>().text = Path.GetFileName(f);

            FileButton fileButton = newButton.transform.GetComponent<FileButton>();
            fileButton.FileSelectScreen = this;
            fileButton.FilePath = f;
        }
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

    public void LoadFile(string filePath)
    {
        if (File.Exists(SelectedFile))
        {
            fileManager.Load(SelectedFile);
            AlertScreen.triggerAlert(SelectedFile + " has been loaded!", this.gameObject, true);
        }
    }

    public void LoadButton()
    {
        if (File.Exists(SelectedFile))
        {
            fileManager.Load(SelectedFile);
            AlertScreen.triggerAlert(SelectedFile + " has been loaded!", this.gameObject, true);
        }
        else if (Directory.Exists(SelectedFile))
        {
            GoToPath(SelectedFile);
        }
    } 

    public void SaveButton()
    {
        if (Directory.Exists(SelectedFile))
        {
            GoToPath(SelectedFile);
        }
        else
        {
            string savePath = FilePath + "/" + fileNameToSave + saveExtension;
            fileManager.Save(savePath);
            AlertScreen.triggerAlert(savePath + " has been saved!", this.gameObject, true);
            Reload();
        }
    }

    public void SaveAsButton()
    {
        fileNameInput.Activate(this, fileNameToSave);
    }

    public void FileButtonDClick(string filePath)
    {
        if (mode.ToLower() == "save")
        {
            fileNameToSave = Path.GetFileNameWithoutExtension(filePath);
            SaveButton();
        }
        else if (mode.ToLower() == "load")
        {
            SelectedFile = filePath;
            LoadButton();
        }
    }

    public void CancelButton()
    {
        if (PreviousScreen != null)
            GoToScreenNoSave(PreviousScreen);
    }
}
