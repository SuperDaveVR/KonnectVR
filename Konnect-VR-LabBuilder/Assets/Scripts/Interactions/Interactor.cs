using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace KonnectVR.Interactions
{
    [System.Serializable]
    public class InteractableEvent : UnityEvent<Interactable> { }

    [RequireComponent(typeof(XRBaseInteractor))]
    public class Interactor : MonoBehaviour
    {
        public XRBaseInteractor xrInteractor { get; private set; }

        [SerializeField]
        private InteractableEvent onInteractionStarted;

        public InteractableEvent OnInteractionStarted
        {
            get => onInteractionStarted;
        }

        [SerializeField]
        private InteractableEvent onInteractionStopped;

        public InteractableEvent OnInteractionStopped
        {
            get => onInteractionStopped;
        }

        public bool interacting { get; private set; }

        private void Start()
        {
            xrInteractor = GetComponent<XRBaseInteractor>();
            xrInteractor.enableInteractions = true;

            xrInteractor.onSelectEntered.AddListener(handleInteractionStarted);
            xrInteractor.onSelectExited.AddListener(handleInteractionStopped);
        }

        private void handleInteractionStarted(XRBaseInteractable xrInteractable)
        {
            startInteraction(xrInteractable.GetComponent<Interactable>());
        }

        public void startInteraction(Interactable interactable)
        {
            if (interactable)
            {
                interacting = true;
                onInteractionStarted.Invoke(interactable);
            }
        }

        private void handleInteractionStopped(XRBaseInteractable xrInteractable)
        {
            if (xrInteractable)
                stopInteraction(xrInteractable.GetComponent<Interactable>());
        }

        public void stopInteraction(Interactable interactable)
        {
            if (interactable)
            {
                interacting = false;
                OnInteractionStopped.Invoke(interactable);
            }
        }
    }
}