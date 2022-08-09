using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizEditScreen : QuizScreen
{
    //Drag the prefab for questions here
    [SerializeField] private GameObject questionPrefab;
    [SerializeField] private TMPro.TMP_InputField QuizTitle;

    private void LoadTitle()
    {
        QuizTitle.text = quizObj.Name;
    }

    private void LoadQuestions()
    {
        Debug.Log("Loading " + base.quizObj.Name);
        GameObject newQuestion;
          
        foreach (QuizQuestion question in quizObj.QuestionsList) {
            newQuestion = Instantiate(questionPrefab, base.ContentField.transform);
            newQuestion.GetComponent<QuizEditorQuestion>().BuildMe(question, this); //Create a component in your Answer prefab and include a public BuildMe script that takes data from QuizQuestion.cs
        }
    }


    protected override void Reload()
    {
        base.Clear();
        quizObj = activeQuiz.GetComponent<QuizObj>();
        LoadTitle();
        LoadQuestions();
        base.Reload();
    }

    public void SaveButton()
    {
        quizFileManager.SelectQuiz(quizObj);
        quizFileManager.ActivateFileSelectionScreen("Save", ScreenName);
    }

    public void AddQuestionButton()
    {
        QuizQuestion question = new QuizQuestion("New Question");
        question.AddDefaultAnswer("Option #1", "true");
        question.AddDefaultAnswer("Option #2");

        quizObj.QuestionsList.Add(question);
        Reload();
    }

    public override void DeleteQuestionButton(QuizQuestion deletedQuestion)
    {
        base.DeleteQuestionButton(deletedQuestion);
        Reload();
    }

    public void DeleteQuizButton()
    {
        StartCoroutine(DeleteQuizCo());
    }

    private IEnumerator DeleteQuizCo()
    {
        Destroy(activeQuiz);

        while (activeQuiz != null)
            yield return null;

        base.PreviousScreen();
    }

    public void OnTitleChanged(string value)
    {
        quizObj.Name = value;
    }
}