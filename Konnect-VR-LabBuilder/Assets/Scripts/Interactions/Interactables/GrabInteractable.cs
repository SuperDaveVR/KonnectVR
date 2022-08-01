using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace KonnectVR.Interactions
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class GrabInteractable : Interactable
    {
        public XRGrabInteractable xrGrabInteractable { get; private set; }

        public Rigidbody rb { get; private set; }

        /// <summary>
        /// Is this grab interactable currently being grabbed?
        /// </summary>
        public bool grabbed
        {
            get
            {
                return interacting && interactingInteractor;
            }
        }

        protected override void Start()
        {
            base.Start();

            xrGrabInteractable = GetComponent<XRGrabInteractable>();
            rb = GetComponent<Rigidbody>();

            //Make gravity on detach settings match initial use gravity flag on rigidbody
            xrGrabInteractable.forceGravityOnDetach = rb.useGravity;
        }
    }
}