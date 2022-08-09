using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizAnswerPrefabLoader : MonoBehaviour
{
    [SerializeField] protected GameObject multipleChoicePrefab;
    [SerializeField] protected GameObject objectSelectionPrefab;
    [SerializeField] protected GameObject placementPrefab;
    [SerializeField] protected GameObject sequencePrefab;

    public GameObject GetPrefabType(QuizQuestion.QuestionTypes questionType)
    {
        switch (questionType)
        {
            case QuizQuestion.QuestionTypes.MultipleChoice:
                return multipleChoicePrefab;

            case QuizQuestion.QuestionTypes.ObjectSelection:
                return objectSelectionPrefab;

            case QuizQuestion.QuestionTypes.Placement:
                return placementPrefab;

            case QuizQuestion.QuestionTypes.Sequence:
                return sequencePrefab;

            default:
                return multipleChoicePrefab;
        }
    }
}
