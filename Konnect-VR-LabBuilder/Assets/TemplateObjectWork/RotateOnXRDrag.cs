using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace KonnectVR.Interactions
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class RotateOnXRDrag : Interactable
    {
        public XRGrabInteractable xrGrabInteractable { get; private set; }
        public Rigidbody rb { get; private set; }
        [SerializeField] private GameObject graphicObject;
        private enum enAxis { x, y, z, all };

        [SerializeField] enAxis Axis = enAxis.x;

        private Vector3 referenceLocation;
        [SerializeField] private float sensitivity = 25;
        [SerializeField] private bool invert = false;
        private bool hasReference = false;

        private float distance;
        private const float distanceOffset = 10;

        public GameObject GraphicObject
        {
            set { graphicObject = value; }
        }

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

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (grabbed)
            {
                XRBaseInteractor interactor = xrGrabInteractable.selectingInteractor;

                if (!hasReference)
                {
                    Ray targetRay = new Ray(interactor.transform.parent.transform.position, interactor.transform.parent.transform.forward);

                    referenceLocation = targetRay.GetPoint(distance);

                    hasReference = true;
                }

                distance = distanceOffset + Vector3.Distance(interactor.transform.position, this.transform.position);

                TargetRotation(interactor);

            } else
            {
                if (hasReference)
                {
                    hasReference = false;
                }
            }
        }

        private void TargetRotation(XRBaseInteractor interactor)
        {
            Vector3 interactorLocation = interactor.transform.parent.transform.position;
            Ray targetRay = new Ray(interactorLocation, interactor.transform.parent.transform.forward);

            Vector3 pointingLocation = targetRay.GetPoint(distance);
            Vector3 targetDir = pointingLocation - referenceLocation;

            float xAngle = 0;
            float yAngle = 0;
            float zAngle = 0;

            float baseValue = 1;

            if (invert)
            {
                baseValue = -1;
            }

            float xVal;
            float yVal;
            float zVal;
            float xDist;
            float zDist;

            switch (Axis)
            {
                case enAxis.x:
                    zVal = (pointingLocation.z - referenceLocation.z) * sensitivity;
                    yVal = baseValue * ((pointingLocation.y - referenceLocation.y) * sensitivity);

                    if (Mathf.Abs(zVal) > Mathf.Abs(yVal))
                    {
                        xAngle = zVal;
                    } else
                    {
                        xAngle = yVal;
                    }

                    break;

                case enAxis.y:
                    zDist = (interactorLocation.z - this.transform.position.z);
                    xVal = (pointingLocation.x - referenceLocation.x) * sensitivity;
                    zVal = baseValue * (pointingLocation.z - referenceLocation.z) * sensitivity;

                    if (Mathf.Abs(zVal) > Mathf.Abs(xVal))
                    {
                        yAngle = zVal;
                    }
                    else
                    {
                        if (zDist < 0)
                        {
                            yAngle = -xVal;
                        } else
                        {
                            yAngle = xVal;
                        }
                    }
                    break;

                case enAxis.z:
                    xDist = (interactorLocation.x - this.transform.position.x);
                    xVal = baseValue * (pointingLocation.x - referenceLocation.x) * sensitivity;
                    yVal = ((pointingLocation.y - referenceLocation.y) * sensitivity);

                    if (Mathf.Abs(xVal) > Mathf.Abs(yVal))
                    {
                        zAngle = xVal;
                    }
                    else
                    {
                        if (xDist < 0)
                        {
                            zAngle = -yVal;
                        }
                        else
                        {
                            zAngle = yVal;
                        }
                    }

                    break;

                case enAxis.all:
                    xDist = (interactorLocation.x - this.transform.position.x);
                    zDist = (interactorLocation.z - this.transform.position.z);
                    if (Mathf.Abs(zDist) > Mathf.Abs(xDist))
                    {
                        if (zDist < 0) {
                            xAngle = ((pointingLocation.y - referenceLocation.y) * sensitivity);
                            yAngle = -(pointingLocation.x - referenceLocation.x) * sensitivity;
                        } else
                        {
                            xAngle = -((pointingLocation.y - referenceLocation.y) * sensitivity);
                            yAngle = (pointingLocation.x - referenceLocation.x) * sensitivity;
                        }
                    } else
                    {
                        if(xDist < 0)
                        {
                            zAngle = -((pointingLocation.y - referenceLocation.y) * sensitivity);
                            yAngle = (pointingLocation.z - referenceLocation.z) * sensitivity;
                        } else
                        {
                            zAngle = ((pointingLocation.y - referenceLocation.y) * sensitivity);
                            yAngle = -(pointingLocation.z - referenceLocation.z) * sensitivity;
                        }
                    }
                    break;

                default:
                    break;
            }
            referenceLocation = pointingLocation;

            graphicObject.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
        }
    }
}
