using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace KonnectVR.Avatar
{
    public class HandAnimator : MonoBehaviour
    {
        public float speed = 5.0f;
        //public XRController controller = null;
        public XRNode node = XRNode.LeftHand;

        private Animator animator = null;

        public enum FingerGroup { Grip, Point }

        private readonly List<Finger> gripFingers = new List<Finger>()
        {
            new Finger(FingerType.Middle),
            new Finger(FingerType.Ring),
            new Finger(FingerType.Pinky)
        };

        public UnityAction<float> gripFingerTargetChange;
        private float gripFingerTarget = 0f;
        public float GripFingerTarget
        {
            get
            {
                return gripFingerTarget;
            }
            private set
            {
                gripFingerTarget = value;
                gripFingerTargetChange?.Invoke(gripFingerTarget);
            }
        }

        private readonly List<Finger> pointFingers = new List<Finger>
        {
            new Finger(FingerType.Index),
            new Finger(FingerType.Thumb)
        };

        public event UnityAction<float> pointFingerTargetChange;
        private float pointFingerTarget = 0f;
        public float PointFingerTarget
        {
            get
            {
                return pointFingerTarget;
            }
            private set
            {
                pointFingerTarget = value;
                pointFingerTargetChange?.Invoke(pointFingerTarget);
            }
        }

        public bool animateWithInput { get; set; } = true;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (animateWithInput)
            {
                //Store input
                CheckGrip();
                CheckPointer();
            }

            //Smooth input values
            SmoothFinger(pointFingers);
            SmoothFinger(gripFingers);

            //Apply smoothed values
            AnimateFinger(pointFingers);
            AnimateFinger(gripFingers);
        }

        #region Input Animation

        private void CheckGrip()
        {
            if (InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                if (gripValue != GripFingerTarget)
                {
                    SetFingerTargets(gripFingers, gripValue);
                    GripFingerTarget = gripValue;
                }
            }
        }

        private void CheckPointer()
        {
            if (InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.trigger, out float pointerValue))
            {
                if (pointerValue != PointFingerTarget)
                {
                    SetFingerTargets(pointFingers, pointerValue);
                    PointFingerTarget = pointerValue;
                }
            }
        }

        #endregion Input Animation

        #region Manual Animation

        public void setFingerTarget(FingerGroup fingerGroup, float value)
        {
            if (fingerGroup == FingerGroup.Grip)
            {                
                SetFingerTargets(gripFingers, value);
                GripFingerTarget = value;
            }
            else //FingerGroup.Point
            {
                SetFingerTargets(pointFingers, value);
                PointFingerTarget = value;
            }
        }

        #endregion Manual Animation

        private void SetFingerTargets(List<Finger> fingers, float value)
        {
            foreach (Finger finger in fingers)
                finger.target = value;
        }

        private void SmoothFinger(List<Finger> fingers)
        {
            foreach (Finger finger in fingers)
            {
                float time = speed * Time.unscaledDeltaTime;
                finger.current = Mathf.MoveTowards(finger.current, finger.target, time);
            }
        }

        private void AnimateFinger(List<Finger> fingers)
        {
            foreach (Finger finger in fingers)
                AnimateFinger(finger.type.ToString(), finger.current);
        }

        private void AnimateFinger(string finger, float blend)
        {
            animator.SetFloat(finger, blend);
        }
    }
}