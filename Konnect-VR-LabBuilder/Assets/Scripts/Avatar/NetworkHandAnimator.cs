using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace KonnectVR.Avatar
{
    public class NetworkHandAnimator : NetworkBehaviour
    {
        private HandAnimator handAnim;

        private NetworkVariable<float> gripFingerTarget = new NetworkVariable<float>();
        private NetworkVariable<float> pointFingerTarget = new NetworkVariable<float>();

        private void Start()
        {
            //handAnim = GetComponent<HandAnimator>();
            //handAnim.animateWithInput = IsOwner;
        }

        public override void OnNetworkSpawn()
        {
            handAnim = GetComponent<HandAnimator>();
            handAnim.animateWithInput = IsOwner;
            handAnim.gripFingerTargetChange += onGripFingerTargetChange;
            handAnim.pointFingerTargetChange += onPointFingerTargetChange;
            gripFingerTarget.OnValueChanged += onNetworkGripFingerTargetChange;
            pointFingerTarget.OnValueChanged += onNetworkPointFingerTargetChange;
          
        }

        public override void OnNetworkDespawn()
        {
            handAnim.gripFingerTargetChange -= onGripFingerTargetChange;
            handAnim.pointFingerTargetChange -= onPointFingerTargetChange;
            gripFingerTarget.OnValueChanged += onNetworkGripFingerTargetChange;
            pointFingerTarget.OnValueChanged += onNetworkPointFingerTargetChange;
        }

        private void onGripFingerTargetChange(float gripTarget)
        {
            if (IsOwner)
                GripTargetServerRpc(gripTarget);
        }

        private void onPointFingerTargetChange(float pointTarget)
        {
            if (IsOwner)
                PointTargetServerRpc(pointTarget);
        }

        private void onNetworkGripFingerTargetChange(float previous, float current)
        {
            if (!IsOwner)
                handAnim.setFingerTarget(HandAnimator.FingerGroup.Grip, gripFingerTarget.Value);
        }

        private void onNetworkPointFingerTargetChange(float previous, float current)
        {
            if (!IsOwner)
                handAnim.setFingerTarget(HandAnimator.FingerGroup.Point, pointFingerTarget.Value);
        }

        [ServerRpc]
        private void GripTargetServerRpc(float gripTarget)
        {
            gripFingerTarget.Value = gripTarget;
        }

        [ServerRpc]
        private void PointTargetServerRpc(float pointTarget)
        {
            pointFingerTarget.Value = pointTarget;
        }
    }
}
