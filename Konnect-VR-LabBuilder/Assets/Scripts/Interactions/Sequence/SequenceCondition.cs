/*
 * @author Marty Fitzer
 * @coauthor Hissam Shaikh
 */

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace KonnectVR.Interactions.Sequence
{
    [System.Serializable]
    public abstract class SequenceCondition : MonoBehaviour
    {
        public bool active { get; private set; } = false;

        private bool inactiveConditionMet = false;

        /// <summary>
        /// Event is fired when the condition is met in the inactive state before it has been met when active.
        /// </summary>
        [SerializeField]
        private UnityEvent onConditionMetEarly;
        public UnityEvent OnConditionMetEarly
        {
            get => onConditionMetEarly;
        }

        /// <summary>
        /// Event is fired when the condition is met in the active state.
        /// </summary>
        [SerializeField]
        private UnityEvent onConditionMetInSequence;
        public UnityEvent OnConditionMetInSequence
        {
            get => onConditionMetInSequence;
        }

        private bool activeConditionMet = false;

        /// <summary>
        /// Event is fired when the condition is met in the inactive state after it has been met when active.
        /// </summary>
        [SerializeField]
        private UnityEvent onConditionMetLate;
        public UnityEvent OnConditionMetLate
        {
            get => onConditionMetEarly;
        }

        public void activate()
        {
            active = true;
            onActivate();

            StartCoroutine(checkActiveCondition());
        }

        protected virtual void onActivate() { }

        private IEnumerator checkActiveCondition()
        {
            while (active && !activeConditionMet)
            {
                if (areConditionRequirementsMet())
                {
                    inactiveConditionMet = false;
                    activeConditionMet = true;
                    onConditionMetInSequence.Invoke();
                }

                yield return null;
            }
        }

        public void deactivate()
        {
            active = false;
            onDeactivate();

            StartCoroutine(checkInactiveCondition());
        }

        protected virtual void onDeactivate() { }

        private IEnumerator checkInactiveCondition()
        {
            while (!active && !inactiveConditionMet)
            {
                if (areConditionRequirementsMet()) //Calling private method to allow out of sequence condition to be met
                {
                    inactiveConditionMet = true;

                    if (!activeConditionMet)
                        onConditionMetEarly.Invoke();
                    else
                        onConditionMetLate.Invoke();
                }

                yield return null;
            }
        }

        public bool isMet()
        {
            return activeConditionMet || areConditionRequirementsMet();
        }

        protected abstract bool areConditionRequirementsMet();

        internal void forceConditionFullfillment()
        {
            if (!activeConditionMet)
            {
                inactiveConditionMet = false;
                activeConditionMet = true;
                onConditionMetInSequence.Invoke();
            }
        }

        internal void reset()
        {
            deactivate();

            activeConditionMet = false;
            inactiveConditionMet = false;
        }

        protected abstract void onReset();
    }
}
