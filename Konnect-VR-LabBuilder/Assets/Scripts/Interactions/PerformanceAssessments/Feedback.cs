using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KonnectVR.Interactions.PerformanceAssessments;
using TMPro;

public class Feedback : MonoBehaviour
{
    public TaskItem taskItem;
    public RectTransform content;
    public Transform SpawnPoint;

    private int MasterListLength = 0;

    public TMP_Text ConditionMetEarlyFeedback;
    public TMP_Text ConditionMetLateFeedback;

    public void ShowList(SequenceTask[] list)
    {

        int i = 0;
        foreach (SequenceTask task in list)
        {
            // 60 width of item
            float spawnY = i * 10;
            //newSpawn Position
            Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
            //instantiate item
            TaskItem SpawnedItem = Instantiate(taskItem, pos, new Quaternion(0, 0, 0, 0));
            //setParent
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            //get task name
            SpawnedItem.setTaskName(task.getTaskName());
            i++;
        }
        content.sizeDelta = new Vector2(0, list.Length * 10);

        MasterListLength = list.Length;
    }

    public void displayErrorMessage(int condition, TMP_Text ErrorText)
    {
        if (condition == 1)
        {
            ErrorText.text = ConditionMetEarlyFeedback.text;
        }
        else if (condition == 0)
        {
            ErrorText.text = ConditionMetLateFeedback.text;
        }
    }

    public void clearErrorMessage(TMP_Text ErrorText)
    {
        ErrorText.text = "";
    }

    public void changeTasksRemaining(TMP_Text tasksText, int taskscompleted)
    {
        tasksText.text = taskscompleted + " of " + MasterListLength + " Tasks Completed";
    }
}
