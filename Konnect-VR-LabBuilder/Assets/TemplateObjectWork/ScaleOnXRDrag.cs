using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace KonnectVR.Interactions
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class ScaleOnXRDrag : Interactable
    {
        public XRGrabInteractable xrGrabInteractable { get; private set; }
        public Rigidbody rb { get; private set; }
        [SerializeField] private GameObject parentObject;

        [SerializeField] private Vector3 referenceLocation;
        [SerializeField] private float startingDistance;
        [SerializeField] private float sensitivity;
        private bool hasReference = false;

        private float distance;
        private const float distanceOffset = 10;

        //RelativePositions
        float relX;
        float relY;
        float relZ;

        public GameObject GraphicObject
        {
            set { parentObject = value; }
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

            sensitivity = 2;
            xrGrabInteractable = GetComponent<XRGrabInteractable>();
            rb = GetComponent<Rigidbody>();

            //Make gravity on detach settings match initial use gravity flag on rigidbody
            xrGrabInteractable.forceGravityOnDetach = rb.useGravity;

            relX = this.transform.localPosition.x;
            relY = this.transform.localPosition.y;
            relZ = this.transform.localPosition.z;

            Debug.Log("RelX = " + relX);
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
                    startingDistance = Vector3.Distance(referenceLocation, this.transform.position);

                    hasReference = true;
                }

                distance = Vector3.Distance(interactor.transform.position, this.transform.position);

                TargetScale(interactor);
                FixLocationAndScale();
            }
        }

        private void TargetScale(XRBaseInteractor interactor)
        {
            Vector3 interactorLocation = interactor.transform.parent.transform.position;
            Ray targetRay = new Ray(interactorLocation, interactor.transform.parent.transform.forward);

            Vector3 pointingLocation = targetRay.GetPoint(distance);

            float pointingDistance = Vector3.Distance(pointingLocation, parentObject.transform.position);

            referenceLocation = pointingLocation;

            float difference = pointingDistance - startingDistance;
            Debug.Log("PointingDistance: " + pointingDistance + "Starting Distance: " + startingDistance + " Distance: " + difference);

            float x = parentObject.transform.localScale.x + (difference * sensitivity);
            float y = parentObject.transform.localScale.y + (difference * sensitivity);
            float z = parentObject.transform.localScale.z + (difference * sensitivity);


            if (x > 0 && y > 0 && z > 0)
                parentObject.transform.localScale = new Vector3(x, y, z);

            startingDistance = pointingDistance;
        }

        //Fixes relative location and scale to parent when grabbed.
        private void FixLocationAndScale() {
            this.transform.localScale = parentObject.transform.localScale * 0.125f;

            //float parX = parentObject.transform.position.x;
            //float parY = 
            Vector3 position = parentObject.transform.TransformPoint(new Vector3(relX, relY, relZ));

            this.transform.position = position;
        }
    }
}
