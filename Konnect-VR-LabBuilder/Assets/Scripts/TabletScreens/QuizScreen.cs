using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuizScreen : TabletScreenHandler
{
    [SerializeField] private string defaultQuizSysName = "BuilderQuizSystem";

    //Drag Load Quiz Screen into this
    [SerializeField] protected QuizFileManager quizFileManager;

    //Drag quiz selection screen into this
    [SerializeField] protected GameObject quizSelectScreen;

    //Content field of the quiz edit screen
    [SerializeField] protected GameObject ContentField;

    protected GameObject activeQuiz;
    protected QuizObj quizObj;

    [SerializeField] private List<GameObject> KeepAndMoveToTopObjects;
    [SerializeField] private List<GameObject> KeepAndMoveToBottomObjects;

    [SerializeField] private string previousScreen;

    public GameObject ActiveQuiz
    {
        get { return activeQuiz; }
        set
        {
            activeQuiz = value;
        }
    }

    void Awake()
    {
        if (quizFileManager == null)
        {
            quizFileManager = GameObject.Find(defaultQuizSysName).GetComponent<QuizFileManager>();
        }
        Reload();
    }

    private void OnEnable()
    {
        Reload();
    }

    protected virtual void Reload()
    {
        foreach(GameObject obj in KeepAndMoveToTopObjects)
        {
            obj.transform.SetAsFirstSibling();
        }

        foreach(GameObject obj in KeepAndMoveToBottomObjects)
        {
            obj.transform.SetAsLastSibling();
        }
    }

    protected void Clear()
    {
        Debug.Log("Deleting from " + ContentField.name);
        foreach (Transform child in ContentField.transform)
        {
            if (!KeepAndMoveToBottomObjects.Contains(child.gameObject) && !KeepAndMoveToTopObjects.Contains(child.gameObject))
                Destroy(child.gameObject);
        }
    }

    public virtual void DeleteQuestionButton(QuizQuestion deletedQuestion)
    {
        if (quizObj.QuestionsList.Count > 1)
            quizObj.QuestionsList.Remove(deletedQuestion);
        else
        {
            ErrorManager.Instance.ThrowError("Cannot delete! At least one question required.", true);
        }
    }

    public virtual void DeleteAnswerButton(QuizQuestion question, QuizAnswer deletedAnswer)
    {
        if (quizObj.QuestionsList.Contains(question))
        {
            int index = quizObj.QuestionsList.IndexOf(question);

            quizObj.QuestionsList[index].Answers.Remove(deletedAnswer);
        }
    }

    protected void PreviousScreen()
    {
        base.TabletMenuHandler.GoToScreenNoSave(previousScreen);
    }
}
