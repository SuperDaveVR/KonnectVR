using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizEditorQuestionDisplay : QuizEditorQuestion
{
    [SerializeField] private TMPro.TMP_Text QuestionTextBox;
    [SerializeField] private ToggleGroup answerToggleGroup;

    [SerializeField] private string ScreenToOpen = "Edit Quiz Question";

    protected override void LoadQuestionText()
    {
        QuestionTextBox.text = targetQuestion.Text;
    }

    protected override void LoadChooseMultiple()
    {
        bool chooseMult = targetQuestion.ChooseMultiple;
        multipleAnswersToggle.isOn = chooseMult;
        answerToggleGroup.enabled = !chooseMult;
    }

    protected override void LoadHighlight()
    {
        bool highlightObj = targetQuestion.Highlight;
        highlightObjectToggle.isOn = highlightObj;
        targetObjectInput.text = targetQuestion.HighlightedObjName;
    }

    public void EditButton()
    {
        OpenScreen();
    }

    private void OpenScreen()
    {
        QuizQuestionScreen questionScreen = quizScreen.GetScreen(ScreenToOpen).GetComponent<QuizQuestionScreen>();
        questionScreen.ActiveQuiz = quizScreen.ActiveQuiz;
        questionScreen.ActiveQuestion = targetQuestion;
        quizScreen.GoToScreenNoSave(ScreenToOpen);
    }

    public override void DeleteButton()
    {
        quizScreen.DeleteQuestionButton(targetQuestion);
    }
}
