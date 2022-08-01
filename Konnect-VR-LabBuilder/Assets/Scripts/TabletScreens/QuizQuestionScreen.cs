using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuizQuestionScreen : QuizScreen
{
    public QuizQuestion ActiveQuestion
    {
        get;
        set;
    }
}
