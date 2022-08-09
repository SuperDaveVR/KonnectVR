using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class QuizAnswer : IQuizAnswer
{
    [SerializeField, SerializeReference] string answerText;
    [SerializeField, SerializeReference] string correctAnswer;

    public string AnswerText
    {
        get { return answerText; }
        set { 
                answerText = value; 
        }
    }
    public bool IsSelected { get; set; }
    public bool IsCorrect
    {
        get
        {
            return EnteredAnswer == CorrectAnswer;
        }
    }
    public string EnteredAnswer { get; set; }
    public string CorrectAnswer
    {
        get { return correctAnswer; }
        set { if (ValidateAnswer(value)) 
                correctAnswer = value; 
        }
    }

    public abstract string CorrectTextValue();

    public void DebugMe()
    {
        Debug.Log("Answer: " + AnswerText + " - Correct Answer: " + CorrectAnswer);
    }

    public abstract QuizAnswer DefaultAnswer();

    public abstract bool ValidateAnswer(string value);

    public void toggleSelected()
    {
        IsSelected = !IsSelected;
    }

    public bool AnswerObjectExists()
    {
        bool exists = PlacedObjectsHandler.Instance.CheckObjectExists(answerText);

        return exists;
    }
}
