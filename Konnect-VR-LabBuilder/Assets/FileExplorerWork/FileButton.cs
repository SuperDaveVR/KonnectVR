using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileButton : MonoBehaviour
{
    [SerializeField] private FileSelectionScreen fileSelectScreen;
    [SerializeField] private string filePath;

    private bool clickedOnce = false;
    
    public string FilePath
    {
        get
        {
            return filePath;
        }

        set
        {
            filePath = value;
        }
    }

    public FileSelectionScreen FileSelectScreen
    {

        set
        {
            fileSelectScreen = value;
        }
    }

    public void OnFileButtonPress()
    {
        if (fileSelectScreen.SelectedFile == filePath && clickedOnce)
        {
            fileSelectScreen.FileButtonDClick(filePath);
            fileSelectScreen.SelectedFile = "";
        }
        else
        {
            fileSelectScreen.SelectedFile = filePath;
            StartCoroutine(DoubleClickCheck());
        }
    }

    IEnumerator DoubleClickCheck()
    {
        clickedOnce = true;
        yield return new WaitForSeconds(.5f);
        clickedOnce = false;
    }
}
