/*
 * @author Marty Fitzer
 * @coauthor Hissam Shaikh
 */

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace KonnectVR.Interactions.Sequence
{
    public class SequenceEvent : MonoBehaviour
    {
        private SequenceCondition[] conditions;
        public SequenceCondition[] Conditions
        {
            get
            {
                if (conditions == null)
                    conditions = GetComponentsInChildren<SequenceCondition>();
                return conditions;
            }
        }

        [SerializeField]
        private UnityEvent onEventFired;
        public UnityEvent OnEventFired
        {
            get => onEventFired;
        }

        [SerializeField]
        private UnityEvent onSequenceEventReset;
        public UnityEvent OnSequenceEventReset
        {
            get => onSequenceEventReset;
        }

        public bool fired { get; private set; } = false;

        private enum EventState
        {
            Inactive, Active
        }

        private EventState eventState = EventState.Inactive;

        public void activate()
        {
            eventState = EventState.Active;

            //Activate conditions
            foreach (SequenceCondition condition in Conditions)
            {
                condition.activate();
            }

            StartCoroutine(runEvent());
        }

        private IEnumerator runEvent()
        {
            while (eventState == EventState.Active)
            {
                if (conditionsMet())
                {
                    fired = true;
                    eventState = EventState.Inactive;
                    OnEventFired.Invoke();
                }

                yield return null;
            }
        }

        public void deactivate()
        {
            eventState = EventState.Inactive;

            //Dectivate conditions
            foreach (SequenceCondition condition in Conditions)
            {
                condition.deactivate();
            }
        }

        public bool conditionsMet()
        {
            foreach (SequenceCondition condition in Conditions)
            {
                if (!condition.isMet())
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Force the event conditions to be met and fire the event.
        /// </summary>
        internal void forceFireEvent()
        {
            if (!fired)
            {
                fired = true;

                //Force conditions to be met
                foreach (SequenceCondition condition in Conditions)
                {
                    condition.forceConditionFullfillment();
                }

                //Deactivate this event and its conditions
                if (eventState == EventState.Active)
                {
                    deactivate();
                }
                
                OnEventFired.Invoke();
            }
        }

        /// <summary>
        /// Resets the event and its conditions.
        /// </summary>
        internal void reset()
        {
            //Deactivating conditions twice, here, refactor this
            deactivate();

            //Resetting condition also deactivates
            foreach (SequenceCondition condition in Conditions)
            {
                condition.reset();
            }

            fired = false;

            onSequenceEventReset.Invoke();
        }
    }
}