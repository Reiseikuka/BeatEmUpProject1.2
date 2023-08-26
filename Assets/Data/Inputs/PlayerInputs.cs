// GENERATED AUTOMATICALLY FROM 'Assets/Data/Inputs/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""Player1"",
            ""id"": ""7b8c53e3-3a5f-4fb1-8a23-18fa896f7889"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""eec28aa6-3872-4997-9288-fedacc06f7d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""99f22704-fbae-4158-81e0-aa049998eede"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""f254e439-033b-4db0-8486-e27473a69956"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""dd6516f5-398b-4d32-a6f3-af4f52ef51e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack 1"",
                    ""type"": ""Button"",
                    ""id"": ""bee78402-ff85-4777-a9b0-e7e74e2ad630"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack 2"",
                    ""type"": ""Button"",
                    ""id"": ""74ad4509-e685-4264-a05d-738109816bfc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d10e6025-afc0-4ad1-8090-4c858d605de0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b3dc7f26-6040-4281-9460-5396eed037f2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0837a8a1-afef-4312-a009-f010d93aec7a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1e75d34-28c2-478d-a255-d7ee1519d6cd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f53b428f-b66c-4fd0-a9b9-edfbf2d3330b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a15bbb1f-39b5-4a50-86cd-43a68f7d0461"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8425c818-1c8a-4074-8738-68b8e1c659cc"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6d98976-02a1-4c96-aa96-0725644af70e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player2"",
            ""id"": ""6c6959b3-dfe4-43a9-be9a-0d6afebfc0ca"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""4fd10cb5-dcd2-4aa3-bdcb-0f1c62e15fad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""c1b18fac-d585-40d9-ab3d-f1ff9455b2d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""f3499475-67f0-45c6-8fa3-b4fee95e1d6a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""c0f4ae8e-74db-4782-ab83-6743d4931390"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack 1"",
                    ""type"": ""Button"",
                    ""id"": ""5cd43e9e-f511-436c-ba98-7ee0d2480190"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack 2"",
                    ""type"": ""Button"",
                    ""id"": ""61525292-5ef7-4403-9f83-5d14d0c9c8d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""3c13d26a-db46-4de8-b46d-ff344bca0774"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""08188b08-4eff-4b61-84d9-f60e817e7585"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58b2ebb0-458f-422c-b9c1-8e6a18f74201"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3bfd3bea-4205-48bb-95f9-555e019da251"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90cddfe0-16ec-4ace-83d8-3ad9389d31e9"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""556c0c54-0f76-4540-bce3-fa4d81e73b01"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5444536a-8809-4ab3-982b-c7f4cd7adb96"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed947199-18ae-42d8-b088-d7d9731e35cf"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        }
    ]
}");
        // Player1
        m_Player1 = asset.FindActionMap("Player1", throwIfNotFound: true);
        m_Player1_Up = m_Player1.FindAction("Up", throwIfNotFound: true);
        m_Player1_Down = m_Player1.FindAction("Down", throwIfNotFound: true);
        m_Player1_Left = m_Player1.FindAction("Left", throwIfNotFound: true);
        m_Player1_Right = m_Player1.FindAction("Right", throwIfNotFound: true);
        m_Player1_Attack1 = m_Player1.FindAction("Attack 1", throwIfNotFound: true);
        m_Player1_Attack2 = m_Player1.FindAction("Attack 2", throwIfNotFound: true);
        m_Player1_Jump = m_Player1.FindAction("Jump", throwIfNotFound: true);
        // Player2
        m_Player2 = asset.FindActionMap("Player2", throwIfNotFound: true);
        m_Player2_Up = m_Player2.FindAction("Up", throwIfNotFound: true);
        m_Player2_Down = m_Player2.FindAction("Down", throwIfNotFound: true);
        m_Player2_Left = m_Player2.FindAction("Left", throwIfNotFound: true);
        m_Player2_Right = m_Player2.FindAction("Right", throwIfNotFound: true);
        m_Player2_Attack1 = m_Player2.FindAction("Attack 1", throwIfNotFound: true);
        m_Player2_Attack2 = m_Player2.FindAction("Attack 2", throwIfNotFound: true);
        m_Player2_Jump = m_Player2.FindAction("Jump", throwIfNotFound: true);
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

    // Player1
    private readonly InputActionMap m_Player1;
    private IPlayer1Actions m_Player1ActionsCallbackInterface;
    private readonly InputAction m_Player1_Up;
    private readonly InputAction m_Player1_Down;
    private readonly InputAction m_Player1_Left;
    private readonly InputAction m_Player1_Right;
    private readonly InputAction m_Player1_Attack1;
    private readonly InputAction m_Player1_Attack2;
    private readonly InputAction m_Player1_Jump;
    public struct Player1Actions
    {
        private @PlayerInputs m_Wrapper;
        public Player1Actions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_Player1_Up;
        public InputAction @Down => m_Wrapper.m_Player1_Down;
        public InputAction @Left => m_Wrapper.m_Player1_Left;
        public InputAction @Right => m_Wrapper.m_Player1_Right;
        public InputAction @Attack1 => m_Wrapper.m_Player1_Attack1;
        public InputAction @Attack2 => m_Wrapper.m_Player1_Attack2;
        public InputAction @Jump => m_Wrapper.m_Player1_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterface != null)
            {
                @Up.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnRight;
                @Attack1.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAttack1;
                @Attack1.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAttack1;
                @Attack1.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAttack1;
                @Attack2.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAttack2;
                @Attack2.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAttack2;
                @Attack2.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAttack2;
                @Jump.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_Player1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Attack1.started += instance.OnAttack1;
                @Attack1.performed += instance.OnAttack1;
                @Attack1.canceled += instance.OnAttack1;
                @Attack2.started += instance.OnAttack2;
                @Attack2.performed += instance.OnAttack2;
                @Attack2.canceled += instance.OnAttack2;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);

    // Player2
    private readonly InputActionMap m_Player2;
    private IPlayer2Actions m_Player2ActionsCallbackInterface;
    private readonly InputAction m_Player2_Up;
    private readonly InputAction m_Player2_Down;
    private readonly InputAction m_Player2_Left;
    private readonly InputAction m_Player2_Right;
    private readonly InputAction m_Player2_Attack1;
    private readonly InputAction m_Player2_Attack2;
    private readonly InputAction m_Player2_Jump;
    public struct Player2Actions
    {
        private @PlayerInputs m_Wrapper;
        public Player2Actions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_Player2_Up;
        public InputAction @Down => m_Wrapper.m_Player2_Down;
        public InputAction @Left => m_Wrapper.m_Player2_Left;
        public InputAction @Right => m_Wrapper.m_Player2_Right;
        public InputAction @Attack1 => m_Wrapper.m_Player2_Attack1;
        public InputAction @Attack2 => m_Wrapper.m_Player2_Attack2;
        public InputAction @Jump => m_Wrapper.m_Player2_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Player2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player2Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer2Actions instance)
        {
            if (m_Wrapper.m_Player2ActionsCallbackInterface != null)
            {
                @Up.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnRight;
                @Attack1.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnAttack1;
                @Attack1.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnAttack1;
                @Attack1.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnAttack1;
                @Attack2.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnAttack2;
                @Attack2.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnAttack2;
                @Attack2.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnAttack2;
                @Jump.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_Player2ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Attack1.started += instance.OnAttack1;
                @Attack1.performed += instance.OnAttack1;
                @Attack1.canceled += instance.OnAttack1;
                @Attack2.started += instance.OnAttack2;
                @Attack2.performed += instance.OnAttack2;
                @Attack2.canceled += instance.OnAttack2;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public Player2Actions @Player2 => new Player2Actions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayer1Actions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnAttack1(InputAction.CallbackContext context);
        void OnAttack2(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IPlayer2Actions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnAttack1(InputAction.CallbackContext context);
        void OnAttack2(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
