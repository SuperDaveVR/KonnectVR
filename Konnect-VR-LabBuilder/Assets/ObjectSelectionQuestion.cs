using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelectionQuestion : QuizTakingQuestion
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
        
    }
}
