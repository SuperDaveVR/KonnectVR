using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceAnswer : QuizTakingAnswer
{
    [SerializeField] protected Toggle AnswerToggle;

    public void Toggle()
    {
        AnswerEntry();
    } 

    protected override void AnswerEntry()
    {
        targetAnswer.IsSelected = AnswerToggle.isOn;
    }

    protected override void ClassBuilder()
    {
        SetToggleGroup();
    }

    private void SetToggleGroup()
    {
        ToggleGroup toggleGroup = answerContainer.GetComponent<ToggleGroup>();
        if (toggleGroup != null)
        {
            AnswerToggle.group = toggleGroup;
        }
    }

    protected override void LoadSelected()
    {
        AnswerToggle.isOn = targetAnswer.IsSelected;
    }

}
