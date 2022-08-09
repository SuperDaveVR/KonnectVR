using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuizAnswer
{
    string AnswerText
    {
        get;
        set;
    }

    bool IsSelected
    {
        get;
        set;
    }

    string EnteredAnswer
    {
        get;
        set;
    }

    string CorrectAnswer
    {
        get;
        set;
    }

    bool IsCorrect
    {
        get;
    }

    QuizAnswer DefaultAnswer();

    void toggleSelected();

    void DebugMe();
}
