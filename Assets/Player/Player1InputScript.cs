using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player1InputScript : MonoBehaviour {
    public Player1FrameInput FrameInput { get; private set; }
    private Vector2 _move, _aim;
    private bool _jumpDown, _jumpHeld, _dashDown, _bombDown, _anchorDown;
    private void Update() {
        FrameInput = GartherFrameInput();
        ResetValues();
    }
    private Player1FrameInput GartherFrameInput() {
        return new Player1FrameInput {
            Move = _move,
            Aim = _aim,
            JumpDown = _jumpDown,
            JumpHeld = _jumpHeld,
            DashDown = _dashDown,
            BombDown = _bombDown,
            AnchorDown = _anchorDown
        };
    }
    private void ResetValues() {
        _jumpDown = false;;
        _dashDown = false;
        _bombDown = false;
        _anchorDown = false;
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
    public void DashnInput(InputAction.CallbackContext context) {
        if (context.started) {
            _dashDown = true;
        }
    }
    public void BombInput(InputAction.CallbackContext context) {
        if (context.started) {
            _bombDown = true;
        }
    }
    public void AnchorInput(InputAction.CallbackContext context) {
        if (context.started) {
            _anchorDown = true;
        }
    }
}
public struct Player1FrameInput {
    public Vector2 Move;
    public Vector2 Aim;
    public bool JumpDown;
    public bool JumpHeld;
    public bool DashDown;
    public bool BombDown;
    public bool AnchorDown;
}