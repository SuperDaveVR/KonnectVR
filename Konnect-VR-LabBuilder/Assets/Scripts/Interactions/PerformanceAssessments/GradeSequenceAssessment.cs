using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KonnectVR.Interactions.Sequence;
using UnityEngine.Events;
using TMPro;
using KonnectVR.UserManagement;
using System.Text;
using System.IO;


namespace KonnectVR.Interactions.PerformanceAssessments
{
    public class GradeSequenceAssessment : MonoBehaviour  // combine with SequenceTaskManager
    {
        public string AssessmentName = "Insert name of assessment";

        private static GradeSequenceAssessment instance;
        public static GradeSequenceAssessment Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<GradeSequenceAssessment>();
                return instance;
            }
        }

        private List<TaskPerformed> RecordedSequenceTasks = new List<TaskPerformed>();
        private SequenceTask[] MasterTaskList; // this is the master list of all the tasks in the sequence, in order

        private SetCriteriaWeights setCriteriaWeights;
        private SetTimer timer;
        private SetPenalties setPenalties;
        private Penalties penalties;
        private SetFeedback setFeedback;
        private Feedback feedback;
        private TMP_Text progressDisplayedPanel;
        private TMP_Text displayErrorPanel;

        private int timeDeducation;
        private float scoreDeduction;
        private string userID;
        private int numTasksPerformed = 0;
        private int numTasksPerformedCorrect = 0;
        private string[][] FinalResults;

        private List<string[]> AssessmentResults = new List<string[]>();

        void Start()
        {
            setCriteriaWeights = AssessmentMetrics.Instance.setCriteriaWeights;
            timeDeducation = AssessmentMetrics.Instance.setPenalties.getTimeDeduction();
            scoreDeduction = AssessmentMetrics.Instance.setPenalties.getScoreDeduction();
            MasterTaskList = SequenceTaskManager.Instance.SequenceTasks;
            timer = AssessmentMetrics.Instance.setTimer;
            setPenalties = AssessmentMetrics.Instance.setPenalties;
            penalties = AssessmentMetrics.Instance.penalties;
            setFeedback = AssessmentMetrics.Instance.setFeedback;
            feedback = AssessmentMetrics.Instance.feedback;
            progressDisplayedPanel = setFeedback.DisplayProgressPanel.GetComponent<TMP_Text>();
            displayErrorPanel = setFeedback.DisplayErrorPanel.GetComponent<TMP_Text>();

            userID = GameObject.Find("CurrentUser").GetComponent<User>().getUserId();


            Debug.Log("trying to fill in Master Task List");
            foreach (SequenceTask task in MasterTaskList)
            {
                task.OnTaskPerformedEarly.AddListener(taskPerformedEarlyIncorrect);
                task.OnTaskPerformed.AddListener(taskPerformedCorrect);
                task.OnTaskPerformedLate.AddListener(taskPerformedLateIncorrect);
                task.sequenceEvent.OnSequenceEventReset.AddListener(clearTasks);
            }
        }

        /* commented out because we broke this function up into two smaller functions, and changed the way we evaluate if a task is correct or not. But we don't want 
         * to delete it outright yet, as some functionality may be necessary depending on how the sequences are set up...
         * 
         * public void taskPerformed(SequenceTask theTaskPerformed)
        {
            int count = RecordedSequenceTasks.Count;
            string previousTaskName;
            if (count == 0)
            {
                count = 1;
                previousTaskName = theTaskPerformed.getTaskName();
            }
            else
                previousTaskName = RecordedSequenceTasks[count - 1].taskName;
            Debug.Log("count = "+count);
            if (gradeTask(previousTaskName, theTaskPerformed)) //we need to get the component of the specific task performed though...
            {
                RecordedSequenceTasks.Add(new TaskPerformed(
                    theTaskPerformed.getTaskName(),
                    "username", // We'll put the identifier for the student who performed the task here
                    true,
                    0,
                    0,
                    timer.getTimestamp())
                );
            }
            else
            {
                RecordedSequenceTasks.Add(new TaskPerformed(
                    theTaskPerformed.getTaskName(),
                    "username", // We'll put the identifier for the student who performed the task here
                    false,
                    timeDeducation,
                    scoreDeduction,
                    timer.getTimestamp())
                );
            }
            if (theTaskPerformed.getTaskName().Equals(MasterTaskList[MasterTaskList.Length - 1].getTaskName())){
                timer.stopTimer();
                Debug.Log("RecordedSequenceTasks finished: " + printRecordedSequenceTasks());
            }
            Debug.Log("RecordedSequenceTasks: " + printRecordedSequenceTasks());
        }*/

        public void taskPerformedCorrect(SequenceTask theTaskPerformed)
        {
            numTasksPerformed++;
            numTasksPerformedCorrect++;
            theTaskPerformed.setIsCorrect(true);
            RecordedSequenceTasks.Add(new TaskPerformed(
                theTaskPerformed.getTaskName(),
                userID,
                true,
                0,
                0,
                timer.getTimestamp())
            );
            feedback.changeTasksRemaining(progressDisplayedPanel, numTasksPerformedCorrect);
            feedback.clearErrorMessage(displayErrorPanel);
            if (theTaskPerformed.getTaskName().Equals(MasterTaskList[MasterTaskList.Length - 1].getTaskName()))
            {
                timer.stopTimer();
                Debug.Log("RecordedSequenceTasks: " + printRecordedSequenceTasks());
                FinalResults = AssessmentResultsToArray();
            }
            Debug.Log("RecordedSequenceTasks: " + printRecordedSequenceTasks());

        }

        public void taskPerformedEarlyIncorrect(SequenceTask theTaskPerformed)
        {
            numTasksPerformed++;
            theTaskPerformed.setIsCorrect(false);
            RecordedSequenceTasks.Add(new TaskPerformed(
                theTaskPerformed.getTaskName(),
                userID,
                false,
                timeDeducation,
                scoreDeduction,
                timer.getTimestamp())
            );
            penalties.AssessTimePenalty();
            feedback.displayErrorMessage(0, displayErrorPanel);
            if (theTaskPerformed.getTaskName().Equals(MasterTaskList[MasterTaskList.Length - 1].getTaskName()))
            {
                timer.stopTimer();
                Debug.Log("RecordedSequenceTasks: " + printRecordedSequenceTasks());
                FinalResults = AssessmentResultsToArray();
            }
            //Debug.Log("RecordedSequenceTasks: " + printRecordedSequenceTasks());
        }

        public void taskPerformedLateIncorrect(SequenceTask theTaskPerformed)
        {
            numTasksPerformed++;
            theTaskPerformed.setIsCorrect(false);
            RecordedSequenceTasks.Add(new TaskPerformed(
                theTaskPerformed.getTaskName(),
                userID,
                false,
                timeDeducation,
                scoreDeduction,
                timer.getTimestamp())
            );
            penalties.AssessTimePenalty();
            feedback.displayErrorMessage(1, displayErrorPanel);
            if (theTaskPerformed.getTaskName().Equals(MasterTaskList[MasterTaskList.Length - 1].getTaskName()))
            {
                timer.stopTimer();
                Debug.Log("RecordedSequenceTasks: " + printRecordedSequenceTasks());
                FinalResults = AssessmentResultsToArray();
            }
            //Debug.Log("RecordedSequenceTasks: " + printRecordedSequenceTasks());
        }

        public void undoTask()
        {
            numTasksPerformed--;
            for (int i = 0; i < RecordedSequenceTasks.Count; i++)
            {
                if (RecordedSequenceTasks[RecordedSequenceTasks.Count - 1].taskName.Equals(MasterTaskList[i].getTaskName()))
                {
                    if (MasterTaskList[i].getIsCorrect() == true)
                    {
                        numTasksPerformedCorrect--;
                    }
                    MasterTaskList[i].setIsCorrect(null);
                    RecordedSequenceTasks.RemoveAt(RecordedSequenceTasks.Count - 1);
                    return;
                }
            }
            feedback.changeTasksRemaining(progressDisplayedPanel, numTasksPerformedCorrect);
        }

        public void clearTasks()
        {
            numTasksPerformed = 0;
            numTasksPerformedCorrect = 0;
            feedback.changeTasksRemaining(progressDisplayedPanel, numTasksPerformedCorrect);
            RecordedSequenceTasks.Clear();
        }
        /*public bool gradeTask(string previousTaskName, SequenceTask performedTask)
        {
            int taskIndexInMasterlist = Array.IndexOf(MasterTaskList, performedTask);
            SequenceTask previousTask;
            if (taskIndexInMasterlist == 0)
                previousTask = MasterTaskList[0];
            else
                previousTask = MasterTaskList[taskIndexInMasterlist - 1];
            Debug.Log("Previous Task Name From The recorded task list: "+previousTaskName + "\nPrevious Task Name From The Master list: " + previousTask.getTaskName());
            if (previousTaskName.Equals(previousTask.getTaskName()))
            {
                performedTask.setIsCorrect(true);
                return true;
            }
            else
            {
                performedTask.setIsCorrect(false);
                return false;
            }
        }*/

        public string printMasterTaskList()
        {
            string toString = "";
            foreach (SequenceTask task in MasterTaskList)
            {
                toString += task.getTaskName() + ",\n";
            }
            return toString;
        }

        public SequenceTask[] getMasterTaskList()
        {
            return MasterTaskList;
        }

        public List<TaskPerformed> getRecordedSequenceTasks()
        {
            return RecordedSequenceTasks;
        }

        private string printRecordedSequenceTasks()
        {
            string toString = "";
            foreach (TaskPerformed task in RecordedSequenceTasks)
            {
                toString += "Task Name: " + task.taskName + ", Username: " + task.username + ", Result: " + task.result + ", Time Penalty: " + task.timePenalty + ", ScorePenalty: " + task.scorePenalty + ", Timestamp: " + task.timestamp + "\n";
            }
            return toString;
        }

        private string[][] AssessmentResultsToArray()
        {
            StringBuilder sb = new StringBuilder();

            string[][] output = new string[RecordedSequenceTasks.Count + 6][];

            int length = output.GetLength(0);
            string delimiter = ",";

            int i = 1;
            string[] AssessmentResultsTemp = new string[6];
            AssessmentResultsTemp[0] = "Task Name";
            AssessmentResultsTemp[1] = "Username";
            AssessmentResultsTemp[2] = "Result";
            AssessmentResultsTemp[3] = "Time Penalty";
            AssessmentResultsTemp[4] = "ScorePenalty";
            AssessmentResultsTemp[5] = "Timestamp";
            AssessmentResults.Add(AssessmentResultsTemp);
            output[0] = AssessmentResults[0];
            sb.AppendLine(string.Join(delimiter, output[0]));


            foreach (TaskPerformed task in RecordedSequenceTasks)
            {
                AssessmentResultsTemp[0] = task.taskName;
                AssessmentResultsTemp[1] = task.username;
                AssessmentResultsTemp[2] = task.result.ToString();
                AssessmentResultsTemp[3] = task.timePenalty.ToString();
                AssessmentResultsTemp[4] = task.scorePenalty.ToString();
                AssessmentResultsTemp[5] = task.timestamp.ToString();
                AssessmentResults.Add(AssessmentResultsTemp);
                output[i] = AssessmentResults[i];
                sb.AppendLine(string.Join(delimiter, output[i]));
                i++;
            }

            AssessmentResultsTemp[0] = "";
            AssessmentResultsTemp[1] = "";
            AssessmentResultsTemp[2] = "";
            AssessmentResultsTemp[3] = "";
            AssessmentResultsTemp[4] = "";
            AssessmentResultsTemp[5] = "";
            AssessmentResults.Add(AssessmentResultsTemp);
            output[output.Length - 5] = AssessmentResults[output.Length - 5];
            sb.AppendLine(string.Join(delimiter, output[output.Length - 5]));

            int timerWeight = setCriteriaWeights.getTimerWeight();
            int accuracyWeight = 100 - timerWeight;

            AssessmentResultsTemp[0] = "Timer Weight: " + timerWeight+"%";
            AssessmentResultsTemp[1] = "Accuracy Weight: " + accuracyWeight + "%";
            AssessmentResults.Add(AssessmentResultsTemp);
            output[output.Length - 4] = AssessmentResults[output.Length - 4];
            sb.AppendLine(string.Join(delimiter, output[output.Length - 4]));

            int numTasksIncorrect = numTasksPerformed - numTasksPerformedCorrect;
            float pointsLost = scoreDeduction * numTasksIncorrect;
            float pointsOffScore = ((float)pointsLost * ((float)accuracyWeight / 100));
            float score = 100 - pointsOffScore;
            Debug.Log("Score: " + score);

            AssessmentResultsTemp[0] = "Number of Incorrect Tasks: " + numTasksIncorrect;
            AssessmentResultsTemp[1] = "Scored Deduction per Incorrect Task: " + scoreDeduction;
            AssessmentResultsTemp[2] = "Points Lost: " + pointsLost;
            AssessmentResultsTemp[3] = "Score Deduction: " + pointsOffScore;
            AssessmentResults.Add(AssessmentResultsTemp);
            output[output.Length - 3] = AssessmentResults[output.Length - 3];
            sb.AppendLine(string.Join(delimiter, output[output.Length - 3]));

            int timeLimit = setPenalties.getTimeLimit();
            float timeOver = timer.getTimestamp() - (float)timeLimit;
            float percentTimeOver = 0;
            if (timeOver > 0)
            {
                if (timeLimit != 0)
                {
                    percentTimeOver = timeOver / timeLimit;
                    score = score - ((float)timerWeight * percentTimeOver);
                    Debug.Log("Score: " + score);
                }
            }
            else
                timeOver = 0;

            AssessmentResultsTemp[0] = "Time Limit: " + timeLimit + " seconds";
            AssessmentResultsTemp[1] = "Time over limit: " + timeOver + " seconds";
            AssessmentResultsTemp[2] = "PercentTimeOver: " + (percentTimeOver * 100) + "%";
            AssessmentResultsTemp[3] = "";
            AssessmentResults.Add(AssessmentResultsTemp);
            output[output.Length - 2] = AssessmentResults[output.Length - 2];
            sb.AppendLine(string.Join(delimiter, output[output.Length - 2]));

            AssessmentResultsTemp[0] = "Overall Score";
            AssessmentResultsTemp[1] = ""+ score;
            AssessmentResultsTemp[2] = "";
            AssessmentResults.Add(AssessmentResultsTemp);
            output[output.Length - 1] = AssessmentResults[output.Length - 1];
            sb.AppendLine(string.Join(delimiter, output[output.Length - 1]));
            Debug.Log("Score: "+ score);

            string filePath = getPath();

            StreamWriter outStream = System.IO.File.CreateText(filePath);
            outStream.WriteLine(sb);
            outStream.Close();
            return output;
            // Score is at output[count - 1][1];
        }

        private string getPath()
        {
            #if UNITY_EDITOR
            return Application.dataPath + "/Assessment Results/" + AssessmentName + "_" + userID + "_Assessment_Results.csv";
            #elif UNITY_ANDROID
            return Application.persistentDataPath+ /* whatever the name of this assessment is */ "" + "_" + userID + "_Assessment_Results.csv";
            #elif UNITY_IPHONE
            return Application.persistentDataPath+ /* whatever the name of this assessment is */ "" + "_" + userID + "_Assessment_Results.csv";
            #else
            return Application.dataPath +"/Assessment Results/"+ /* whatever the name of this assessment is */ "" + "_" + userID + "_Assessment_Results.csv";
            #endif
        }

        /*public void calculateGrade(string[] temp, StringBuilder sb)
        {
            int timerWeight = setCriteriaWeights.getTimerWeight();
            int accuracyWeight = 100 - timerWeight;

            temp[0] = "Timer Weight: " + timerWeight;
            temp[1] = "Accuracy Weight: " + accuracyWeight;
            AssessmentResults.Add(temp);

            int numTasksIncorrect = numTasksPerformed- numTasksPerformedCorrect;
            float pointsLost = scoreDeduction * numTasksIncorrect;
            float score = 100 - (pointsLost * (accuracyWeight/100));

            temp[0] = "Number of Incorrect Tasks: " + numTasksIncorrect;
            temp[1] = "Scored Deduction per Incorrect Task: " + scoreDeduction;
            temp[2] = "Points Lost: " + pointsLost;
            AssessmentResults.Add(temp);

            int timeLimit = setPenalties.getTimeLimit();
            float timeOver = timer.getTimestamp() - timeLimit;
            float percentTimeOver = 0;
            if (timeOver > 0)
            {
                percentTimeOver = timeOver / timeLimit;
                score = score - (timerWeight * percentTimeOver);
            }
            else
                timeOver = 0;

            temp[0] = "Time Limit: " + timeLimit + " seconds";
            temp[1] = "Time over limit: " + timeOver + " seconds";
            temp[2] = "PercentTimeOver: " + (percentTimeOver*100)+"%";
            AssessmentResults.Add(temp);

            temp[0] = "Overall Score";
            temp[1] = score.ToString();
            temp[2] = "";
            AssessmentResults.Add(temp);
        }*/

       
    }
}