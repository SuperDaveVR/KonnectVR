using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceAnswer : QuizTakingAnswer
{
    protected override void SetToggleGroup()
    {
        ToggleGroup toggleGroup = answerContainer.GetComponent<ToggleGroup>();
        if (toggleGroup != null)
        {
            AnswerToggle.group = toggleGroup;
        }
    }
}
