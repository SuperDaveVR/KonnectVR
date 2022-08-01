using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class AnchorJoint : MonoBehaviour
    {
        [Tooltip("Determines the strength of the force applied to stay at the start position.")]
        public float forceStrength = 1f;

        private Rigidbody rb;
        private Vector3 targetPosition;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            targetPosition = transform.position;
        }

        private void controlVelocity()
        {
            Vector3 toTargetPos = targetPosition - transform.position;

            //Not at the target position
            if (toTargetPos.magnitude > 0)
            {
                rb.velocity = new Vector3(toTargetPos.x * forceStrength, toTargetPos.y * forceStrength, toTargetPos.z * forceStrength);
            }
        }

        void FixedUpdate()
        {
            controlVelocity();
        }
    }
}