using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuizEditorQuestionEdit : QuizEditorQuestion
{
    [SerializeField] private TMPro.TMP_InputField QuestionTextBox;
    [SerializeField] private ToggleGroup answerToggleGroup;

    private GameObject highlightedObject;

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
        targetObjectInput.interactable = highlightObj;
        targetObjectInput.text = targetQuestion.HighlightedObjName;

        if (highlightObj)
        {
            HighlightObjectCheck();
        }
    }

    protected override void LoadAnswers()
    {
        ClearAnswers();

        GameObject newAnswer;
        foreach (QuizAnswer answer in targetQuestion.Answers)
        {
            newAnswer = Instantiate(answerPrefab, answerContainer.transform);
            newAnswer.GetComponent<QuizEditorAnswer>().BuildMe(answer, answerContainer); //Create a component in your Answer prefab and include a public BuildMe script that takes data from QuizAnswer.cs
        }
    }

    protected override void ClearAnswers()
    {
        foreach (Transform child in answerContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public override void DeleteButton()
    {

    }

    public void DeleteAnswerButton(QuizAnswer answer)
    {
        targetQuestion.Answers.Remove(answer);
    }

    public void OnQuestionTextChanged()
    {
        targetQuestion.Text = QuestionTextBox.text;
    }

    public void OnChooseMultipleToggle()
    {
        bool chooseMult = multipleAnswersToggle.isOn;
        targetQuestion.ChooseMultiple = chooseMult;
        answerToggleGroup.enabled = !chooseMult;
    }

    public void OnHighlightObjectToggle()
    {
        bool highlightObj = highlightObjectToggle.isOn;
        targetQuestion.Highlight = highlightObj;
        targetObjectInput.interactable = highlightObj;

        if (highlightObj)
        {
            HighlightObjectCheck();
        }
    }

    public void OnObjectEntryTextChanged()
    {
        targetQuestion.HighlightedObjName = targetObjectInput.text;
    }

    public void InputClicked(TMPro.TMP_InputField inputField)
    {
        quizScreen.InputClicked(inputField);
    }

    public void InputExited()
    {
        quizScreen.InputExited();
    }

    public void HighlightObjectCheck()
    {
        RemoveHighlight();

        Transform highlightTransform = PlacedObjectsHandler.Instance.gameObject.transform.Find(targetObjectInput.text);

        if (!highlightTransform || targetObjectInput.text.Length < 1)
        {
            HighlightObjectNotFound();
        } else
        {

            highlightedObject = highlightTransform.gameObject;
            Debug.Log("Found: " + highlightedObject.name);
            HighlightingManager.Instance.AddHighlight(highlightedObject);
        }
    }

    private void RemoveHighlight()
    {
        if (highlightedObject)
        {
            HighlightingManager.Instance.RemoveHighlight(highlightedObject);
        }
    }

    private void HighlightObjectNotFound()
    {
        Debug.Log("Requested Highlight Object does not exist");
    }

    private void OnDisable()
    {
        RemoveHighlight();
    }
}
