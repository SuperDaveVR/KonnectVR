using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizEditorPlaAnswer : QuizEditorAnswer
{
    [Header("Class Specific Values")]
    [SerializeField] private Toggle AnswerToggle;

    protected override void QuestionTypeMethods()
    {
        SetToggled();
        SetToggleGroup();
    }

    private void SetToggled()
    {

        bool toggleOn = false;

        string correctAns = targetAnswer.CorrectAnswer;

        bool canConvert;
        Boolean.TryParse(correctAns, out canConvert);

        if (canConvert)
        {
            toggleOn = Convert.ToBoolean(correctAns);
        }

        AnswerToggle.isOn = toggleOn;
    }

    private void SetToggleGroup()
    {
        ToggleGroup toggleGroup = answerContainer.GetComponent<ToggleGroup>();
        if (toggleGroup != null && toggleGroup.isActiveAndEnabled)
        {
            AnswerToggle.group = toggleGroup;
        }
    }

    public void OnIsCorrectToggle()
    {
        targetAnswer.CorrectAnswer = AnswerToggle.isOn.ToString();
    }

}
