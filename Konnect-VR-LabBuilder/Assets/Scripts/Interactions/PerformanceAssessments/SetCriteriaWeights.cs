using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KonnectVR.Interactions.PerformanceAssessments
{
    public class SetCriteriaWeights : MonoBehaviour
    {
        public int TimerWeight;
        private int AccuracyWeight;

        private void Start()
        {
            SetWeights();
        }

        private void SetWeights()
        {
            TimerWeight = Math.Abs(TimerWeight % 100);
            AccuracyWeight = 100 - TimerWeight;
            Debug.Log("TimerWeight = " + TimerWeight + ", AccuracyWeight = " + AccuracyWeight);
        }

        public int getTimerWeight()
        {
            return TimerWeight;
        }
    }

}
