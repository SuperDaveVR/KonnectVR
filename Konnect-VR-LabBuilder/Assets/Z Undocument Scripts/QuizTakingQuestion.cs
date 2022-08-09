using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuizTakingQuestion : MonoBehaviour
{
    [SerializeField] protected TMPro.TMP_Text questionText;
    [SerializeField] protected TMPro.TMP_Text chooseMultipleText;

    [SerializeField] protected GameObject answerContainer;
    [SerializeField] protected GameObject answerPrefab;

    [Header("No entry required for targetQuestion, visible in editor for debug purposes.")]
    [SerializeField] protected QuizQuestion targetQuestion;

    [SerializeField] private ToggleGroup answerToggleGroup;

    protected QuizScreen quizScreen;

    protected GameObject highlightedObject;

    public void BuildMe(QuizQuestion question, QuizScreen quizScreen)
    {
        targetQuestion = question;
        this.quizScreen = quizScreen;

        if (targetQuestion != null)
        {
            LoadQuestionText();
            LoadChooseMultiple();
            LoadHighlight();
            LoadAnswers();
            ClassBuilder();
        }
    }

    protected void LoadQuestionText()
    {
        questionText.text = targetQuestion.Text;
    }

    protected abstract void LoadChooseMultiple();

    protected virtual void LoadHighlight()
    {
        bool highlightObj = targetQuestion.Highlight;

        if (highlightObj)
        {
            HighlightObjectCheck();
        }
    }

    protected virtual void HighlightObjectCheck()
    {
        RemoveHighlight();

        string targetObjectName = targetQuestion.HighlightedObjName;

        Transform highlightTransform = PlacedObjectsHandler.Instance.gameObject.transform.Find(targetObjectName);

        if (!highlightTransform || targetObjectName.Length < 1)
        {
            HighlightObjectNotFound();
        }
        else
        {

            highlightedObject = highlightTransform.gameObject;
            Debug.Log("Found: " + highlightedObject.name);
            HighlightingManager.Instance.AddHighlight(highlightedObject);
        }
    }

    protected void RemoveHighlight()
    {
        if (highlightedObject)
        {
            HighlightingManager.Instance.RemoveHighlight(highlightedObject);
        }
    }

    protected void HighlightObjectNotFound()
    {
        Debug.Log("Requested Highlight Object does not exist");
    }

    protected virtual void LoadAnswers()
    {
        ClearAnswers();

        GameObject newAnswer;
        foreach (QuizAnswer answer in targetQuestion.Answers)
        {
            newAnswer = Instantiate(answerPrefab, answerContainer.transform);
            newAnswer.GetComponent<QuizTakingAnswer>().BuildMe(answer, answerContainer); //Create a component in your Answer prefab and include a public BuildMe script that takes data from QuizAnswer.cs
        }
    }

    protected abstract void ClearAnswers();

    protected virtual void OnDisable()
    {
        RemoveHighlight();
    }

    protected virtual void OnDestroy()
    {
        RemoveHighlight();
    }

    protected virtual void ClassBuilder()
    {

    }
}
