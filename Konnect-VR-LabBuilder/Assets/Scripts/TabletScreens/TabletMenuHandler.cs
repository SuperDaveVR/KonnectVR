using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabletMenuHandler : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _TitleText;
    [SerializeField] private string HomeMenu = "Home";
    [SerializeField] private GameObject SidePanel;
    [SerializeField] private List<GameObject> MenuButtons;

    [SerializeField] private Color defaultButtonColor;
    [SerializeField] private Color highlightButtonColor;

    [Tooltip("Drag all screens to this list")]
    [SerializeField] private List<TabletScreenHandler> ScreenList;
    [SerializeField] private List<TabletScreen> TabletScreens;
    [SerializeField] private TabletScreen activeScreen;
    [SerializeField] private mfitzer.Interactions.Keyboard TabletKeyboard;
    private TabletScreen savedScreen;

    private Button highlightedButton;

    //Back and Forward Stacks
    [Tooltip("Max size of back/forward stack")]
    [SerializeField] private int maxHistory = 20;
    private LimitedStack<TabletScreen> BackScreens;
    private LimitedStack<TabletScreen> ForwardScreens;

    [SerializeField] private Button backButton;
    [SerializeField] private Button forwardButton;

    public ErrorScreen ErrorScreen;

    public string TitleText
    {
        set
        {
            _TitleText.text = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BuildTabletScreens();
        SetErrorScreen();
        SetKeyboard();
        BackScreens = new LimitedStack<TabletScreen>(maxHistory);
        ForwardScreens = new LimitedStack<TabletScreen>(maxHistory);
        GoHome();
        ButtonVisibility(backButton, BackScreens);
        ButtonVisibility(forwardButton, ForwardScreens);
    }

    private void SetKeyboard()
    {
        if (TabletKeyboard)
        {
            mfitzer.Interactions.Keyboard.Instance = TabletKeyboard;
        }
    }

    private void SetErrorScreen()
    {
        if (ErrorManager.Instance)
            ErrorManager.Instance.ErrorScreen = ErrorScreen;
    }

    private void BuildTabletScreens()
    {
        foreach (TabletScreenHandler screen in ScreenList)
        {
            TabletScreens.Add(screen.BuildTabletScreen(this));
        }
    }

    public void GoHome()
    {
        GoToScreen(HomeMenu);
    }

    private void SwapScreen(TabletScreen swapScreen)
    {
        if (activeScreen != null && activeScreen.Exists)
            activeScreen.SetActive(false);

        swapScreen.SetActive(true);

        SetMenuOptions(swapScreen);

        activeScreen = swapScreen;
    }

    private void AddBackScreen(TabletScreen backScreen)
    {
        BackScreens.Push(backScreen);
        ButtonVisibility(backButton, BackScreens);
    }

    private void AddForwardScreen (TabletScreen forwardScreen)
    {
        ForwardScreens.Push(forwardScreen);
        ButtonVisibility(forwardButton, ForwardScreens);
    }

    public void SaveCurrentScreen()
    {
        if (activeScreen != null && activeScreen.Exists)
            savedScreen = activeScreen;
    }

    public void GoToSaved()
    {
        GoToScreen(savedScreen.Name);
    }

    public void ToggleScreen(string goScreen)
    {
        if (activeScreen.Name == goScreen)
        {
            GoToSaved();
        } else
        {
            SaveCurrentScreen();
            GoToScreen(goScreen);
        }
    }

    public void GoToScreen(string goScreen)
    {
        if(activeScreen != null && activeScreen.Exists && activeScreen.AllowHistory)
            AddBackScreen(activeScreen);
        SwapScreen(FindScreenByName(goScreen));

        ForwardScreens.Clear();
        ButtonVisibility(forwardButton, ForwardScreens);
    }

    public void GoToScreenNoSave(string goScreen)
    {
        SwapScreen(FindScreenByName(goScreen));
        ForwardScreens.Clear();
        ButtonVisibility(forwardButton, ForwardScreens);
    }

    public void GoBack()
    {
        TabletScreen backScreen = BackScreens.Pop();
        if(activeScreen.AllowHistory)
            AddForwardScreen(activeScreen);
        SwapScreen(backScreen);
        ButtonVisibility(backButton, BackScreens);
    }

    public void GoForward()
    {
        TabletScreen forwardScreen = ForwardScreens.Pop();
        if (activeScreen.AllowHistory)
            AddBackScreen(activeScreen);
        SwapScreen(forwardScreen);
        ButtonVisibility(forwardButton, ForwardScreens);
    }

    private void ToggleSidePanel(bool value)
    {
        SidePanel.SetActive(value);
    }

    private void ChangeHighlightButton()
    {
        if (highlightedButton)
            UnhighlightButton(highlightedButton);
        highlightedButton = null;
    }

    private void ChangeHighlightButton(string name)
    {
        Button buttonObj = FindMenuButton(name);

        //Toggle Highlights
        if (highlightedButton)
            UnhighlightButton(highlightedButton);
        HighlightButton(buttonObj);

        highlightedButton = buttonObj;
    }

    private void UnhighlightButton(Button button)
    {
        ColorBlock cb = button.colors;

        cb.normalColor = defaultButtonColor;

        button.colors = cb;
    }

    private void HighlightButton(Button button)
    {
        ColorBlock cb = button.colors;

        cb.normalColor = highlightButtonColor;

        button.colors = cb;
    }

    private Button FindMenuButton(string name)
    {
        GameObject buttonToHighlight;
        Button buttonObj = null;

        for (int i = 0; i < MenuButtons.Count; i++)
        {
            if (MenuButtons[i].name.ToLower() == name.ToLower())
            {
                buttonToHighlight = MenuButtons[i];
                buttonObj = buttonToHighlight.GetComponent<Button>();
                break;
            }
        }

        return buttonObj;
    }

    public TabletScreen FindScreenByName(string screenName)
    {
        TabletScreen tabletScreen = TabletScreens.Find(x => x.Name.ToLower().Equals(screenName.ToLower()));
        return tabletScreen;
    }

    private void SetMenuOptions(TabletScreen tabletScreen)
    {
        ToggleSidePanel(!tabletScreen.HideSideBar);
        TitleText = tabletScreen.Name.ToUpper();

        if (tabletScreen.HighlightMenuButton)
        {
            ChangeHighlightButton(tabletScreen.MenuButtonToHighlight);
        } else
        {
            ChangeHighlightButton();
        }
    }

    public void AddTabletScreen(TabletScreen tabletScreen)
    {
        TabletScreens.Add(tabletScreen);
    }

    private void ButtonVisibility(Button button, LimitedStack<TabletScreen> tabletScreenStack)
    {
        int screenCount = tabletScreenStack.Count;
        Debug.Log("Screen Count: " + screenCount);
        button.interactable = (screenCount > 0);
    }
}
