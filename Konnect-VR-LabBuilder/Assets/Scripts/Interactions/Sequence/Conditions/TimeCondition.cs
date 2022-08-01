using UnityEngine;

namespace KonnectVR.Interactions.Sequence
{
    public class TimeCondition : SequenceCondition
    {
        [SerializeField, Tooltip("Timer length in seconds.")]
        private float timerLength = 0f;

        [SerializeField, ReadOnlyField]
        private float timer = 0f;

        private const float NOT_SET = -1f;
        private float previousTime = NOT_SET;

        protected override void onActivate()
        {
            base.onActivate();

            previousTime = Time.time;
        }

        protected override bool areConditionRequirementsMet()
        {
            if (active)
            {
                if (timer >= timerLength)
                    return true;
                else
                {
                    float elapsedTime = Time.time - previousTime;
                    timer += elapsedTime;
                    previousTime = Time.time;

                    return false;
                }
            }

            return false;
        }

        protected override void onReset()
        {
            timer = 0f;
            previousTime = NOT_SET;
        }
    }
}