using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditQuizQuestionScreen : QuizQuestionScreen
{
    [SerializeField] private GameObject questionPrefab;
    [SerializeField] private string ScreenToOpen = "Edit Quiz";
    protected override void Reload()
    {
        base.Clear();
        quizObj = activeQuiz.GetComponent<QuizObj>();
        LoadQuestion();
        base.Reload();
    }

    private void LoadQuestion()
    {
        GameObject newQuestion;
        newQuestion = Instantiate(questionPrefab, base.ContentField.transform);
        newQuestion.GetComponent<QuizEditorQuestion>().BuildMe(ActiveQuestion, this);
    }

    public void AddAnswerButton()
    {
        ActiveQuestion.AddDefaultAnswer("New Option");

        Reload();
    }

    public void SaveQuestionButton()
    {
        GoToScreenNoSave(ScreenToOpen);
    }

    public void DeleteAnswerButton(QuizAnswer answer)
    {
        base.DeleteAnswerButton(ActiveQuestion, answer);
    }

    public void DeleteButton()
    {
        DeleteQuestionButton(ActiveQuestion);
        GoToScreenNoSave(ScreenToOpen);
    }
}
