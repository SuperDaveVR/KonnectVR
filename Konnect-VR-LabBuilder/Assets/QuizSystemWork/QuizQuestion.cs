using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class QuizQuestion
{
    [SerializeField] private string questionText;
    [SerializeField] private bool chooseMultiple;
    [SerializeField] private bool highlight;
    [SerializeField] private string highlightedObject;
    [SerializeField] private List<QuizAnswer> answers;

    public string Text { 
        get { return questionText; } 
        set { questionText = value; }
    }
    public bool ChooseMultiple { 
        get { return chooseMultiple; }
        set { chooseMultiple = value; }
    }
    public bool Highlight { 
        get { return highlight; } 
        set { highlight = value; }
    }
    public List<QuizAnswer> Answers{get {return answers; }}

    public string HighlightedObjName {
        get {
            return highlightedObject;
        }
        set { highlightedObject = value; }
    }

    public string OptionsText
    {
        get
        {
            string optionsStr = "";
            foreach (QuizAnswer answer in answers)
            {
                optionsStr += answer.AnswerText + "|";
            }
            return optionsStr.Substring(0, optionsStr.Length - 1);
        }
    }

    public string CorrectText
    {
        get
        {
            string correctStr = "";
            foreach (QuizAnswer answer in answers)
            {
                if (answer.IsCorrect)
                    correctStr += answer.AnswerText + "|";
            }
            return correctStr.Substring(0, correctStr.Length - 1);
        }
    }

    public QuizQuestion()
    { 
    }

    public QuizQuestion(string text)
    {
        questionText = text;
        chooseMultiple = false;
        highlight = false;
        SetHighlightObject("");
        answers = new List<QuizAnswer>();
    }

    public void SelectAnswer(int selectedOption)
    {
        answers[selectedOption].toggleSelected();

        if (!chooseMultiple)
        {
            foreach (QuizAnswer answer in answers)
            {
                if (answer != answers[selectedOption])
                    answer.IsSelected = false;
            }
        }

    }

    public float CheckIfCorrect()
    {
        float amountCorrect = 0;
        int howManyAnswers = answers.Count;

        foreach (QuizAnswer answer in answers)
        {
            if (answer.IsCorrect == answer.IsSelected)
            {
                amountCorrect++;
            }
        }

        return amountCorrect;
    }

    public void SetHighlightObject(string objectName)
    {
        if (highlight && !string.IsNullOrEmpty(objectName) )
            highlightedObject = objectName;
    }

    public void BuildFromExcel(DataRow question)
    {
        questionText = question[0].ToString();
        chooseMultiple = StrToBool(question[1].ToString());
        highlight = StrToBool(question[2].ToString());
        SetHighlightObject(question[3].ToString());
        answers = BuildAnswersFromExcel(question[4].ToString(), question[5].ToString());
    }

    private bool StrToBool(string boolString)
    {
        bool retBool;

        string trimmed = String.Concat(boolString.Where(c => !Char.IsWhiteSpace(c)));

        retBool = trimmed.ToLower().Equals("true");

        return retBool;
    }

    private List<QuizAnswer> BuildAnswersFromExcel(string answersStr, string correctAnswersStr)
    {
        List<QuizAnswer> listOfAnswers = new List<QuizAnswer>();
        string[] answersArray = answersStr.Split('|');
        string[] correctAnswersArray = correctAnswersStr.Split('|');


        for (int i = 0; i < answersArray.Length; i++)
        {
            //Remove starting and ending whitespace
            answersArray[i] = answersArray[i].TrimStart(' ');
            answersArray[i] = answersArray[i].TrimEnd(' ');
        }

        for (int i = 0; i < correctAnswersArray.Length; i++)
        {
            //Remove starting and ending whitespace
            correctAnswersArray[i] = correctAnswersArray[i].TrimStart(' ');
            correctAnswersArray[i] = correctAnswersArray[i].TrimEnd(' ');
        }

        foreach (string answer in answersArray)
        {
            bool isCorrect = false;

            if (correctAnswersArray.Contains(answer))
            {
                isCorrect = true;
            }

            QuizAnswer quizAnswer = new QuizAnswer(answer, isCorrect);
            listOfAnswers.Add(quizAnswer);
        }

        return listOfAnswers;
    }

    public void shuffleAnswers(System.Random random)
    {
        answers = answers.OrderBy(item => random.Next()).ToList();
    }
}
