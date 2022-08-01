/*
 * @author Marty Fitzer
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KonnectVR
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyLimiter : MonoBehaviour
    {
        private Rigidbody rb;

        [SerializeField]
        private bool limitVelocity = false;
        public bool LimitVelocity
        {
            get => limitVelocity;
            set => limitVelocity = value;
        }

        [SerializeField]
        private float maxVelocityMagnitude = float.PositiveInfinity;
        public float MaxVelocityMagnitude
        {
            get => maxVelocityMagnitude;
            set => maxVelocityMagnitude = value;
        }

        [SerializeField]
        private bool limitAngularVelocity = false;
        public bool LimitAngularVelocity
        {
            get => limitAngularVelocity;
            set => limitAngularVelocity = value;
        }

        [SerializeField]
        private float maxAngularVelocityMagnitude = float.PositiveInfinity;
        public float MaxAngularVelocityMagnitude
        {
            get => maxAngularVelocityMagnitude;
            set => maxAngularVelocityMagnitude = value;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (limitVelocity)
                enforceVelocityLimit();

            if (limitAngularVelocity)
                enforceAngularVelocityLimit();
        }

        private void enforceVelocityLimit()
        {
            if (rb.velocity.magnitude > maxVelocityMagnitude)
            {
                rb.velocity = Vector3.zero;
            }
        }

        private void enforceAngularVelocityLimit()
        {
            if (rb.angularVelocity.magnitude > maxAngularVelocityMagnitude)
            {
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
