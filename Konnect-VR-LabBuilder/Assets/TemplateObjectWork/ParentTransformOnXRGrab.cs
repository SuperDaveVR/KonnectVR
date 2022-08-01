using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace KonnectVR.Interactions
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class ParentTransformOnXRGrab : Interactable
    {
        //Point Calculation
        [SerializeField] LayerMask raycastMask = -1;

        public XRGrabInteractable xrGrabInteractable { get; private set; }
        public Rigidbody rb { get; private set; }
        [SerializeField] private GameObject parentObject;
        private enum enAxis { x, y, z, all };

        [SerializeField] enAxis Axis = enAxis.x;
        [SerializeField] float offset = 0;

        public GameObject ParentObject
        {
            set{ parentObject = value; }
        }


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

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            xrGrabInteractable = GetComponent<XRGrabInteractable>();
            rb = GetComponent<Rigidbody>();

            //Make gravity on detach settings match initial use gravity flag on rigidbody
            xrGrabInteractable.forceGravityOnDetach = rb.useGravity;

            offset = getOffset();
        }

        // Update is called once per frame
        void Update()
        {
            if (grabbed)
            {
                XRBaseInteractor interactor = xrGrabInteractable.selectingInteractor;

                if (Axis == enAxis.all && offset == 0)
                {
                    offset = Vector3.Distance(interactor.transform.position, this.transform.position);
                }

                Vector3 targetPosition = getTargetPosition(interactor);
                this.transform.position = targetPosition;

                Vector3 targetParentPosition = getTargetParentPosition(targetPosition);
                parentObject.transform.position = targetParentPosition;
     
            } else
            {
                if (Axis == enAxis.all && offset != 0)
                {
                    offset = 0;
                }
            }
        }

        [SerializeField]
        Transform m_AttachTransform;

        /// <summary>
        /// The attachment point to use on this Interactable (will use this object's position if none set).
        /// </summary>
        public Transform attachTransform
        {
            get => m_AttachTransform;
            set => m_AttachTransform = value;
        }

        Vector3 getTargetPosition(XRBaseInteractor interactor)
        {
            Ray targetRay = new Ray(interactor.transform.parent.transform.position, interactor.transform.parent.transform.forward);
            float distance;

            if (Axis != enAxis.all)
            {
                distance = Vector3.Distance(interactor.transform.position, this.transform.position);
            } else
            {
                distance = offset;
            }

            Vector3 rawPosition = targetRay.GetPoint(distance);

            Debug.Log("Raw Position: " + rawPosition);

            Vector3 truePosition;

            switch (Axis)
            {
                case enAxis.x:
                    truePosition.x = rawPosition.x;
                    truePosition.y = transform.position.y;
                    truePosition.z = transform.position.z;
                    break;

                case enAxis.y:
                    truePosition.x = transform.position.x;
                    truePosition.y = rawPosition.y;
                    truePosition.z = transform.position.z;
                    break;

                case enAxis.z:
                    truePosition.x = transform.position.x;
                    truePosition.y = transform.position.y;
                    truePosition.z = rawPosition.z;
                    break;

                case enAxis.all:
                    truePosition.x = rawPosition.x;
                    truePosition.y = rawPosition.y;
                    truePosition.z = rawPosition.z;
                    break;

                default:
                    truePosition.x = transform.position.x;
                    truePosition.y = transform.position.y;
                    truePosition.z = transform.position.z;
                    break;
            }

            return truePosition;
        }

        Vector3 getTargetParentPosition(Vector3 position)
        {
            var rawPosition = position;

            Vector3 truePosition;

            switch (Axis)
            {
                case enAxis.x:
                    truePosition.x = rawPosition.x - offset;
                    truePosition.y = parentObject.transform.position.y;
                    truePosition.z = parentObject.transform.position.z;
                    break;

                case enAxis.y:
                    truePosition.x = parentObject.transform.position.x;
                    truePosition.y = rawPosition.y - offset;
                    truePosition.z = parentObject.transform.position.z;
                    break;

                case enAxis.z:
                    truePosition.x = parentObject.transform.position.x;
                    truePosition.y = parentObject.transform.position.y;
                    truePosition.z = rawPosition.z - offset;
                    break;

                case enAxis.all:
                    truePosition.x = rawPosition.x;
                    truePosition.y = rawPosition.y;
                    truePosition.z = rawPosition.z;
                    break;

                default:
                    truePosition.x = parentObject.transform.position.x;
                    truePosition.y = parentObject.transform.position.y;
                    truePosition.z = parentObject.transform.position.z;
                    break;
            }

            return truePosition;
        }

        float getOffset()
        {
            float offsetValue = 0;
            switch (Axis)
            {
                case enAxis.x:
                    offsetValue = this.transform.position.x - parentObject.transform.position.x;
                    break;

                case enAxis.y:
                    offsetValue = this.transform.position.y - parentObject.transform.position.y;
                    break;

                case enAxis.z:
                    offsetValue = this.transform.position.z - parentObject.transform.position.z;
                    break;

                default:
                    break;
            }

            return offsetValue;
        }

    }
}
