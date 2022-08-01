using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace InteractionSystem
{
    public class PhysicsButton : MonoBehaviour
    {
        [Tooltip("Back of the button, used for determining a button press.")]
        public Transform buttonBack;

        /// <summary>
        /// Indicates if the button is currently pressed.
        /// </summary>
        public bool pressed { get; private set; } = false;

        /// <summary>
        /// Called when the button reaches the pressed state.
        /// </summary>
        public UnityEvent OnButtonPress;

        /// <summary>
        /// Called when the button leaves the pressed state.
        /// </summary>
        public UnityEvent OnButtonRelease;

        private void OnTriggerEnter(Collider other)
        {
            //Button has been pressed
            if (other.transform == buttonBack)
            {
                handleButtonPress();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Button has been released
            if (other.transform == buttonBack)
            {
                handleButtonRelease();
            }
        }

        private void handleButtonPress()
        {
            pressed = true;
            OnButtonPress.Invoke();
        }

        private void handleButtonRelease()
        {
            pressed = false;
            OnButtonRelease.Invoke();
        }
    }
}