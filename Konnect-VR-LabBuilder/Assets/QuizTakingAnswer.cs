using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuizTakingAnswer : MonoBehaviour
{
    [SerializeField] protected Toggle AnswerToggle;
    [SerializeField] protected TMPro.TMP_Text AnswerTextBox;

    [Header("No entry required for targetAnswer, visible in editor for debug purposes.")]
    [SerializeField] protected QuizAnswer targetAnswer;

    [Header("No entry required for answerContainer, visible in editor for debug purposes.")]
    [SerializeField] protected GameObject answerContainer;
    public void BuildMe(QuizAnswer answer, GameObject container)
    {
        targetAnswer = answer;
        answerContainer = container;

        if (targetAnswer != null)
        {
            LoadAnswerText();
            SetToggleGroup();
        }
    }

    protected void LoadAnswerText()
    {
        AnswerTextBox.text = targetAnswer.AnswerText;
    }

    abstract protected void SetToggleGroup();
}
