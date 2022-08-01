using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskItem : MonoBehaviour
{ 
    public TMP_Text TaskName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTaskName(string taskName)
    {
        TaskName.text = taskName;
    }
}
