using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KonnectVR.Interactions.PerformanceAssessments
{
    public class SetPenalties : MonoBehaviour
    {
        public bool TimePenaltyOn; // whether or not a time penalty occurs when a mistake is make
        public int TimeDeductionInSeconds; // the time deduction for a mistake
        public int TimeLimitInSeconds; // the max amount of time for which the assessment is allowed to count for full credit
        public bool AssessTimePenaltyImmediately; // Whether or not the time penalty gets added to the timer as soon as a mistake is made
        public bool ScorePenaltyOn; // whether or not a score penalty occurs when a mistake is make
        public float ScoreDeduction; // the score deduction for a mistake
        public bool AllowIncorrectTasks; // whether or not an assessment allows the student to continue with a sequence after having made a sequencing error
        public bool AllowUndoingTasks; // whether or not a sequence task can be undone if the student believes they have made an error

        public int getTimeDeduction()
        {
            return TimeDeductionInSeconds;
        }

        public int getTimeLimit()
        {
            return TimeLimitInSeconds;
        }

        public float getScoreDeduction()
        {
            return ScoreDeduction;
        }
    }

}
