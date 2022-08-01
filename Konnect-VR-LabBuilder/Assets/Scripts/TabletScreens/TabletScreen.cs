using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TabletScreen
{
    [SerializeField] private string name;
    [SerializeField] private GameObject screenObj;
    [SerializeField] private bool hideSideBar = false;
    [SerializeField] private bool highlightMenuButton;

    [Tooltip("Only needs to be filled if highlightMenuButton is true")]
    [SerializeField] private string menuButtonToHighlight;
    [SerializeField] private bool allowHistory;

    public TabletScreen()
    {

    }

    public TabletScreen(string name, GameObject screenObj, bool hideSideBar, string menuButtonToHighlight, bool allowHistory)
    {
        this.name = name;
        this.screenObj = screenObj;
        this.hideSideBar = hideSideBar;

        if (!string.IsNullOrEmpty(menuButtonToHighlight))
        {
            highlightMenuButton = true;
            this.menuButtonToHighlight = menuButtonToHighlight;
        }

        this.allowHistory = allowHistory;
    }
 
    public string Name
    {
        get
        {
            return name;
        }
    }

    public GameObject ScreenObj
    {
        get
        {
            return screenObj;
        }
    }

    public bool Exists
    {
        get
        {
            return (screenObj != null);
        }
    }

    public bool HideSideBar
    {
        get
        {
            return hideSideBar;
        }
    }

    public bool HighlightMenuButton {
        get {
            return highlightMenuButton;
        }
    }

    public string MenuButtonToHighlight
    {
        get
        {
            return menuButtonToHighlight;
        }
    }

    public bool AllowHistory
    {
        get
        {
            return allowHistory;
        }
    }

    public void SetActive(bool value)
    {
        screenObj.SetActive(value);
    }
}
