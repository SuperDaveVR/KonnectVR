using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLabScreen : TabletScreenHandler
{
    [SerializeField] private LabFileManager labFileManager;

    public void Save()
    {
        base.TabletMenuHandler.SaveCurrentScreen();
        labFileManager.ActivateFileSelectionScreen("Save", ScreenName);
    }

    public void Load()
    {
        base.TabletMenuHandler.SaveCurrentScreen();
        labFileManager.ActivateFileSelectionScreen("Load", ScreenName);
    }
}
