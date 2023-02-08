using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player1InputScript : MonoBehaviour {
    public Player1FrameInput FrameInput { get; private set; }
    private PlayerInputAction _playerInputAction;
    private InputAction _move, _jump, _block, _attack, _vine;
    private void Awake() {
        _playerInputAction = new PlayerInputAction();
        _move = _playerInputAction.Player1.Move;
        _jump = _playerInputAction.Player1.Jump;
        _block = _playerInputAction.Player1.Ability1;
        _attack = _playerInputAction.Player1.Ability2;
        _vine = _playerInputAction.Player1.Ability3;
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
    private Player1FrameInput GartherFrameInput() {
        return new Player1FrameInput {
            Move = _move.ReadValue<Vector2>(),
            JumpDown = _jump.WasPerformedThisFrame(),
            JumpHeld = _jump.IsPressed(),
            Ability1Down = _block.WasPerformedThisFrame(),
            AttackDown = _attack.WasPerformedThisFrame(),
            VineDown = _vine.WasPerformedThisFrame()
        };
    }
}
public struct Player1FrameInput {
    public Vector2 Move;
    public bool JumpDown;
    public bool JumpHeld;
    public bool Ability1Down;
    public bool AttackDown;
    public bool VineDown;
}