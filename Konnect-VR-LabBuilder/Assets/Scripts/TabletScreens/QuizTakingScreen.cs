using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizTakingScreen : QuizScreen
{
    //Use this variable to interact with the active question that is currently meant to be visible.
    private QuizQuestion activeQuestion;

    [SerializeField] private TMPro.TMP_Text TitleText;

    //Prefabs for each quiz type
    [SerializeField] private GameObject MultipleChoicePrefab;
    [SerializeField] private GameObject PlacementPrefab;
    [SerializeField] private GameObject SelectionPrefab;

    private int currentQuestionNum = 0;

    protected override void Reload()
    {
        //Base Clear
        base.Clear();
        
        quizObj = activeQuiz.GetComponent<QuizObj>();

        //Enter additional methods you want to have run on reload here. 
        LoadTitle();
        LoadQuestion();

        //Base Reload
        base.Reload();
    }

    //Additional methods below
    private void LoadTitle()
    {
        TitleText.text = base.quizObj.Name;
    }

    private void LoadQuestion()
    {
        Debug.Log("Loading " + base.quizObj.Name);
        activeQuestion = base.quizObj.GetQuestion(currentQuestionNum);

        GameObject newQuestion;

        GameObject questionPrefab = GetQuizTypePrefab();

        if (questionPrefab != null) { 
            newQuestion = Instantiate(questionPrefab, base.ContentField.transform);
            newQuestion.GetComponent<QuizTakingQuestion>().BuildMe(activeQuestion, this); //Create a component in your Answer prefab and include a public BuildMe script that takes data from QuizQuestion.cs
        }
    }

    private GameObject GetQuizTypePrefab()
    {
        Type enumType = typeof(QuizObj.QuizTypes);
        string quizTypeString = base.quizObj.Type;

        if (Enum.IsDefined(enumType, quizTypeString))
        {
            QuizObj.QuizTypes quizType = (QuizObj.QuizTypes)Enum.Parse(enumType, quizTypeString);

            switch(quizType)
            {
                case QuizObj.QuizTypes.MultipleChoice:
                    {
                        Debug.Log("Multiple Choice Quiz");
                        return MultipleChoicePrefab;
                    }

                case QuizObj.QuizTypes.Placement:
                    {
                        Debug.Log("Placement Quiz");
                        return PlacementPrefab;
                    }

                case QuizObj.QuizTypes.ObjectSelection:
                    {
                        Debug.Log("Object Selection Quiz");
                        return SelectionPrefab;
                    }

                default:
                    {
                        return null;
                    }
            }
        }

        return null;
    }

    public void BackButton()
    {
        if (currentQuestionNum > 0)
        {
            currentQuestionNum--;
            Reload();
        }
    }

    public void NextButton()
    {
        if (currentQuestionNum < (base.quizObj.QuestionsList.Count -1))
        {
            currentQuestionNum++;
            Reload();
        }
    }

    public void SubmitButton()
    {
        //TODO: Quiz Submission Code!
    }
}
