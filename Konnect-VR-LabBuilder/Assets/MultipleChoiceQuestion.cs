using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceQuestion : QuizTakingQuestion
{
    protected override void LoadAnswers()
    {
        ClearAnswers();

        GameObject newAnswer;
        foreach (QuizAnswer answer in targetQuestion.Answers)
        {
            newAnswer = Instantiate(answerPrefab, answerContainer.transform);
            newAnswer.GetComponent<QuizTakingAnswer>().BuildMe(answer, answerContainer); //Create a component in your Answer prefab and include a public BuildMe script that takes data from QuizAnswer.cs
        }
    }

    protected override void ClearAnswers()
    {
        foreach (Transform child in answerContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    protected override void LoadChooseMultiple()
    {
        bool chooseMultEnabled = targetQuestion.ChooseMultiple;

        chooseMultipleText.enabled = chooseMultEnabled;

        ToggleGroup toggleGroup = answerContainer.GetComponent<ToggleGroup>();

        if (toggleGroup != null)
        {
            toggleGroup.enabled = !chooseMultEnabled;
        }
    }

    private void OnDisable()
    {
        RemoveHighlight();
    }

    private void OnDestroy()
    {
        RemoveHighlight();
    }
}
