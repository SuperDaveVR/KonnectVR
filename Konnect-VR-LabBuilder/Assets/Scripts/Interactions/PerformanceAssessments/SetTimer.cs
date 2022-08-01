using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace KonnectVR.Interactions.PerformanceAssessments
{
    public class SetTimer : MonoBehaviour
    {
        public bool BeginTimerUponStartup;
        public TMP_Text timerText;
        private float CurrentTime = 0;
        private bool isTimerRunning = false;
        public GameObject BeginAssessmentButton;
        public bool DisplayTimer; // Whether or not the student can see the timer
        private Feedback feedback;
        private SetFeedback setFeedback;
        private TMP_Text progressDisplayedPanel;

        void Start()
        {
            feedback = AssessmentMetrics.Instance.feedback;
            setFeedback = AssessmentMetrics.Instance.setFeedback;
            progressDisplayedPanel = setFeedback.DisplayProgressPanel.GetComponent<TMP_Text>();

            timerText.enabled = false;
            
            if (BeginTimerUponStartup)
                StartTimer();
        }


        void Update()
        {
            if (isTimerRunning)
            {
                CurrentTime += Time.deltaTime;
            }
            if (DisplayTimer && !BeginAssessmentButton.activeSelf)
                timerText.text = CurrentTime.ToString("F1");
        }

        public void StartTimer()
        {
            Debug.Log("Master Task List: \n" + GradeSequenceAssessment.Instance.printMasterTaskList());
            isTimerRunning = true;
            BeginAssessmentButton.SetActive(false);
            timerText.enabled = true;
            feedback.ShowList(GradeSequenceAssessment.Instance.getMasterTaskList());
            feedback.changeTasksRemaining(progressDisplayedPanel, 0);
            Debug.Log("MasterTaskList loaded");
        }
        public float getTimestamp() // This method is run when a task is completed, and the time of completing that task is recorded -  needs to be attached by the content creator to every event in a sequence
        {
            return CurrentTime;
        }

        public void setTimestamp(int penalty) // When a penalty is assessed, need to run this method to add the time deduction
        {
            CurrentTime += (float)penalty;
        }

        public void stopTimer() // this method will run once the final event in the sequence takes place - needs to be attached by the content creator to the final event
        {
            isTimerRunning = false;
        }


    }
}

