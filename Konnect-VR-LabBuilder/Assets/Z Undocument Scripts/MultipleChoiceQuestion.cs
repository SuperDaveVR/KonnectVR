using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceQuestion : QuizTakingQuestion
{
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
}
