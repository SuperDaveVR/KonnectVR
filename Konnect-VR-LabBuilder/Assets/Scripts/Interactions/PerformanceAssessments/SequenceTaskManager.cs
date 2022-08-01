using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KonnectVR.Interactions.PerformanceAssessments
{
    public class SequenceTaskManager : MonoBehaviour
    {
        private static SequenceTaskManager instance;
        public static SequenceTaskManager Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<SequenceTaskManager>();
                return instance;
            }
        }

        private SequenceTask[] sequenceTasks;
        public SequenceTask[] SequenceTasks
        {
            get
            {
                if (sequenceTasks == null)
                    sequenceTasks = GetComponentsInChildren<SequenceTask>();
                return sequenceTasks;
            }
        }
    }
}