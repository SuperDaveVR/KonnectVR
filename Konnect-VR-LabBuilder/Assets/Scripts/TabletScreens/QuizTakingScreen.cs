using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizTakingScreen : QuizScreen
{
    //Use this variable to interact with the active question that is currently meant to be visible.
    private QuizQuestion activeQuestion;

    protected override void Reload()
    {
        base.Clear();
        quizObj = activeQuiz.GetComponent<QuizObj>();

        //Enter additional methods you want to have run on reload here. 
    }

    //Additional methods below
}
