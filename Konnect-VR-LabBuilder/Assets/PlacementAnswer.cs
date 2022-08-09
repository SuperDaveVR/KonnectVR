using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementAnswer : QuizTakingAnswer
{
    [SerializeField] private string placementContainerName;
    public void PlacementBuildMe(QuizAnswer answer, GameObject container, string placementContainerName)
    {
        BuildMe(answer, container);
        this.placementContainerName = placementContainerName;
    }
    protected override void AnswerEntry()
    {
        throw new System.NotImplementedException();
    }

    protected override void ClassBuilder()
    {
        throw new System.NotImplementedException();
    }

    protected override void LoadSelected()
    {
        throw new System.NotImplementedException();
    }
}
