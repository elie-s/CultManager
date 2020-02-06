// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""God"",
            ""id"": ""14acc674-c8f3-4831-9ae0-09abe5b26b87"",
            ""actions"": [
                {
                    ""name"": ""Touch0"",
                    ""type"": ""Value"",
                    ""id"": ""7c706cc4-d551-4dbd-8e64-3e42e2fc14dc"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""95b6032f-1829-4359-a493-d32fe756e62f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Touch0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // God
        m_God = asset.FindActionMap("God", throwIfNotFound: true);
        m_God_Touch0 = m_God.FindAction("Touch0", throwIfNotFound: true);
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

    // God
    private readonly InputActionMap m_God;
    private IGodActions m_GodActionsCallbackInterface;
    private readonly InputAction m_God_Touch0;
    public struct GodActions
    {
        private @Controls m_Wrapper;
        public GodActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Touch0 => m_Wrapper.m_God_Touch0;
        public InputActionMap Get() { return m_Wrapper.m_God; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GodActions set) { return set.Get(); }
        public void SetCallbacks(IGodActions instance)
        {
            if (m_Wrapper.m_GodActionsCallbackInterface != null)
            {
                @Touch0.started -= m_Wrapper.m_GodActionsCallbackInterface.OnTouch0;
                @Touch0.performed -= m_Wrapper.m_GodActionsCallbackInterface.OnTouch0;
                @Touch0.canceled -= m_Wrapper.m_GodActionsCallbackInterface.OnTouch0;
            }
            m_Wrapper.m_GodActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Touch0.started += instance.OnTouch0;
                @Touch0.performed += instance.OnTouch0;
                @Touch0.canceled += instance.OnTouch0;
            }
        }
    }
    public GodActions @God => new GodActions(this);
    public interface IGodActions
    {
        void OnTouch0(InputAction.CallbackContext context);
    }
}
