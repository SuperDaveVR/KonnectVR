using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace mfitzer.Interactions
{
    public class Keyboard : MonoBehaviour
    {
        private static Keyboard instance;
        public static Keyboard Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<Keyboard>();
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        [SerializeField]
        private TMP_InputField inputField;

        [SerializeField]
        private EventSystem eventSystem;

        [SerializeField]
        private PlayerVRCheck XRRig;

        [SerializeField]
        private GameObject LeftDrumstick;
        [SerializeField]
        private GameObject RightDrumstick;

        private bool EnableOnInput = true;

        private const string SHIFT = "↑";
        private const string SYMBOL = "!#1";
        private const string ALTERNATE_SYMBOL = "ABC";
        private const string RETURN = "↵";
        private const string BACK = "←";

        private bool capital = false;

        // Awake is called before the first frame update
        void Awake()
        {
            Debug.Log("Keyboard Awake");
            GetEventSystem();
            VRHeadsetCheck();

            GameObject rightHand = GameObject.Find("XR Rig/Camera Offset/RightHand Controller");
            if (rightHand.transform.localPosition.x == 0)
            {
                Destroy(GameObject.Find("XR Rig&Tablet/Tablet(Updated)/Punchkeyboard"));
                Destroy(GameObject.Find("XR Rig/Camera Offset/LeftHand Controller/Drumstick Offset"));
                Destroy(GameObject.Find("XR Rig/Camera Offset/RightHand Controller/Drumstick Offset"));
            }
        }

        private void GetEventSystem()
        {
            if (!eventSystem)
            {
                eventSystem = FindObjectOfType<EventSystem>();
                if (!eventSystem)
                    eventSystem = new GameObject("Event System").AddComponent<EventSystem>();
            }
        }

        private void VRHeadsetCheck()
        {
            if (XRRig != null)
            {
                if (HeadsetManager.Instance.VRHeadset)
                {
                    EnableOnInput = true;
                }
                else
                {
                    EnableOnInput = false;
                }
            }
        }

        public void onKeyPress(string value)
        {
            //Handle special cases
            switch (value)
            {
                case SHIFT:
                    onShiftPress();
                    return;
                case SYMBOL:
                    onSymbolPress();
                    return;
                case ALTERNATE_SYMBOL:
                    onSymbolPress();
                    return;
                case RETURN:
                    onReturnPress();
                    return;
                case BACK:
                    onBackPress();
                    return;
            }

            if (capital)
                value = value.ToUpper();
            else
                value = value.ToLower();

            setInputFieldText(inputField.text + value);
        }

        private void onShiftPress()
        {
            capital = !capital;
        }

        private void onSymbolPress() { }

        private void onReturnPress()
        {
            //End the edit on the input field
            //eventSystem.SetSelectedGameObject(null);
        }

        private void onBackPress()
        {
            //Input field is not empty, delete last char
            if (inputField.text.Length > 0)
                setInputFieldText(inputField.text.Remove(inputField.text.Length - 1));
        }

        private void setInputFieldText(string text)
        {
            inputField.text = text;
            inputField.caretPosition = text.Length;
            inputField.ForceLabelUpdate();
        }

        public void setField(TMP_InputField textbox)
        {
            inputField = textbox;
        }

        private void OnEnable()
        {
            if (!EnableOnInput) {
                this.gameObject.SetActive(false);
            } else
            {
                EnableDrumsticks(true);
            }
        }

        private void OnDisable()
        {
            EnableDrumsticks(false);
        }

        private void EnableDrumsticks(bool enable)
        {
            if(LeftDrumstick != null)
            {
                LeftDrumstick.SetActive(enable);
            }

            if(RightDrumstick != null)
            {
                RightDrumstick.SetActive(enable);
            }
        }

        public void ActivateKeyboard(TMP_InputField textbox)
        {
            this.gameObject.SetActive(true);
            setField(textbox);
        }

        public void DeactivateKeyboard()
        {
            this.gameObject.SetActive(false);
        }
    }
}