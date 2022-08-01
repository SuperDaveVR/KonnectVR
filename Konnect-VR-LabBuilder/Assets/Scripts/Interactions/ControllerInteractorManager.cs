using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace KonnectVR.Interactions
{
    public class ControllerInteractorManager : MonoBehaviour
    {
        [SerializeField]
        private XRDirectInteractor leftDirectInteractor;
        public XRDirectInteractor LeftDirectInteractor
        {
            get
            {
                return leftDirectInteractor;
            }
            set
            {
                if (value && value.isActiveAndEnabled)
                {
                    //Currently subscribed to valid interactor, unsubscribe
                    if (leftDirectInteractor && leftDirectInteractor.isActiveAndEnabled)
                    {
                        leftDirectInteractor.onSelectEntered.RemoveListener(handleLeftDirectInteractorSelectEntered);
                        leftDirectInteractor.onSelectExited.RemoveListener(handleLeftDirectInteractorSelectExited);
                    }

                    //Subscribe to selection events on new interactor
                    leftDirectInteractor = value;
                    leftDirectInteractor.onSelectEntered.AddListener(handleLeftDirectInteractorSelectEntered);
                    leftDirectInteractor.onSelectExited.AddListener(handleLeftDirectInteractorSelectExited);
                }
            }
        }
        private bool leftDirectSelecting = false;

        [SerializeField]
        private XRRayInteractor leftRayInteractor;
        public XRRayInteractor LeftRayInteractor
        {
            get
            {
                return leftRayInteractor;
            }
            set
            {
                if (value && value.isActiveAndEnabled)
                {
                    leftRayInteractor = value;
                }
            }
        }
        private bool leftRayActive = false;

        [SerializeField]
        private XRDirectInteractor rightDirectInteractor;
        public XRDirectInteractor RightDirectInteractor
        {
            get
            {
                return rightDirectInteractor;
            }
            set
            {
                if (value && value.isActiveAndEnabled)
                {
                    //Currently subscribed to valid interactor, unsubscribe
                    if (rightDirectInteractor && rightDirectInteractor.isActiveAndEnabled)
                    {
                        rightDirectInteractor.onSelectEntered.RemoveListener(handleRightDirectInteractorSelectEntered);
                        rightDirectInteractor.onSelectExited.RemoveListener(handleRightDirectInteractorSelectExited);
                    }

                    //Subscribe to selection events on new interactor
                    rightDirectInteractor = value;
                    rightDirectInteractor.onSelectEntered.AddListener(handleRightDirectInteractorSelectEntered);
                    rightDirectInteractor.onSelectExited.AddListener(handleRightDirectInteractorSelectExited);
                }
            }
        }
        private bool rightDirectSelecting = false;

        [SerializeField]
        private XRRayInteractor rightRayInteractor;
        public XRRayInteractor RightRayInteractor
        {
            get
            {
                return rightRayInteractor;
            }
            set
            {
                if (value && value.isActiveAndEnabled)
                {
                    rightRayInteractor = value;
                }
            }
        }
        private bool rightRayActive = false;

        private void Start()
        {
            if (leftDirectInteractor && leftDirectInteractor.isActiveAndEnabled)
            {
                leftDirectInteractor.onSelectEntered.AddListener(handleLeftDirectInteractorSelectEntered);
                leftDirectInteractor.onSelectExited.AddListener(handleLeftDirectInteractorSelectExited);
            }

            if (rightDirectInteractor && rightDirectInteractor.isActiveAndEnabled)
            {
                rightDirectInteractor.onSelectEntered.AddListener(handleRightDirectInteractorSelectEntered);
                rightDirectInteractor.onSelectExited.AddListener(handleRightDirectInteractorSelectExited);
            }
        }

        public void OnToggleLeftRayInteractor()
        {
            //Only allow player to toggle if direct interactor select is inactive
            if (!leftDirectSelecting)
                leftRayInteractor.gameObject.SetActive(!leftRayInteractor.gameObject.activeSelf);
        }

        private void handleLeftDirectInteractorSelectEntered(XRBaseInteractable interactable)
        {
            leftDirectSelecting = true;

            //Turn off ray interactor
            leftRayActive = leftRayInteractor.gameObject.activeSelf;
            if (leftRayActive)
                leftRayInteractor.gameObject.SetActive(false);
        }

        private void handleLeftDirectInteractorSelectExited(XRBaseInteractable interactable)
        {
            leftDirectSelecting = false;

            if (leftRayActive) //Reactivate ray
                leftRayInteractor.gameObject.SetActive(true);
        }

        public void OnToggleRightRayInteractor()
        {
            //Only allow player to toggle if direct interactor select is inactive
            if (!rightDirectSelecting)
                rightRayInteractor.gameObject.SetActive(!rightRayInteractor.gameObject.activeSelf);
        }

        private void handleRightDirectInteractorSelectEntered(XRBaseInteractable interactable)
        {
            rightDirectSelecting = true;

            //Turn off ray interactor
            rightRayActive = rightRayInteractor.gameObject.activeSelf;
            if (rightRayActive)
                rightRayInteractor.gameObject.SetActive(false);
        }

        private void handleRightDirectInteractorSelectExited(XRBaseInteractable interactable)
        {
            rightDirectSelecting = false;

            if (rightRayActive) //Reactivate ray
                rightRayInteractor.gameObject.SetActive(true);
        }
    }
}