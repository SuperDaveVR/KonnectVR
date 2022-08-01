using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KonnectVR.Interactions.PerformanceAssessments
{
    public class SetFeedback : MonoBehaviour
    {
        public bool SequenceErrorOccurs; // whether or not the student is informed when they make a sequence mistake
        public bool DisplayTasks; // Lists the tasks in sequence to be completed
        public bool DisplayProgress; // informs student of the number of tasks remaining
        public GameObject DisplayTasksPanel; // Panel where Tasks will be displayed
        public GameObject DisplayProgressPanel; // Panel where the sequence progress will be displayed
        public GameObject DisplayErrorPanel; // Panel where the student will be informed that they made an error

        [SerializeField]
        private GameObject earlyConditionMetFeedback;

        [SerializeField]
        private GameObject lateConditionMetFeedback;

        void Start()
        {
            
            DisplaySequenceErrors();
            DisplayTasksOnScreen();
            DisplayProgressOnScreen();
        }

        private void DisplaySequenceErrors()
        {
            if (SequenceErrorOccurs)
                DisplayErrorPanel.SetActive(true);
            else
                DisplayErrorPanel.SetActive(false);
        }

        private void DisplayTasksOnScreen()
        {
            if (DisplayTasks)
                DisplayTasksPanel.SetActive(true);
            else
                DisplayTasksPanel.SetActive(false);
        }

        private void DisplayProgressOnScreen()
        {
            if (DisplayProgress)
                DisplayProgressPanel.SetActive(true);
            else
                DisplayProgressPanel.SetActive(false);
        }

        public void onTaskConditionMetEarly()
        {
            earlyConditionMetFeedback.SetActive(false);
            lateConditionMetFeedback.SetActive(false);
        }

        public void onSequenceTaskCompleted()
        {
            earlyConditionMetFeedback.SetActive(false);
            lateConditionMetFeedback.SetActive(false);
        }

        public void onTaskConditionMetLate()
        {
            earlyConditionMetFeedback.SetActive(false);
            lateConditionMetFeedback.SetActive(false);
        }
    }

}
