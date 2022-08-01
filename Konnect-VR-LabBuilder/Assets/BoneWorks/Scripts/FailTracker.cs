using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FailTracker : MonoBehaviour
{
    [System.Serializable]
    public class FailEvent : UnityEvent<string>
    {
    }

    public FailEvent failEventTracker;

    public string failureReport;

    public bool endOnFail;

    private int failures = 0;

    private List<string> failureNotes;

    private void Start()
    {
        failEventTracker = new FailEvent();
        failEventTracker.AddListener(Fail);
    }

    private void Fail(string failureNote)
    {
        failures++;
        failureNotes.Add(failureNote);
    }

    public void CreateFailureReport()
    {
        
        failureReport = "Total Failures: " + failures + "\n";

        foreach (string failureNote in failureNotes)
        {
            failureReport = failureReport + failureNote + "\n";
        }
    }
}
