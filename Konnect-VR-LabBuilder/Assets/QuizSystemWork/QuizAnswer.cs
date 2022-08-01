using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizAnswer
{
    [SerializeField] private string answerText;
    [SerializeField] private bool isSelected;
    [SerializeField] private bool isCorrect;

    public string AnswerText
    {
        get
        {
            return answerText;
        }

        set
        {
            answerText = value;
        }
    }

    public bool IsSelected
    {
        get
        {
            return isSelected;
        }

        set
        {
            isSelected = value;
        }
    }

    public bool IsCorrect
    {
        get
        {
            return isCorrect;
        }

        set
        {
            isCorrect = value;
        }
    }

    public QuizAnswer(string answerText, bool isCorrect)
    {
        this.answerText = answerText;
        this.isSelected = false;
        this.isCorrect = isCorrect;
    }

    public void toggleSelected()
    {
        isSelected = !isSelected;
    }

    public void DebugMe()
    {
        Debug.Log("Answer: " + answerText + " - IsCorrect?: " + isCorrect.ToString());
    }
}
