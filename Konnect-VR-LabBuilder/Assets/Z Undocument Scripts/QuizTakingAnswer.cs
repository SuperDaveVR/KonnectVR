using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuizTakingAnswer : MonoBehaviour
{
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
            ClassBuilder();
            LoadSelected();
        }
    }

    protected void LoadAnswerText()
    {
        AnswerTextBox.text = targetAnswer.AnswerText;
    }

    abstract protected void LoadSelected();

    abstract protected void ClassBuilder();

    abstract protected void AnswerEntry();

    public void InputClicked(TMPro.TMP_InputField inputField)
    {
        if (mfitzer.Interactions.Keyboard.Instance)
            mfitzer.Interactions.Keyboard.Instance.ActivateKeyboard(inputField);
    }

    public void InputExited()
    {
        if (mfitzer.Interactions.Keyboard.Instance)
            mfitzer.Interactions.Keyboard.Instance.DeactivateKeyboard();
    }
}
