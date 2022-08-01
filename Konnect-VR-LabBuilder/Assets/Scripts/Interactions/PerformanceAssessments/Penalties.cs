using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KonnectVR.Interactions.PerformanceAssessments;

public class Penalties : MonoBehaviour
{
    private SetTimer timer;
    private SetPenalties penalties; 
    // Update is called once per frame
    private void Start()
    {
        timer = AssessmentMetrics.Instance.setTimer;
        penalties = AssessmentMetrics.Instance.setPenalties;
    }

    public void AssessTimePenalty()
    {
        timer.setTimestamp(penalties.getTimeDeduction());
    }
}
