using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class QuizEditorAnswer : MonoBehaviour
{
    [Header("No entry required for targetAnswer, visible in editor for debug purposes.")]
    [SerializeField] protected QuizAnswer targetAnswer;

    [Header("No entry required for answerContainer, visible in editor for debug purposes.")]
    [SerializeField] protected GameObject answerContainer;

    [Header("Only need either AnswerTextBox OR AnswerEditBox")]
    [SerializeField] protected TMPro.TMP_Text AnswerTextBox;
    [SerializeField] protected TMPro.TMP_InputField AnswerEditBox;

    [Header("Optional Error Checking")]
    [SerializeField] protected TMPro.TMP_Text ErrorTextBox;

    public void BuildMe(QuizAnswer answer, GameObject container)
    {
        targetAnswer = answer;
        answerContainer = container;

        if (targetAnswer != null)
        {
            LoadAnswerText();
            QuestionTypeMethods();
        }
    }

    private void LoadAnswerText()
    {
        if (AnswerTextBox)
            AnswerTextBox.text = targetAnswer.AnswerText;

        if (AnswerEditBox)
            AnswerEditBox.text = targetAnswer.AnswerText;

        if (ErrorTextBox != null)
        {
            AnswerTextErrorCheck();
        }
    }

    abstract protected void QuestionTypeMethods();

    public void OnAnswerTextChanged()
    {
        targetAnswer.AnswerText = AnswerEditBox.text;
        if (ErrorTextBox != null)
        {
            AnswerTextErrorCheck();
        }
    }

    protected virtual void AnswerTextErrorCheck()
    {
        if (!targetAnswer.AnswerObjectExists())
        {
            ErrorTextBox.gameObject.SetActive(true);
            ErrorTextBox.text = "Input object is not found!";
        } else
        {
            ErrorTextBox.gameObject.SetActive(false);
        }
    }

    public void DeleteAnswerButton()
    {
        QuizEditorQuestionEdit questionEdit = answerContainer.transform.parent.gameObject.GetComponent<QuizEditorQuestionEdit>();
        int numOfAnswers = answerContainer.transform.childCount;
        if (questionEdit != null && numOfAnswers > 1)
        {
            questionEdit.DeleteAnswerButton(targetAnswer);
            Destroy(this.gameObject);
        }
        else if (numOfAnswers <= 1)
        {
            ErrorManager.Instance.ThrowError("Cannot delete. At least one answer is required!", true);
        }
    }

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
