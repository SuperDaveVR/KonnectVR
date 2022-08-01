
using UnityEngine;
using UnityEngine.UI;

public class QuizEditorAnswerEdit : QuizEditorAnswer
{
    [SerializeField] private TMPro.TMP_InputField AnswerTextBox;

    protected override void LoadAnswerText()
    {
        AnswerTextBox.text = base.targetAnswer.AnswerText;
    }

    protected override void SetToggled()
    {
        AnswerToggle.isOn = targetAnswer.IsCorrect;
    }

    protected override void SetToggleGroup()
    {
        ToggleGroup toggleGroup = answerContainer.GetComponent<ToggleGroup>();
        if (toggleGroup != null)
        {
            AnswerToggle.group = toggleGroup;
        }
    }

    public void OnAnswerTextChanged()
    {
        base.targetAnswer.AnswerText = AnswerTextBox.text;
    }

    public void OnIsCorrectToggle()
    {
        base.targetAnswer.IsCorrect = AnswerToggle.isOn;
    }

    public void DeleteAnswerButton()
    {
        QuizEditorQuestionEdit questionEdit = answerContainer.transform.parent.gameObject.GetComponent<QuizEditorQuestionEdit>();
        int numOfAnswers = answerContainer.transform.childCount;
        if (questionEdit != null && numOfAnswers > 1)
        {
            questionEdit.DeleteAnswerButton(targetAnswer);
            Destroy(this.gameObject);
        } else if (numOfAnswers <= 1)
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
