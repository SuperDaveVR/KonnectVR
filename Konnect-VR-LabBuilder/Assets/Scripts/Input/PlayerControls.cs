// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace KonnectVR.Input
{
    public class @PlayerControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""SystemActions"",
            ""id"": ""08696174-7de9-426e-9973-f9c3217c8fb0"",
            ""actions"": [
                {
                    ""name"": ""ToggleLeftRayInteractor"",
                    ""type"": ""Button"",
                    ""id"": ""3b91f167-895d-4294-ac2f-b0a11ac023e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleRightRayInteractor"",
                    ""type"": ""Button"",
                    ""id"": ""5748574e-fa47-4ff6-96fb-c54b0277e604"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TeleportTabletLeft"",
                    ""type"": ""Button"",
                    ""id"": ""d9821a9d-a752-4ae9-84b6-a35eee8256ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TeleportTabletRight"",
                    ""type"": ""Button"",
                    ""id"": ""31354a81-13ca-4b7e-a6f2-5e2f595708e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""00ae3a99-5398-44ad-b0db-9e6ba15d12d5"",
                    ""path"": ""<XRController>{RightHand}/thumbstickClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleRightRayInteractor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07119468-df46-48d4-bc00-b93e13075d95"",
                    ""path"": ""<XRController>{LeftHand}/thumbstickClicked"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleLeftRayInteractor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d648994c-4c1f-4d5e-a44e-ad4c04df5697"",
                    ""path"": ""<XRController>{LeftHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TeleportTabletLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d99165d-b41a-4a81-b741-8c428a7bd9cc"",
                    ""path"": ""<XRController>{RightHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TeleportTabletRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // SystemActions
            m_SystemActions = asset.FindActionMap("SystemActions", throwIfNotFound: true);
            m_SystemActions_ToggleLeftRayInteractor = m_SystemActions.FindAction("ToggleLeftRayInteractor", throwIfNotFound: true);
            m_SystemActions_ToggleRightRayInteractor = m_SystemActions.FindAction("ToggleRightRayInteractor", throwIfNotFound: true);
            m_SystemActions_TeleportTabletLeft = m_SystemActions.FindAction("TeleportTabletLeft", throwIfNotFound: true);
            m_SystemActions_TeleportTabletRight = m_SystemActions.FindAction("TeleportTabletRight", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // SystemActions
        private readonly InputActionMap m_SystemActions;
        private ISystemActionsActions m_SystemActionsActionsCallbackInterface;
        private readonly InputAction m_SystemActions_ToggleLeftRayInteractor;
        private readonly InputAction m_SystemActions_ToggleRightRayInteractor;
        private readonly InputAction m_SystemActions_TeleportTabletLeft;
        private readonly InputAction m_SystemActions_TeleportTabletRight;
        public struct SystemActionsActions
        {
            private @PlayerControls m_Wrapper;
            public SystemActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @ToggleLeftRayInteractor => m_Wrapper.m_SystemActions_ToggleLeftRayInteractor;
            public InputAction @ToggleRightRayInteractor => m_Wrapper.m_SystemActions_ToggleRightRayInteractor;
            public InputAction @TeleportTabletLeft => m_Wrapper.m_SystemActions_TeleportTabletLeft;
            public InputAction @TeleportTabletRight => m_Wrapper.m_SystemActions_TeleportTabletRight;
            public InputActionMap Get() { return m_Wrapper.m_SystemActions; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(SystemActionsActions set) { return set.Get(); }
            public void SetCallbacks(ISystemActionsActions instance)
            {
                if (m_Wrapper.m_SystemActionsActionsCallbackInterface != null)
                {
                    @ToggleLeftRayInteractor.started -= m_Wrapper.m_SystemActionsActionsCallbackInterface.OnToggleLeftRayInteractor;
                    @ToggleLeftRayInteractor.performed -= m_Wrapper.m_SystemActionsActionsCallbackInterface.OnToggleLeftRayInteractor;
                    @ToggleLeftRayInteractor.canceled -= m_Wrapper.m_SystemActionsActionsCallbackInterface.OnToggleLeftRayInteractor;
                    @ToggleRightRayInteractor.started -= m_Wrapper.m_SystemActionsActionsCallbackInterface.OnToggleRightRayInteractor;
                    @ToggleRightRayInteractor.performed -= m_Wrapper.m_SystemActionsActionsCallbackInterface.OnToggleRightRayInteractor;
                    @ToggleRightRayInteractor.canceled -= m_Wrapper.m_SystemActionsActionsCallbackInterface.OnToggleRightRayInteractor;
                    @TeleportTabletLeft.started -= m_Wrapper.m_SystemActionsActionsCallbackInterface.OnTeleportTabletLeft;
                    @TeleportTabletLeft.performed -= m_Wrapper.m_SystemActionsActionsCallbackInterface.OnTeleportTabletLeft;
                    @TeleportTabletLeft.canceled -= m_Wrapper.m_SystemActionsActionsCallbackInterface.OnTeleportTabletLeft;
                    @TeleportTabletRight.started -= m_Wrapper.m_SystemActionsActionsCallbackInterface.OnTeleportTabletRight;
                    @TeleportTabletRight.performed -= m_Wrapper.m_SystemActionsActionsCallbackInterface.OnTeleportTabletRight;
                    @TeleportTabletRight.canceled -= m_Wrapper.m_SystemActionsActionsCallbackInterface.OnTeleportTabletRight;
                }
                m_Wrapper.m_SystemActionsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ToggleLeftRayInteractor.started += instance.OnToggleLeftRayInteractor;
                    @ToggleLeftRayInteractor.performed += instance.OnToggleLeftRayInteractor;
                    @ToggleLeftRayInteractor.canceled += instance.OnToggleLeftRayInteractor;
                    @ToggleRightRayInteractor.started += instance.OnToggleRightRayInteractor;
                    @ToggleRightRayInteractor.performed += instance.OnToggleRightRayInteractor;
                    @ToggleRightRayInteractor.canceled += instance.OnToggleRightRayInteractor;
                    @TeleportTabletLeft.started += instance.OnTeleportTabletLeft;
                    @TeleportTabletLeft.performed += instance.OnTeleportTabletLeft;
                    @TeleportTabletLeft.canceled += instance.OnTeleportTabletLeft;
                    @TeleportTabletRight.started += instance.OnTeleportTabletRight;
                    @TeleportTabletRight.performed += instance.OnTeleportTabletRight;
                    @TeleportTabletRight.canceled += instance.OnTeleportTabletRight;
                }
            }
        }
        public SystemActionsActions @SystemActions => new SystemActionsActions(this);
        public interface ISystemActionsActions
        {
            void OnToggleLeftRayInteractor(InputAction.CallbackContext context);
            void OnToggleRightRayInteractor(InputAction.CallbackContext context);
            void OnTeleportTabletLeft(InputAction.CallbackContext context);
            void OnTeleportTabletRight(InputAction.CallbackContext context);
        }
    }
}
