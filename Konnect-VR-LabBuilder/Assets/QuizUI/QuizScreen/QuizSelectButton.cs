using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizSelectButton : MonoBehaviour
{
    [SerializeField] private GameObject QuizGameObj;
    [SerializeField] private string ScreenToOpen;
    [SerializeField] private TabletScreenHandler TabletScreenHandler;

    public void BuildMe(GameObject QuizGameObj, string ScreenToOpen, TabletScreenHandler tabletScreenHandler)
    {
        this.QuizGameObj = QuizGameObj;
        this.ScreenToOpen = ScreenToOpen;
        this.TabletScreenHandler = tabletScreenHandler;
    }

    public void OnButtonClick()
    {
        OpenScreen();
    }

    private void OpenScreen()
    {
        //TODO: Send QuizGameObj object reference to screen
        TabletScreenHandler.GetScreen(ScreenToOpen).GetComponent<QuizScreen>().ActiveQuiz = QuizGameObj;
        TabletScreenHandler.SaveCurrentScreen();
        TabletScreenHandler.GoToScreen(ScreenToOpen);
    }
}
