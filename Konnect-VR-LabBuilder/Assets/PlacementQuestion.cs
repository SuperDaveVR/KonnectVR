using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementQuestion : QuizTakingQuestion
{
    [SerializeField] private string placementContainerName;
    public string PlacementContainerName
    {
        get { return placementContainerName; }
        private set { placementContainerName = value; }
    }

    protected override void LoadAnswers()
    {
        ClearAnswers();

        GameObject newAnswer;
        foreach (QuizAnswer answer in targetQuestion.Answers)
        {
            newAnswer = Instantiate(answerPrefab, answerContainer.transform);
            PlacementAnswer quizAnswerComp = newAnswer.GetComponent<PlacementAnswer>(); //Create a component in your Answer prefab and include a public BuildMe script that takes data from QuizAnswer.cs

            quizAnswerComp.PlacementBuildMe(answer, answerContainer, placementContainerName);
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
        
    }
}
