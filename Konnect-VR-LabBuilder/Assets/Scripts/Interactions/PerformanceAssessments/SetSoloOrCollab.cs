using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KonnectVR.Interactions.PerformanceAssessments
{
    public class SetSoloOrCollab : MonoBehaviour
    {
        public bool Collaborative;
        public bool Retakable;

        void Start()
        {
            if (Collaborative)
            {
                // Do something
            }
            else
            {
                // do something else
            }
            SetRetakable();
        }

        private void SetRetakable()
        {
            if (Retakable)
            {
                // Do something
            }
        }

    }

}
