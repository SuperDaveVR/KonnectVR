using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletScreenHandler : MonoBehaviour
{
    [SerializeField] private string tabletIntegratedMenuName = "TabletIntegratedMenu";
    [SerializeField] private TabletMenuHandler tabletMenuHandler;

    [SerializeField] private string screenName;
    [SerializeField] private bool hideSideBar = false;
    [Tooltip("Optional")]
    [SerializeField] private string menuButtonToHighlight;
    [SerializeField] private bool allowHistory = true;

    [TextArea]
    [Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notes = "This component handles basic screen changing functionality for the tablet. When adding a new screen, make sure to drag the object to the TabletMenuHandler component's screen list. " +
        "There is no need to add the TabletMenuHandler manually as this is handled in code.";

    public TabletMenuHandler TabletMenuHandler
    {
        get
        {
            return tabletMenuHandler;
        }
    }

    public string ScreenName
    {
        get
        {
            return screenName;
        }
    }

    public void SaveCurrentScreen()
    {
        tabletMenuHandler.SaveCurrentScreen();
    }

    public void GoToScreen(string goScreen)
    {
        tabletMenuHandler.GoToScreen(goScreen);
    }

    public void GoToScreenNoSave(string goScreen)
    {
        tabletMenuHandler.GoToScreenNoSave(goScreen);
    }

    public void GoToSaved()
    {
        tabletMenuHandler.GoToSaved();
    }

    public GameObject GetScreen(string screenName)
    {
        TabletScreen tabletScreen = tabletMenuHandler.FindScreenByName(screenName);

        return tabletScreen.ScreenObj;
    }

    public TabletScreen BuildTabletScreen(TabletMenuHandler menu)
    {
        tabletMenuHandler = menu;
        TabletScreen tabletScreen = new TabletScreen(screenName,this.gameObject, hideSideBar, menuButtonToHighlight, allowHistory);
        return tabletScreen;
    }

    public void InputClicked(TMPro.TMP_InputField inputField)
    {
        if (mfitzer.Interactions.Keyboard.Instance)
            mfitzer.Interactions.Keyboard.Instance.ActivateKeyboard(inputField);
    }

    public void InputExited()
    {
        if (mfitzer.Interactions.Keyboard.Instance)
            mfitzer.Interactions.Keyboard.Instance.DeactivateKeyboard();
    }
}
