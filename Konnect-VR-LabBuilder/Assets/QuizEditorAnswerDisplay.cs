using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizEditorAnswerDisplay : QuizEditorAnswer
{
    [SerializeField] private TMPro.TMP_Text AnswerTextBox;

    protected override void LoadAnswerText()
    {
        AnswerTextBox.text = targetAnswer.AnswerText;
    }

    protected override void SetToggled()
    {
        AnswerToggle.isOn = targetAnswer.IsCorrect;
    }

    protected override void SetToggleGroup()
    {
    }
}
