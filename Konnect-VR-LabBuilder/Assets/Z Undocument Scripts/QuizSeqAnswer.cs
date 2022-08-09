using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class QuizSeqAnswer : QuizAnswer
{
    public QuizSeqAnswer(string answerText, int orderNum)
    {
        AnswerText = answerText;
        IsSelected = false;
        CorrectAnswer = orderNum.ToString();
    }

    public override string CorrectTextValue()
    {
        return CorrectAnswer + "|";
    }

    public override QuizAnswer DefaultAnswer()
    {
        AnswerText = "New Answer";
        IsSelected = false;
        CorrectAnswer = "0";

        return this;
    }

    public override bool ValidateAnswer(string value)
    {
        int i;
        bool canConvert = int.TryParse(value, out i);

        if (canConvert)
        {
            return true;
        }
        else
        {
            ErrorManager.Instance.ThrowError("Invalid value: '" + value + "'. Answer value must be an integer value!", true);
        }

        return false;
    }
}
