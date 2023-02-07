using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player2InputScript : MonoBehaviour {
    public Player2FrameInput FrameInput { get; private set; }
    private PlayerInputAction _playerInputAction;
    private InputAction _move, _jump, _block, _attack, _vine;
    private void Awake() {
        _playerInputAction = new PlayerInputAction();
        _move = _playerInputAction.Player2.Move;
        _jump = _playerInputAction.Player2.Jump;
        _block = _playerInputAction.Player2.BlockAbility;
        _attack = _playerInputAction.Player2.StunAbility;
        _vine = _playerInputAction.Player2.VineAbility;
    }
    private void OnEnable() {
        _playerInputAction.Enable();
    }
    private void OnDisable() {
        _playerInputAction.Disable();
    }
    private void Update() {
        FrameInput = GartherFrameInput();
    }
    private Player2FrameInput GartherFrameInput() {
        return new Player2FrameInput {
            Move = _move.ReadValue<Vector2>(),
            JumpDown = _jump.WasPerformedThisFrame(),
            JumpHeld = _jump.IsPressed(),
            StunAbilityDown = _attack.WasPerformedThisFrame(),
            BlockAbilityDown = _block.WasPerformedThisFrame(),
            BlockAbilityHeld = _block.IsPressed(),
            VineAbilityDown = _vine.WasPerformedThisFrame(),
            VineAbilityHeld = _vine.IsPressed(),
        };
    }
}
public struct Player2FrameInput {
    public Vector2 Move;
    public bool JumpDown;
    public bool JumpHeld;
    public bool StunAbilityDown;
    public bool BlockAbilityDown;
    public bool BlockAbilityHeld;
    public bool VineAbilityDown;
    public bool VineAbilityHeld;
}