//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Player/PlayerInputAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputAction : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""Player1"",
            ""id"": ""71232487-cd7a-400f-8e66-10d2ef43142d"",
            ""actions"": [
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""a6fe707c-b6e7-407f-adce-af99f48887ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Bomb"",
                    ""type"": ""Button"",
                    ""id"": ""5a642a6c-9410-46f9-8fbf-a57ad0ceb44b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Anchor"",
                    ""type"": ""Button"",
                    ""id"": ""ccff2a03-e072-4e7c-b7f8-2fe5fd301a70"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""966bf4b6-04c7-4c9e-a72f-3a6d89f17982"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""81926e8d-665d-40e9-b00a-ae77e6abb051"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Swing"",
                    ""type"": ""Value"",
                    ""id"": ""96f228f5-c418-4d15-9b6a-2e50164e8912"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""7bd51c52-2ed7-41d0-8c41-4c74f4fd0e98"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ec42d6a3-44f5-4513-8620-8c6f794b8219"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4cd92cd-3d41-458c-b768-ab93e0b8e717"",
                    ""path"": ""<Keyboard>/capsLock"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf00ce5d-702d-4187-a02a-e89e0b3b392f"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Bomb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f056cb15-dc04-4d76-b58a-68b9750330f6"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7863bf4c-b3de-4fe6-930b-549b048ef4d9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""53d520fc-04ab-43a8-9e87-c727cbe9a74f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4b7e43ee-b26c-4cdf-b5bf-727066abbc3f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ea2297b8-31cd-4deb-84d9-33b492d7a539"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8faa66fa-a504-4ed6-b0d5-06bbc162f92d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e1fff4ac-b5df-4657-87d3-781e9ae4c33a"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Bomb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c13b5d3-b97c-4aa5-bffb-7a3164284dc0"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Anchor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbf58181-edcc-4e47-85fa-07f983bda5f7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Anchor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5f6f0ae-514a-484b-a972-8ff93c8d2435"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94bc0f86-cbaa-4c6e-bd84-a5322f0350e6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1103678-1f27-4454-bd22-5e75e7d0a395"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d53fffc-f2f4-4430-be5f-9a93eecb0c28"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player2"",
            ""id"": ""a8edcf8e-dcb8-4a7c-9d5b-7dae61b6935b"",
            ""actions"": [
                {
                    ""name"": ""BlockAbility"",
                    ""type"": ""Button"",
                    ""id"": ""80fd72b2-3240-4d28-acff-47eba107eccf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""StunAbility"",
                    ""type"": ""Button"",
                    ""id"": ""854cfecf-7bbe-4fb8-98b2-8b0274ce71bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""VineAbility"",
                    ""type"": ""Button"",
                    ""id"": ""a420daad-77ed-43aa-90fd-2f3c9da5f824"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""94d7ace5-624b-4dee-84cb-8c1016e83c17"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""5ce5aa4a-190c-486d-877e-deec8874080a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Swing"",
                    ""type"": ""Value"",
                    ""id"": ""0932f512-f4b9-4a48-a62e-70fb739dfae3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""d7c86a77-5c24-486e-8d11-2d8d44ba0fa0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8c6e4893-34b8-489a-956d-d8a9bf741dfa"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""BlockAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b7ff219e-ca37-42a7-9c7a-9b77986a2017"",
                    ""path"": ""<Keyboard>/capsLock"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""BlockAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c55f3b3c-452d-4bbb-b293-b73546da05bf"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""StunAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e44a0875-d429-4bed-b952-2e8642eed1dc"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4a6dd93c-c0ad-4725-83b7-ae2f03309fd8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4f8c957a-1183-4e1a-b8fa-67ddb10b0c2b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bf117fc3-2c78-4865-8e4c-308e76031636"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fc6b091d-8d3c-4bcf-9d9e-d1e130d2112a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""928fd983-054e-4069-8f96-037ebe5775ae"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e9087153-2831-4de7-b5ef-65a52c7f74d3"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""StunAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8db94151-f755-42a1-80a5-c01943cee6d2"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""297e393a-e3b6-46b5-b444-e8abe4723fdd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b088333c-5662-4d84-b940-d38f30320ddd"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36c9667f-859e-475d-bfbc-db4a56fa0432"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44b082bf-b421-4c3c-b9fc-88aae8477011"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""VineAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19046bcb-993e-4d7a-9b5b-0a3d079d5ea3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""VineAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player1
        m_Player1 = asset.FindActionMap("Player1", throwIfNotFound: true);
        m_Player1_Dash = m_Player1.FindAction("Dash", throwIfNotFound: true);
        m_Player1_Bomb = m_Player1.FindAction("Bomb", throwIfNotFound: true);
        m_Player1_Anchor = m_Player1.FindAction("Anchor", throwIfNotFound: true);
        m_Player1_Move = m_Player1.FindAction("Move", throwIfNotFound: true);
        m_Player1_Jump = m_Player1.FindAction("Jump", throwIfNotFound: true);
        m_Player1_Swing = m_Player1.FindAction("Swing", throwIfNotFound: true);
        m_Player1_Aim = m_Player1.FindAction("Aim", throwIfNotFound: true);
        // Player2
        m_Player2 = asset.FindActionMap("Player2", throwIfNotFound: true);
        m_Player2_BlockAbility = m_Player2.FindAction("BlockAbility", throwIfNotFound: true);
        m_Player2_StunAbility = m_Player2.FindAction("StunAbility", throwIfNotFound: true);
        m_Player2_VineAbility = m_Player2.FindAction("VineAbility", throwIfNotFound: true);
        m_Player2_Move = m_Player2.FindAction("Move", throwIfNotFound: true);
        m_Player2_Jump = m_Player2.FindAction("Jump", throwIfNotFound: true);
        m_Player2_Swing = m_Player2.FindAction("Swing", throwIfNotFound: true);
        m_Player2_Aim = m_Player2.FindAction("Aim", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player1
    private readonly InputActionMap m_Player1;
    private IPlayer1Actions m_Player1ActionsCallbackInterface;
    private readonly InputAction m_Player1_Dash;
    private readonly InputAction m_Player1_Bomb;
    private readonly InputAction m_Player1_Anchor;
    private readonly InputAction m_Player1_Move;
    private readonly InputAction m_Player1_Jump;
    private readonly InputAction m_Player1_Swing;
    private readonly InputAction m_Player1_Aim;
    public struct Player1Actions
    {
        private @PlayerInputAction m_Wrapper;
        public Player1Actions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Dash => m_Wrapper.m_Player1_Dash;
        public InputAction @Bomb => m_Wrapper.m_Player1_Bomb;
        public InputAction @Anchor => m_Wrapper.m_Player1_Anchor;
        public InputAction @Move => m_Wrapper.m_Player1_Move;
        public InputAction @Jump => m_Wrapper.m_Player1_Jump;
        public InputAction @Swing => m_Wrapper.m_Player1_Swing;
        public InputAction @Aim => m_Wrapper.m_Player1_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterface != null)
            {
                @Dash.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnDash;
                @Bomb.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnBomb;
                @Bomb.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnBomb;
                @Bomb.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnBomb;
                @Anchor.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAnchor;
                @Anchor.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAnchor;
                @Anchor.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAnchor;
                @Move.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnJump;
                @Swing.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSwing;
                @Swing.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSwing;
                @Swing.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSwing;
                @Aim.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_Player1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Bomb.started += instance.OnBomb;
                @Bomb.performed += instance.OnBomb;
                @Bomb.canceled += instance.OnBomb;
                @Anchor.started += instance.OnAnchor;
                @Anchor.performed += instance.OnAnchor;
                @Anchor.canceled += instance.OnAnchor;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Swing.started += instance.OnSwing;
                @Swing.performed += instance.OnSwing;
                @Swing.canceled += instance.OnSwing;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);

    // Player2
    private readonly InputActionMap m_Player2;
    private IPlayer2Actions m_Player2ActionsCallbackInterface;
    private readonly InputAction m_Player2_BlockAbility;
    private readonly InputAction m_Player2_StunAbility;
    private readonly InputAction m_Player2_VineAbility;
    private readonly InputAction m_Player2_Move;
    private readonly InputAction m_Player2_Jump;
    private readonly InputAction m_Player2_Swing;
    private readonly InputAction m_Player2_Aim;
    public struct Player2Actions
    {
        private @PlayerInputAction m_Wrapper;
        public Player2Actions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @BlockAbility => m_Wrapper.m_Player2_BlockAbility;
        public InputAction @StunAbility => m_Wrapper.m_Player2_StunAbility;
        public InputAction @VineAbility => m_Wrapper.m_Player2_VineAbility;
        public InputAction @Move => m_Wrapper.m_Player2_Move;
        public InputAction @Jump => m_Wrapper.m_Player2_Jump;
        public InputAction @Swing => m_Wrapper.m_Player2_Swing;
        public InputAction @Aim => m_Wrapper.m_Player2_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Player2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player2Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer2Actions instance)
        {
            if (m_Wrapper.m_Player2ActionsCallbackInterface != null)
            {
                @BlockAbility.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnBlockAbility;
                @BlockAbility.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnBlockAbility;
                @BlockAbility.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnBlockAbility;
                @StunAbility.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnStunAbility;
                @StunAbility.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnStunAbility;
                @StunAbility.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnStunAbility;
                @VineAbility.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnVineAbility;
                @VineAbility.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnVineAbility;
                @VineAbility.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnVineAbility;
                @Move.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnJump;
                @Swing.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnSwing;
                @Swing.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnSwing;
                @Swing.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnSwing;
                @Aim.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_Player2ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @BlockAbility.started += instance.OnBlockAbility;
                @BlockAbility.performed += instance.OnBlockAbility;
                @BlockAbility.canceled += instance.OnBlockAbility;
                @StunAbility.started += instance.OnStunAbility;
                @StunAbility.performed += instance.OnStunAbility;
                @StunAbility.canceled += instance.OnStunAbility;
                @VineAbility.started += instance.OnVineAbility;
                @VineAbility.performed += instance.OnVineAbility;
                @VineAbility.canceled += instance.OnVineAbility;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Swing.started += instance.OnSwing;
                @Swing.performed += instance.OnSwing;
                @Swing.canceled += instance.OnSwing;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public Player2Actions @Player2 => new Player2Actions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IPlayer1Actions
    {
        void OnDash(InputAction.CallbackContext context);
        void OnBomb(InputAction.CallbackContext context);
        void OnAnchor(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSwing(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
    public interface IPlayer2Actions
    {
        void OnBlockAbility(InputAction.CallbackContext context);
        void OnStunAbility(InputAction.CallbackContext context);
        void OnVineAbility(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSwing(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
}
