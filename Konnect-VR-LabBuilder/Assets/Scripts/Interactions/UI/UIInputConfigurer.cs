using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace KonnectVR
{
    public class UIInputConfigurer : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private EventSystem eventSystem;

        [SerializeField, ReadOnlyField]
        private XRUIInputModule xrUIInputModule;

        [SerializeField, ReadOnlyField]
        private TrackedDeviceGraphicRaycaster xrGraphicRaycaster;

        [SerializeField, ReadOnlyField]
        private InputSystemUIInputModule inputSystemUIInputModule;

        [SerializeField, ReadOnlyField]
        private GraphicRaycaster graphicRaycaster;

        private void OnValidate()
        {
            if (!eventSystem)
            {
                eventSystem = FindObjectOfType<EventSystem>();
                if (!eventSystem)
                    eventSystem = new GameObject("Event System").AddComponent<EventSystem>();
            }

            if (!xrUIInputModule)
            {
                xrUIInputModule = eventSystem.GetComponent<XRUIInputModule>();
                if (!xrUIInputModule)
                    xrUIInputModule = eventSystem.gameObject.AddComponent<XRUIInputModule>();
            }

            if (!xrGraphicRaycaster && canvas)
            {
                xrGraphicRaycaster = canvas.GetComponent<TrackedDeviceGraphicRaycaster>();
                if (!xrGraphicRaycaster)
                    xrGraphicRaycaster = canvas.gameObject.AddComponent<TrackedDeviceGraphicRaycaster>();
            }

            if (!inputSystemUIInputModule)
            {
                inputSystemUIInputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
                if (!inputSystemUIInputModule)
                    inputSystemUIInputModule = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
            }

            if (!graphicRaycaster && canvas)
            {
                graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
                if (!graphicRaycaster)
                    graphicRaycaster = canvas.gameObject.AddComponent<GraphicRaycaster>();
            }
        }

        private void Start()
        {
            if (!eventSystem)
            {
                eventSystem = FindObjectOfType<EventSystem>();
                if (!eventSystem)
                    eventSystem = new GameObject("Event System").AddComponent<EventSystem>();
            }

            //Configure UI inputs depending on if XR is enabled

            if (!xrUIInputModule)
            {
                xrUIInputModule = eventSystem.GetComponent<XRUIInputModule>();
                if (!xrUIInputModule)
                    xrUIInputModule = eventSystem.gameObject.AddComponent<XRUIInputModule>();
            }
            xrUIInputModule.enabled = XRSettings.enabled;

            if (!xrGraphicRaycaster && canvas)
            {
                xrGraphicRaycaster = canvas.GetComponent<TrackedDeviceGraphicRaycaster>();
                if (!xrGraphicRaycaster)
                    xrGraphicRaycaster = canvas.gameObject.AddComponent<TrackedDeviceGraphicRaycaster>();
            }
            xrGraphicRaycaster.enabled = XRSettings.enabled;

            if (!inputSystemUIInputModule)
            {
                inputSystemUIInputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
                if (!inputSystemUIInputModule)
                    inputSystemUIInputModule = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
            }
            inputSystemUIInputModule.enabled = !XRSettings.enabled;

            if (!graphicRaycaster && canvas)
            {
                graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
                if (!graphicRaycaster)
                    graphicRaycaster = canvas.gameObject.AddComponent<GraphicRaycaster>();
            }
            graphicRaycaster.enabled = !XRSettings.enabled;
        }
    }
}