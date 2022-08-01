using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KonnectVR.Interactions.PerformanceAssessments
{
    [RequireComponent(typeof(GradeSequenceAssessment))]
    [RequireComponent(typeof(SetCriteriaWeights))]
    [RequireComponent(typeof(SetFeedback))]
    [RequireComponent(typeof(SetPenalties))]
    [RequireComponent(typeof(SetSoloOrCollab))]
    [RequireComponent(typeof(SetTimer))]
    [RequireComponent(typeof(Feedback))]
    [RequireComponent(typeof(Penalties))]
    public class AssessmentMetrics : MonoBehaviour
    {
        private static AssessmentMetrics instance;
        public static AssessmentMetrics Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<AssessmentMetrics>();
                return instance;
            }
        }

        private GradeSequenceAssessment _gradeSequenceAssessment;
        public GradeSequenceAssessment gradeSequenceAssessment
        {
            get
            {
                if (!_gradeSequenceAssessment)
                    _gradeSequenceAssessment = GetComponent<GradeSequenceAssessment>();
                return _gradeSequenceAssessment;
            }
        }

        private SetCriteriaWeights _setCriteriaWeights;
        public SetCriteriaWeights setCriteriaWeights
        {
            get
            {
                if (!_setCriteriaWeights)
                    _setCriteriaWeights = GetComponent<SetCriteriaWeights>();
                return _setCriteriaWeights;
            }
        }

        private SetFeedback _setFeedback;
        public SetFeedback setFeedback
        {
            get
            {
                if (!_setFeedback)
                    _setFeedback = GetComponent<SetFeedback>();
                return _setFeedback;
            }
        }

        private SetPenalties _setPenalties;
        public SetPenalties setPenalties
        {
            get
            {
                if (!_setPenalties)
                    _setPenalties = GetComponent<SetPenalties>();
                return _setPenalties;
            }
        }

        private SetSoloOrCollab _setSoloOrCollab;
        public SetSoloOrCollab setSoloOrCollab
        {
            get
            {
                if (!_setSoloOrCollab)
                    _setSoloOrCollab = GetComponent<SetSoloOrCollab>();
                return _setSoloOrCollab;
            }
        }

        private SetTimer _setTimer;
        public SetTimer setTimer
        {
            get
            {
                if (!_setTimer)
                    _setTimer = GetComponent<SetTimer>();
                return _setTimer;
            }
        }

        private Feedback _feedback;
        public Feedback feedback
        {
            get
            {
                if (!_feedback)
                    _feedback = GetComponent<Feedback>();
                return _feedback;
            }
        }

        private Penalties _penalties;
        public Penalties penalties
        {
            get
            {
                if (!_penalties)
                    _penalties = GetComponent<Penalties>();
                return _penalties;
            }
        }
    }
}