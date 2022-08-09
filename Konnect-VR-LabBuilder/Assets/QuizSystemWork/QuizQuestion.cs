using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class QuizQuestion
{
    public enum QuestionTypes { MultipleChoice, ObjectSelection, Placement, Sequence }

    [SerializeField] private QuestionTypes questionType;
    [SerializeField] private string questionText;
    [SerializeField] private bool chooseMultiple;
    [SerializeField] private bool highlight;
    [SerializeField] private string highlightedObject;
    [SerializeField, SerializeReference] private List<QuizAnswer> answers;

    [SerializeField] private int pointValue;

    public string Type
    {
        get { return questionType.ToString(); }
        set
        {
            if (Enum.IsDefined(typeof(QuestionTypes), value))
                questionType = (QuestionTypes)Enum.Parse(typeof(QuestionTypes), value);
        }
    }
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
                /*if (answer.IsCorrect)
                    correctStr += answer.AnswerText + "|";*/
                correctStr += answer.CorrectTextValue();
            }
            return correctStr.Substring(0, correctStr.Length - 1);
        }
    }

    public int PointValue
    {
        get;
        set;
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
            if (answer.IsCorrect)
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
        questionType = SetQuestionType(question[0].ToString());
        questionText = question[1].ToString();
        chooseMultiple = StrToBool(question[2].ToString());
        highlight = StrToBool(question[3].ToString());
        SetHighlightObject(question[4].ToString());
        answers = BuildAnswersFromExcel(question[5].ToString(), question[6].ToString());
        pointValue = int.Parse(question[7].ToString());
    }

    private QuestionTypes SetQuestionType(string questionTypeStr)
    {
        QuestionTypes retType;

        string trimmed = String.Concat(questionTypeStr.Where(c => !Char.IsWhiteSpace(c)));
        switch (trimmed.ToLower())
        {
            case "multiplechoice":
                retType = QuestionTypes.MultipleChoice;
                break;
            case "objectselection":
                retType = QuestionTypes.ObjectSelection;
                break;
            case "placement":
                retType = QuestionTypes.Placement;
                break;
            case "sequence":
                retType = QuestionTypes.Sequence;
                break;
            default:
                retType = QuestionTypes.MultipleChoice;
                break;
        }

        return retType;
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
        //List<QuizAnswer> listOfAnswers = new List<QuizAnswer>();
        string[] answersArray = answersStr.Split('|');
        string[] correctAnswersArray = correctAnswersStr.Split('|');

        List<QuizAnswer> listOfAnswers = QuizAnswerTypeHandler.BuildAnswersOfType(questionType, answersArray, correctAnswersArray);

        return listOfAnswers;
    }

    public void AddDefaultAnswer(string answerText, string? correctText = "false")
    {
        answers.Add(QuizAnswerTypeHandler.BuildAnswerOfType(questionType, answerText, correctText));
    }

    public void ChangeQuizType(QuestionTypes type)
    {
        questionType = type;
        answers = QuizAnswerTypeHandler.SwapTypes(answers, type);
    }

    public void shuffleAnswers(System.Random random)
    {
        answers = answers.OrderBy(item => random.Next()).ToList();
    }
}
