using UnityEngine;

namespace KonnectVR.Interactions.Sequence
{
    public class EventCondition : SequenceCondition
    {
        [SerializeField, Tooltip("This event must fire before the condition is met.")]
        private SequenceEvent eventDependency;

        [SerializeField, ReadOnlyField]
        private bool dependencyEventFired = false;

        protected override bool areConditionRequirementsMet()
        {
            if (active)
            {
                dependencyEventFired = eventDependency.fired;
                return dependencyEventFired;
            }

            return false;
        }

        protected override void onReset()
        {
            dependencyEventFired = false;
        }
    }
}