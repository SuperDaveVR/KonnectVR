using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace KonnectVR.Interactions
{
    [System.Serializable]
    public class InteractorEvent : UnityEvent<Interactor> { }

    [RequireComponent(typeof(XRBaseInteractable))]
    public class Interactable : MonoBehaviour
    {
        public XRBaseInteractable xrInteractable { get; private set; }

        public bool interacting { get; private set; } = false;

        /// <summary>
        /// Was the current interaction forced?
        /// </summary>
        public bool interactionForced { get; private set; } = false;
        
        [ReadOnlyField]
        public bool isSelected = false;

        /// <summary>
        /// How many interactions have been performed.
        /// </summary>
        public int interactions { get; private set; } = 0;

        /// <summary>
        /// How many activations have been performed.
        /// </summary>
        public int activations { get; private set; } = 0;

        /// <summary>
        /// The interactor, if any, that is currently interacting.
        /// </summary>
        public Interactor interactingInteractor { get; private set; }

        #region Start/Stop

        [SerializeField]
        private InteractorEvent onInteractionStarted;
        public InteractorEvent OnInteractionStarted
        {
            get => onInteractionStarted;
        }

        [SerializeField]
        private UnityEvent onIndependentInteractionStarted;
        public UnityEvent OnIndependentInteractionStarted
        {
            get => onIndependentInteractionStarted;
        }

        [SerializeField]
        private InteractorEvent onInteractionStopped;
        public InteractorEvent OnInteractionStopped
        {
            get => onInteractionStopped;
        }

        [SerializeField]
        private UnityEvent onIndependentInteractionStopped;
        public UnityEvent OnIndependentInteractionStopped
        {
            get => onIndependentInteractionStopped;
        }

        #endregion Start/Stop

        #region Activation

        [SerializeField]
        private UnityEvent onActivate;
        public UnityEvent OnActivate
        {
            get => onActivate;
        }

        [SerializeField]
        private UnityEvent onDeactivate;
        public UnityEvent OnDeactivate
        {
            get => onDeactivate;
        }

        #endregion Activation

        protected virtual void Start()
        {
            xrInteractable = GetComponent<XRBaseInteractable>();

            xrInteractable.onSelectEntered.AddListener(handleInteractionStarted);
            xrInteractable.onSelectExited.AddListener(handleInteractionStopped);
            xrInteractable.onActivate.AddListener(handleActivation);
            xrInteractable.onDeactivate.AddListener(handleDeactivation);
        }

        private void Update()
        {
            isSelected = xrInteractable.isSelected;
        }

        #region Start/Stop

        #region Start

        private void handleInteractionStarted(XRBaseInteractor xrInteractor)
        {
            Interactor interactor = xrInteractor.GetComponent<Interactor>();
            if (interactor && !interacting)
            {
                //Debug.Log("[" + interactor.transform.root.name + ", " + interactor.name + " | " + Time.time + "] started interacting with the interactable " + name);

                interactingInteractor = interactor;
                interactions++;
                interacting = true;
                onStartInteracting(interactor);
                onInteractionStarted.Invoke(interactor);
            }
        }

        protected virtual void onStartInteracting(Interactor interactor) { }

        /// <summary>
        /// Start an interaction independent of an interactor.
        /// </summary>
        public void startIndependentInteraction()
        {
            //Debug.Log("Started interacting with the interactable " + name);

            interactions++;
            interacting = true;
            onStartInteractingIndependent();
            onIndependentInteractionStarted.Invoke();
        }

        protected virtual void onStartInteractingIndependent() { }

        /// <summary>
        /// Force the start of an interaction with the given interactor.
        /// </summary>
        public virtual void forceStartInteraction(Interactor interactor)
        {
            //Debug.Log("[" + interactor.transform.root.name + ", " + name + " | " + Time.time + "] Forcing interaction start with " + interactor);
            interactionForced = true;

            onForceStartInteraction(interactor);
        }

        protected virtual void onForceStartInteraction(Interactor interactor)
        {
            //Signal to interactor that a manual interaction is about to take place
            interactor.xrInteractor.StartManualInteraction(xrInteractable);

            //Start the interaction
            interactor.xrInteractor.interactionManager.ForceSelect(interactor.xrInteractor, xrInteractable);
        }        

        #endregion Start

        #region Stop

        private void handleInteractionStopped(XRBaseInteractor xrInteractor)
        {
            Interactor interactor = xrInteractor.GetComponent<Interactor>();

            if (interactor && interacting)
            {
                //Debug.Log("[" + interactor.transform.root.name + ", " + interactor.name + " | " + Time.time + "] stopped interacting with the interactable " + name);

                interactingInteractor = null;
                interacting = false;
                onStopInteracting(interactor);
                onInteractionStopped.Invoke(interactor);
            }
        }

        protected virtual void onStopInteracting(Interactor interactor) { }

        /// <summary>
        /// Stop an interaction independent of an interactor.
        /// </summary>
        public void stopIndependentInteraction()
        {
            //Debug.Log("Stopped interacting with the interactable " + name);

            interacting = false;
            onStopInteractingIndependent();
            onIndependentInteractionStopped.Invoke();
        }

        protected virtual void onStopInteractingIndependent() { }

        /// <summary>
        /// Force the stop of an interaction with the given interactor.
        /// </summary>
        public void forceStopInteraction(Interactor interactor)
        {
            //Debug.Log("[" + interactor.transform.root.name + ", " + name + " | " + Time.time + "] Forcing interaction stop with " + interactor);
            interactionForced = false;

            onForceStopInteraction(interactor);
        }

        protected virtual void onForceStopInteraction(Interactor interactor)
        {
            //Stop the interaction
            if (interactor.xrInteractor.selectTarget == xrInteractable)
                interactor.xrInteractor.interactionManager.SelectExit(interactor.xrInteractor, xrInteractable);

            //Signal to the interactor that the manual interaction is done
            interactor.xrInteractor.EndManualInteraction();
        }

        #endregion Stop

        #endregion Start/Stop

        #region Activation

        private void handleActivation(XRBaseInteractor xrInteractor)
        {
            activate();
        }

        public void activate()
        {
            activations++;
            onActivation();
            onActivate.Invoke();

            //Debug.Log("[" + name + "] Activated");
        }

        protected virtual void onActivation() { }

        private void handleDeactivation(XRBaseInteractor xrInteractor)
        {
            deactivate();
        }

        public void deactivate()
        {
            onDeactivation();
            onDeactivate.Invoke();

            //Debug.Log("[" + name + "] Deactivated");
        }

        protected virtual void onDeactivation() { }

        #endregion Activation
    }
}