using UnityEngine;

namespace KonnectVR.Interactions.Sequence
{
    public class CounterCondition : SequenceCondition
    {
        public int counter = 0;
        public int completeCondition = 0;
           

        protected override bool areConditionRequirementsMet()
        {
            return counter >= completeCondition;
        }

        protected override void onReset()
        {
            counter = 0;
        }

        public void increment()
        {
            if(counter < completeCondition)
                counter++;
        }

        public void decrement()
        {
            if(counter >= 0)
                counter--;
        }
    }
}
