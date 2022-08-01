using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizEditScreen : QuizScreen
{
    //Drag the prefab for questions here
    [SerializeField] private GameObject questionPrefab;
    [SerializeField] private TMPro.TMP_InputField QuizTitle;
    [SerializeField] private TMPro.TMP_Dropdown QuizType;

    private void LoadTitle()
    {
        QuizTitle.text = quizObj.Name;
    }

    private void LoadOptions()
    {
        QuizType.ClearOptions();

        List<TMPro.TMP_Dropdown.OptionData> OptionList = new List<TMPro.TMP_Dropdown.OptionData>();
        Type enumType = typeof(QuizObj.QuizTypes);

        for (int i = 0; i < Enum.GetNames(enumType).Length; i++)//Populate new Options
        {
            OptionList.Add(new TMPro.TMP_Dropdown.OptionData(Enum.GetName(enumType, i)));
        }

        QuizType.AddOptions(OptionList);

        QuizType.value = (int)Enum.Parse(enumType, quizObj.Type); 
    }

    private void LoadQuestions()
    {
        Debug.Log("Loading " + base.quizObj.Name);
        GameObject newQuestion;
          
        foreach (QuizQuestion question in quizObj.QuestionsList) {
            newQuestion = Instantiate(questionPrefab, base.ContentField.transform);
            newQuestion.GetComponent<QuizEditorQuestion>().BuildMe(question, this); //Create a component in your Answer prefab and include a public BuildMe script that takes data from QuizQuestion.cs
            //TODO: Enter your code for setting the edit button to target this question
        }
    }


    protected override void Reload()
    {
        base.Clear();
        quizObj = activeQuiz.GetComponent<QuizObj>();
        LoadTitle();
        LoadOptions();
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
        QuizAnswer answerDefault1 = new QuizAnswer("Option #1", true);
        QuizAnswer answerDefault2 = new QuizAnswer("Option #2", false);

        question.Answers.Add(answerDefault1);
        question.Answers.Add(answerDefault2);

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

    public void OnDropDownChanged(Int32 value)
    {
        QuizObj.QuizTypes enumValue = (QuizObj.QuizTypes)value;
        quizObj.Type = enumValue.ToString();
    }
}