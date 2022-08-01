using UnityEngine;

namespace KonnectVR.Interactions.Sequence
{
    public class ZoneCondition : SequenceCondition
    {
        [SerializeField, ReadOnlyField]
        bool zoneFilled;
        
        protected override bool areConditionRequirementsMet()
        {
            return zoneFilled;
        }

        protected override void onReset()
        {
            zoneFilled = false;
        }

        public void zoneDone()
        {
            zoneFilled = true;
        }

        public void zoneUndone()
        {
            zoneFilled = false;
        }
    }
}
