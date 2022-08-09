using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(QuizAnswerPrefabLoader))]
abstract public class QuizEditorQuestion : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown QuestionType;

    [SerializeField] protected GameObject answerContainer;
    [SerializeField] protected QuizAnswerPrefabLoader answerLoader;

    [Header("No entry required for targetQuestion, visible in editor for debug purposes.")]
    [SerializeField] protected QuizQuestion targetQuestion;

    [SerializeField] protected Toggle multipleAnswersToggle;
    [SerializeField] protected Toggle highlightObjectToggle;
    [SerializeField] protected TMPro.TMP_InputField targetObjectInput;

    protected QuizScreen quizScreen;

    public void BuildMe(QuizQuestion question, QuizScreen quizScreen)
    {
        answerLoader = GetComponent<QuizAnswerPrefabLoader>();
        targetQuestion = question;
        this.quizScreen = quizScreen;

        if (targetQuestion != null)
        {
            LoadOptions();
            LoadQuestionText();
            LoadChooseMultiple();
            LoadHighlight();
            LoadAnswers();
        }
    }
    private void LoadOptions()
    {
        QuestionType.ClearOptions();

        List<TMPro.TMP_Dropdown.OptionData> OptionList = new List<TMPro.TMP_Dropdown.OptionData>();
        Type enumType = typeof(QuizQuestion.QuestionTypes);

        for (int i = 0; i < Enum.GetNames(enumType).Length; i++)//Populate new Options
        {
            OptionList.Add(new TMPro.TMP_Dropdown.OptionData(Enum.GetName(enumType, i)));
        }

        QuestionType.AddOptions(OptionList);

        QuestionType.value = (int)Enum.Parse(enumType, targetQuestion.Type);
    }

    public void OnDropDownChanged(Int32 value)
    {
        QuizQuestion.QuestionTypes enumValue = (QuizQuestion.QuestionTypes)value;
        targetQuestion.ChangeQuizType(enumValue);
        LoadAnswers();
    }

    protected abstract void LoadQuestionText();

    protected abstract void LoadChooseMultiple();

    protected abstract void LoadHighlight();

    private void LoadAnswers()
    {
        ClearAnswers();

        Type enumType = typeof(QuizQuestion.QuestionTypes);
        GameObject answerPrefab = answerLoader.GetPrefabType((QuizQuestion.QuestionTypes)Enum.Parse(enumType, targetQuestion.Type));

        GameObject newAnswer;
        foreach (QuizAnswer answer in targetQuestion.Answers)
        {
            newAnswer = Instantiate(answerPrefab, answerContainer.transform);
            newAnswer.GetComponent<QuizEditorAnswer>().BuildMe(answer, answerContainer); //Create a component in your Answer prefab and include a public BuildMe script that takes data from QuizAnswer.cs
        }
    }

    private void ClearAnswers()
    {
        foreach (Transform child in answerContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public abstract void DeleteButton();
}
