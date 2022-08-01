using KonnectVR.Interactions.Sequence;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KonnectVR.Interactions.PerformanceAssessments
{
    [RequireComponent(typeof(SequenceEvent))]
    public class SequenceTask : MonoBehaviour
    {
        public string taskName;
        private bool? isCorrect = null;

        public SequenceEvent sequenceEvent { get; private set; }

        public UnityEvent<SequenceTask> OnTaskPerformedEarly { get; private set; } = new UnityEvent<SequenceTask>();
        public UnityEvent<SequenceTask> OnTaskPerformed { get; private set; } = new UnityEvent<SequenceTask>();
        public UnityEvent<SequenceTask> OnTaskPerformedLate { get; private set; } = new UnityEvent<SequenceTask>();

        private UnityAction taskPerformedEarlyHandler;
        private UnityAction taskPerformedHandler;
        private UnityAction taskPerformedLateHandler;

        private bool subscribed = false;

        private void subscribeToSequenceErrorEvents()
        {
            if (!subscribed)
            {
                if (taskPerformedEarlyHandler == null)
                    taskPerformedEarlyHandler = () => { OnTaskPerformedEarly.Invoke(this); };
                if (taskPerformedHandler == null)
                    taskPerformedHandler = () => { OnTaskPerformed.Invoke(this); };
                if (taskPerformedLateHandler == null)
                    taskPerformedLateHandler = () => { OnTaskPerformedLate.Invoke(this); };

                sequenceEvent.OnEventFired.AddListener(taskPerformedHandler);
                sequenceEvent.OnEventFired.AddListener(AssessmentMetrics.Instance.setFeedback.onSequenceTaskCompleted);

                foreach (SequenceCondition condition in sequenceEvent.Conditions)
                {
                    condition.OnConditionMetEarly.AddListener(taskPerformedEarlyHandler);
                    condition.OnConditionMetEarly.AddListener(AssessmentMetrics.Instance.setFeedback.onTaskConditionMetEarly);

                    condition.OnConditionMetLate.AddListener(taskPerformedLateHandler);
                    condition.OnConditionMetLate.AddListener(AssessmentMetrics.Instance.setFeedback.onTaskConditionMetLate);
                }
            }

            subscribed = true;
        }

        private void unsubscribeFromSequenceErrorEvents()
        {
            if (subscribed && sequenceEvent)
            {
                sequenceEvent.OnEventFired.RemoveListener(taskPerformedHandler);

                if (AssessmentMetrics.Instance)
                    sequenceEvent.OnEventFired.RemoveListener(AssessmentMetrics.Instance.setFeedback.onSequenceTaskCompleted);

                foreach (SequenceCondition condition in sequenceEvent.Conditions)
                {
                    condition.OnConditionMetEarly.RemoveListener(taskPerformedEarlyHandler);

                    if (AssessmentMetrics.Instance)
                        condition.OnConditionMetEarly.RemoveListener(AssessmentMetrics.Instance.setFeedback.onTaskConditionMetEarly);

                    condition.OnConditionMetLate.RemoveListener(taskPerformedLateHandler);

                    if (AssessmentMetrics.Instance)
                        condition.OnConditionMetLate.RemoveListener(AssessmentMetrics.Instance.setFeedback.onTaskConditionMetLate);
                }
            }

            subscribed = false;
        }

        public string getTaskName()
        {
            return taskName;
        }

        public bool? getIsCorrect()
        {
            return isCorrect;
        }

        public void setIsCorrect(bool? value)
        {
            isCorrect = value;
        }

        /*public void taskPerformedCorrect()
        {
            GradeSequenceAssessment.Instance.taskPerformedCorrect(this);
        }

        public void taskPerformedIncorrect()
        {
            GradeSequenceAssessment.Instance.taskPerformedIncorrect(this);
        }*/

        private void OnEnable()
        {
            if (!sequenceEvent)
                sequenceEvent = GetComponent<SequenceEvent>();
            subscribeToSequenceErrorEvents();
        }

        private void OnDisable()
        {
            if (!sequenceEvent)
                sequenceEvent = GetComponent<SequenceEvent>();
            unsubscribeFromSequenceErrorEvents();
        }
    }
}