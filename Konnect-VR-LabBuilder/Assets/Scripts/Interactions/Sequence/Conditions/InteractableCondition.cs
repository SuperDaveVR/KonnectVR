using UnityEngine;

namespace KonnectVR.Interactions.Sequence
{
    public class InteractableCondition : SequenceCondition
    {
        [SerializeField]
        private Interactable interactable;

        [SerializeField, Tooltip("The number of interactions with the given interactable for the condition to be met.")]
        private int requiredInteractions = 1;

        private int initialInteractions = -1;

        [SerializeField, ReadOnlyField]
        private int interactions = 0;

        private void OnValidate()
        {
            if (requiredInteractions < 1) //Require at least 1 interaction
                requiredInteractions = 1;
        }

        protected override void onActivate()
        {
            initialInteractions = interactable.interactions;
            interactions = 0;
        }

        protected override void onDeactivate()
        {
            initialInteractions = interactable.interactions;
            interactions = 0;
        }

        protected override bool areConditionRequirementsMet()
        {
            interactions = interactable.interactions - initialInteractions;
            return interactable && interactions >= requiredInteractions;
        }

        protected override void onReset()
        {
            interactions = 0;
        }
    }
}