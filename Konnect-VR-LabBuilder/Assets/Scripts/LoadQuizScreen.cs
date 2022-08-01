using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadQuizScreen : TabletScreenHandler
{
    [SerializeField] private string defaultQuizSysName = "BuilderQuizSystem";
    [SerializeField] private QuizFileManager quizFileManager;

    void Awake()
    {
        if (quizFileManager == null)
        {
            quizFileManager = GameObject.Find(defaultQuizSysName).GetComponent<QuizFileManager>();
        }
    }

    public void LoadSetMode(string mode)
    {
        quizFileManager.SetMode(mode);

        if(mode != "Blank")
            quizFileManager.ActivateFileSelectionScreen("Load", ScreenName);
        else
        {
            quizFileManager.Load(null);
        }
    }
}
