using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceAnswer : QuizTakingAnswer
{
    [SerializeField] private TMPro.TMP_InputField inputBox;
    protected override void LoadSelected()
    {
        inputBox.text = targetAnswer.EnteredAnswer;
    }

    protected override void AnswerEntry()
    {
        targetAnswer.EnteredAnswer = inputBox.text;
    }

    protected override void ClassBuilder()
    {
        
    }

    public void InputEdited()
    {
        AnswerEntry();
    }

    private void OnDestroy()
    {
        AnswerEntry();
    }
}
