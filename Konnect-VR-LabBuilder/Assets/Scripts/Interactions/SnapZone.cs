using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

namespace KonnectVR.Interactions
{
    [RequireComponent(typeof(XRSocketInteractor))]
    [RequireComponent(typeof(Interactor))]
    public class SnapZone : MonoBehaviour
    {
        //Correct gameobject for the snapzone
        public GameObject CorrectObject;

        public bool AllowIncorrect;

        //Objects that will cause a failure
        public List<GameObject> FailObjects;

        //object currently in snapzone
        private GameObject ObjectInZone;

        //This Socket Interactor
        XRSocketInteractor ThisSocket;

        //SnapZone Events
        public UnityEvent CorrectEnterEvent;
        public UnityEvent CorrectExitEvent;
        public UnityEvent CorrectStayEvent;
        public UnityEvent IncorrectEnterEvent;
        public UnityEvent IncorrectExitEvent;
        public UnityEvent IncorrectStayEvent;

        // Start is called before the first frame update
        void Start()
        {
            ThisSocket = GetComponent<XRSocketInteractor>(); 
            //if(CorrectObject != null)
            //    AllowedObjects.Add(CorrectObject.GetComponent<XRBaseInteractable>());
            gameObject.GetComponent<XRSocketInteractor>().showInteractableHoverMeshes = false;
            //gameObject.GetComponent<XRSocketInteractor>().GetValidTargets(AllowedObjects);
        }

        //object has entered the snapzone
        private void OnTriggerEnter(Collider other)
        {
            //if(other.gameObject)
            if (other.gameObject == CorrectObject)
            {
                OnCorrect(other.gameObject);
                CorrectEnterEvent.Invoke();
                ThisSocket.enabled = true;
            }
            else
            {
                OnIncorrect(other.gameObject);
                IncorrectEnterEvent.Invoke();
            }
                
        }

        //Correct object has been put into the snap zone
        private void OnCorrect(GameObject objectPlaced)
        {
            ObjectInZone = objectPlaced;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        
        }

        //Incorrect object has been put into the snap zone
        private void OnIncorrect(GameObject objectPlaced)
        {
            if (AllowIncorrect)
                ObjectInZone = objectPlaced;
            if (!AllowIncorrect && ObjectInZone == null)
            {
                ThisSocket.enabled = false;
            }
            if (FailObjects.Contains(objectPlaced))
                OnFail();
        }

        private void OnFail()
        {
            //send "incorrect object in snapzone" to fail tracker
        }

        private void OnTriggerExit(Collider other)
        {
            if (ObjectInZone == other.gameObject)
                ObjectInZone = null;
            if(other.gameObject == CorrectObject)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                CorrectExitEvent.Invoke();
            }
            else if(other.gameObject != CorrectObject)
            {
                ThisSocket.enabled = true;
                IncorrectExitEvent.Invoke();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject == CorrectObject)
            {
                CorrectStayEvent.Invoke();
            }
            else if (other.gameObject != CorrectObject)
            {
                IncorrectStayEvent.Invoke();
            }
        }

    }
}


    