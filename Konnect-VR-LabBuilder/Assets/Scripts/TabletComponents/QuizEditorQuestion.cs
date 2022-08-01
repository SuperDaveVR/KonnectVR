using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class QuizEditorQuestion : MonoBehaviour
{
    [SerializeField] protected GameObject answerContainer;
    [SerializeField] protected GameObject answerPrefab;

    [Header("No entry required for targetQuestion, visible in editor for debug purposes.")]
    [SerializeField] protected QuizQuestion targetQuestion;

    [SerializeField] protected Toggle multipleAnswersToggle;
    [SerializeField] protected Toggle highlightObjectToggle;
    [SerializeField] protected TMPro.TMP_InputField targetObjectInput;

    protected QuizScreen quizScreen;

    public void BuildMe(QuizQuestion question, QuizScreen quizScreen)
    {
        targetQuestion = question;
        this.quizScreen = quizScreen;

        if (targetQuestion != null)
        {
            LoadQuestionText();
            LoadChooseMultiple();
            LoadHighlight();
            LoadAnswers();
        }
    }


    protected abstract void LoadQuestionText();

    protected abstract void LoadChooseMultiple();

    protected abstract void LoadHighlight();

    protected abstract void LoadAnswers();

    protected abstract void ClearAnswers();

    public abstract void DeleteButton();
}
