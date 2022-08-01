using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class QuizEditorAnswer : MonoBehaviour
{
    [SerializeField] protected Toggle AnswerToggle;

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
            SetToggled();
            SetToggleGroup();
        }
    }

    abstract protected void LoadAnswerText();

    abstract protected void SetToggled();

    abstract protected void SetToggleGroup();
}
