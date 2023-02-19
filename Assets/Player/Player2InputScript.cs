using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player2InputScript : MonoBehaviour {
    public Player2FrameInput FrameInput { get; private set; }
    //private PlayerInputAction _playerInputAction;
    //private InputAction _move, _jump, _block, _attack, _vine;
    private Vector2 _move, _aim;
    private bool _jumpDown, _jumpHeld, _stunDown, _blockDown, _blockHeld, _vineDown, _vineHeld, _grappleDown, _grappleHeld;
    private void Awake() {
        /*_playerInputAction = new PlayerInputAction();
        _move = _playerInputAction.Player2.Move;
        _jump = _playerInputAction.Player2.Jump;
        _block = _playerInputAction.Player2.BlockAbility;
        _attack = _playerInputAction.Player2.StunAbility;
        _vine = _playerInputAction.Player2.VineAbility;*/
    }
    private void OnEnable() {
        //_playerInputAction.Enable();
    }
    private void OnDisable() {
        //_playerInputAction.Disable();
    }
    private void Update() {
        FrameInput = GartherFrameInput();
        ResetValues();
    }
    private void ResetValues() {
        //_move = Vector2.zero;
        _jumpDown = false;
        //_jumpHeld = false;
        _stunDown = false;
        _blockDown = false;
        //_blockHeld = false;
        _vineDown = false;
        //_vineHeld = false;
        _grappleDown = false;
    }
    private Player2FrameInput GartherFrameInput() {
        return new Player2FrameInput {
            Move = _move,
            Aim = _aim,
            JumpDown = _jumpDown,
            JumpHeld = _jumpHeld,
            StunAbilityDown = _stunDown,
            BlockAbilityDown = _blockDown,
            BlockAbilityHeld = _blockHeld,
            VineAbilityDown = _vineDown,
            VineAbilityHeld = _vineHeld,
            GrappleDown = _grappleDown,
            GrappleHeld = _grappleHeld

            /*Move = _move.ReadValue<Vector2>(),
            JumpDown = _jump.WasPerformedThisFrame(),
            JumpHeld = _jump.IsPressed(),
            StunAbilityDown = _attack.WasPerformedThisFrame(),
            BlockAbilityDown = _block.WasPerformedThisFrame(),
            BlockAbilityHeld = _block.IsPressed(),
            VineAbilityDown = _vine.WasPerformedThisFrame(),
            VineAbilityHeld = _vine.IsPressed(),*/
        };
    }
    public void MoveInput(InputAction.CallbackContext context) {
        _move = context.ReadValue<Vector2>();
    }
    public void AimInput(InputAction.CallbackContext context) {
        _aim = context.ReadValue<Vector2>();
    }
    public void JumpInput(InputAction.CallbackContext context) {
        if (context.started) {
            _jumpDown = true;
            _jumpHeld = true;
        }
        else if (context.canceled) {
            _jumpHeld = false;
        }
    }
    public void StunInput(InputAction.CallbackContext context) {
        if (context.started) {
            _stunDown = true;
        }
    }
    public void BlockInput(InputAction.CallbackContext context) {
        if (context.started) {
            _blockDown = true;
            _blockHeld = true;
        }
        else if (context.canceled) {
            _blockHeld = false;
        }
    }
    public void VineInput(InputAction.CallbackContext context) {
        if (context.started) {
            _vineDown = true;
            _vineHeld = true;
        }
        else if (context.canceled) {
            _vineHeld = false;
        }
    }
    public void GrappleInput(InputAction.CallbackContext context) {
        if (context.started) {
            _grappleDown = true;
            _grappleHeld = true;
        }
        else if (context.canceled) {
            _grappleHeld = false;
        }
    }
}
public struct Player2FrameInput {
    public Vector2 Move;
    public Vector2 Aim;
    public bool JumpDown;
    public bool JumpHeld;
    public bool StunAbilityDown;
    public bool BlockAbilityDown;
    public bool BlockAbilityHeld;
    public bool VineAbilityDown;
    public bool VineAbilityHeld;
    public bool GrappleDown;
    public bool GrappleHeld;//maybe we can do the early end like with jump
}