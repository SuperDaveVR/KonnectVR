using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileNameInput : MonoBehaviour
{
    [SerializeField] FileSelectionScreen fileSelectionScreen;
    [SerializeField] GameObject InputField;

    public void Activate(FileSelectionScreen fileSelectionScreen, string defaultName)
    {
        this.gameObject.SetActive(true);
        this.fileSelectionScreen = fileSelectionScreen;
        InputField.GetComponent<TMPro.TMP_InputField>().text = defaultName;
    }

    public void SaveButton()
    {
        fileSelectionScreen.FileNameToSave = InputField.GetComponent<TMPro.TMP_InputField>().text;
        fileSelectionScreen.SaveButton();
        this.gameObject.SetActive(false);
    }
}
