using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizEditorSeqAnswer : QuizEditorAnswer
{
    [Header("Class Specific Values")]
    [SerializeField] private TMPro.TMP_InputField SeqTextBox;

    protected override void QuestionTypeMethods()
    {
        CorrectAnswerText();
    }

    private void CorrectAnswerText()
    {
        if (SeqTextBox)
            SeqTextBox.text = targetAnswer.CorrectAnswer;
    }

    public void OnCorrectAnswerChange()
    {
        targetAnswer.CorrectAnswer = SeqTextBox.text;
    }
}
