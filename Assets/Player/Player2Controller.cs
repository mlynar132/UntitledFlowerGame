using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player2Controller : MonoBehaviour, IPlayer2Controller {
    [SerializeField] private ScriptableStats _stats;

    #region internal

    private Rigidbody2D _rb;
    [SerializeField ]private GameObject _visuals;
    [SerializeField] private CapsuleCollider2D _col;
    private Player2InputScript _input = null;
    private bool _cachedTriggerSetting;

    private Player2FrameInput _frameInput;
    private Vector2 _speed;
    private Vector2 _currentExternalVelocity;
    private int _fixedFrame;
    private bool _hasControl = true;

    #endregion

    #region external

    public event Action<bool, float> GroundedChanged;
    public event Action<bool, Vector2> DashingChanged;
    public event Action<bool> WallGrabChanged;
    public event Action<bool> Jumped;
    public event Action AirJumped;
    public event Action StunAbility;
    public event Action BlockAbilityStart;
    public event Action BlockAbilityEnd;
    public event Action VineAbilityStart;
    public event Action VineAbilityEnd;
    public ScriptableStats PlayerStats => _stats;
    public Vector2 Input => _frameInput.Move;
    public Vector2 Speed => _speed;
    public Vector2 Velocity => _rb.velocity;
    public Vector2 GroundNormal { get; private set; }
    public int WallDirection { get; private set; }

    public void ApplyVelocity(Vector2 vel, PlayerForce forceType) {
        if (forceType == PlayerForce.Burst) {
            _speed += vel;
        }
        else {
            _currentExternalVelocity += vel;
        }
    }
    public void SetVelocity(Vector2 vel, PlayerForce velocityType) {
        if (velocityType == PlayerForce.Burst) {
            _speed = vel;
        }
        else {
            _currentExternalVelocity = vel;
        }
    }
    public void TakeAwayControl(bool resetVelocity = true) {
        if (resetVelocity) {
            _rb.velocity = Vector2.zero;
        }
        _hasControl = false;
    }
    public void ReturnControl() {
        _speed = Vector2.zero;
        _hasControl = true;
    }
    public void Deactivate(bool resetVelocity = true) {
        TakeAwayControl(resetVelocity);
        //turn off col
        _col.enabled = false;
        _checkForColision = false;
        //hide player
        _visuals.SetActive(false);

    }
    public void Activate() {
        ReturnControl();
        //turn on col
        _col.enabled = true;
        _checkForColision = true;
        //show player
        _visuals.SetActive(true);
    }

    #endregion

    protected void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<Player2InputScript>();
        _cachedTriggerSetting = Physics2D.queriesHitTriggers;
        Physics2D.queriesStartInColliders = true;

        //TOGGLE colliders
    }
    protected void Update() {
        GatherInput();
    }
    protected void GatherInput() {
        _frameInput = _input.FrameInput;

        if (_stats.SnapInput) {
            _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadzoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
            _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadzoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
        }

        if (_frameInput.JumpDown) {
            _jumpToConsume = true;
            _frameJumpWasPressed = _fixedFrame;
        }

        if (_frameInput.Move.x != 0) _stickyFeet = false;

        //if (_frameInput.DashDown && _stats.AllowDash) _dashToConsume = true;
        if (_frameInput.StunAbilityDown) _stunAbilityToConsume = true;

        if (_frameInput.BlockAbilityDown) _blockAbilityToConsume = true;//max duration?
        //if (_frameInput.AttackDown && _stats.AllowAttacks) _attackToConsume = true;
    }
    protected void FixedUpdate() {
        _fixedFrame++;

        //functions
        CheckCollisions();
        HandleCollisions();

        HandleJump();
        HandleStunAbility();
        HandleBlockAbility();

        HandleHorizontal();
        HandleVertical();
        HandleRotation();
        ApplyMovement();
    }

    #region Collisions

    private readonly RaycastHit2D[] _groundHits = new RaycastHit2D[2];
    private readonly RaycastHit2D[] _ceilingHits = new RaycastHit2D[2];
    private readonly Collider2D[] _wallHits = new Collider2D[5];
    private readonly Collider2D[] _ladderHits = new Collider2D[1];
    private RaycastHit2D _hittingWall;
    private int _groundHitCount;
    private int _ceilingHitCount;
    private int _wallHitCount;
    private int _ladderHitCount;
    private int _frameLeftGrounded = int.MinValue;
    private bool _grounded;
    private bool _checkForColision = true;
    private Vector2 _skinWidth = new(0.02f, 0.02f); // Expose this?

    protected virtual void CheckCollisions() {
        Physics2D.queriesHitTriggers = false;

        // Ground and Ceiling
        if (_checkForColision) {
            _groundHitCount = Physics2D.CapsuleCastNonAlloc(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _groundHits, _stats.GrounderDistance, ~_stats.PlayerLayer);
            _ceilingHitCount = Physics2D.CapsuleCastNonAlloc(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _ceilingHits, _stats.GrounderDistance, ~_stats.PlayerLayer);
        }
        // Walls and Ladders
        //var bounds = GetWallDetectionBounds(); // won't be able to detect a wall if we're crouching mid-air
        //_wallHitCount = Physics2D.OverlapBoxNonAlloc(bounds.center, bounds.size, 0, _wallHits, _stats.ClimbableLayer);

        //_hittingWall = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, new Vector2(_input.FrameInput.Move.x, 0), _stats.GrounderDistance, ~_stats.PlayerLayer);

        //Physics2D.queriesHitTriggers = true; // Ladders are set to Trigger
        //_ladderHitCount = Physics2D.OverlapBoxNonAlloc(bounds.center, bounds.size, 0, _ladderHits, _stats.LadderLayer);
        Physics2D.queriesHitTriggers = _cachedTriggerSetting;
    }
    protected virtual bool TryGetGroundNormal(out Vector2 groundNormal) {
        Physics2D.queriesHitTriggers = false;
        var hit = Physics2D.Raycast(_rb.position, Vector2.down, _stats.GrounderDistance * 2, ~_stats.PlayerLayer);
        Physics2D.queriesHitTriggers = _cachedTriggerSetting;
        groundNormal = hit.normal; // defaults to Vector2.zero if nothing was hit
        return hit.collider;
    }
    /*private Bounds GetWallDetectionBounds() {
        var colliderOrigin = _rb.position + _col.offset;
        return new Bounds(colliderOrigin, _stats.WallDetectorSize);
    }*/
    protected virtual void HandleCollisions() {
        // Hit a Ceiling
        if (_ceilingHitCount > 0) {
            // prevent sticking to ceiling if we did an InAir jump after receiving external velocity w/ PlayerForce.Decay
            _currentExternalVelocity.y = Mathf.Min(0f, _currentExternalVelocity.y);
            _speed.y = Mathf.Min(0, _speed.y);
        }
        // Landed on the Ground
        if (!_grounded && _groundHitCount > 0) {
            _grounded = true;
            //ResetDash();
            ResetJump();
            GroundedChanged?.Invoke(true, Mathf.Abs(_speed.y));
            if (_frameInput.Move.x == 0) _stickyFeet = true;
        }
        // Left the Ground
        else if (_grounded && _groundHitCount == 0) {
            _grounded = false;
            _frameLeftGrounded = _fixedFrame;
            GroundedChanged?.Invoke(false, 0);
        }
    }

    //private bool IsStandingPosClear(Vector2 pos) => CheckPos(pos, _standingCollider);
    //private bool IsCrouchingPosClear(Vector2 pos) => CheckPos(pos, _crouchingCollider);

    /*protected virtual bool CheckPos(Vector2 pos, CapsuleCollider2D col) {
        Physics2D.queriesHitTriggers = false;
        var hit = Physics2D.OverlapCapsule(pos + col.offset, col.size - _skinWidth, col.direction, 0, ~_stats.PlayerLayer);
        Physics2D.queriesHitTriggers = _cachedTriggerSetting;
        return !hit;
    }*/

    #endregion

    #region Jumping

    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private bool _wallJumpCoyoteUsable;
    private int _frameJumpWasPressed;
    private int _airJumpsRemaining;

    private bool HasBufferedJump => _bufferedJumpUsable && _fixedFrame < _frameJumpWasPressed + _stats.JumpBufferFrames;
    private bool CanUseCoyote => _coyoteUsable && !_grounded && _fixedFrame < _frameLeftGrounded + _stats.CoyoteFrames;
    //private bool CanWallJump => (_isOnWall && !_isLeavingWall) || (_wallJumpCoyoteUsable && _fixedFrame < _frameLeftWall + _stats.WallJumpCoyoteFrames);
    private bool CanAirJump => !_grounded && _airJumpsRemaining > 0;

    protected virtual void HandleJump() {
        if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true; // Early end detection

        if (!_jumpToConsume && !HasBufferedJump) return;
        //if (CanWallJump) WallJump();
        if (_grounded /*|| ClimbingLadder*/ || CanUseCoyote) NormalJump();
        else if (_jumpToConsume && CanAirJump) AirJump();

        _jumpToConsume = false; // Always consume the flag
    }
    // Includes Ladder Jumps
    protected virtual void NormalJump() {
        //if (Crouching && !TryToggleCrouching(false)) return; // try standing up first so we don't get stuck in low ceilings
        _endedJumpEarly = false;
        _frameJumpWasPressed = 0; // prevents double-dipping 1 input's jumpToConsume and buffered jump for low ceilings
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        //ToggleClimbingLadder(false);
        _speed.y = _stats.JumpPower;
        Jumped?.Invoke(false);
    }
    /*protected virtual void WallJump() {
        _endedJumpEarly = false;
        _bufferedJumpUsable = false;
        if (_isOnWall) _isLeavingWall = true; // only toggle if it's a real WallJump, not CoyoteWallJump
        _wallJumpCoyoteUsable = false;
        _currentWallJumpMoveMultiplier = 0;
        _speed = Vector2.Scale(_stats.WallJumpPower, new(-_lastWallDirection, 1));
        Jumped?.Invoke(true);
    }*/
    protected virtual void AirJump() {
        _endedJumpEarly = false;
        _airJumpsRemaining--;
        _speed.y = _stats.JumpPower;
        _currentExternalVelocity.y = 0; // optional. test it out with a Bouncer if this feels better or worse
        AirJumped?.Invoke();
    }
    protected virtual void ResetJump() {
        _coyoteUsable = true;
        _bufferedJumpUsable = true;
        _endedJumpEarly = false;
        ResetAirJumps();
    }
    protected virtual void ResetAirJumps() => _airJumpsRemaining = _stats.MaxAirJumps;

    #endregion

    #region StunAbility

    private bool _stunAbilityToConsume;
    private bool _stunAbilityReady = true;
    Coroutine _stunAbilityCooldownCoroutine;

    protected virtual void HandleStunAbility() {
        if (!_stats.AllowStunAbility) return;
        if (_stunAbilityToConsume && _stunAbilityReady) {
            StunAbility?.Invoke();
            _stunAbilityReady = false;
            _stunAbilityCooldownCoroutine = StartCoroutine(StunAbilityCooldownCoroutine());
        }
        _stunAbilityToConsume = false;
    }
    IEnumerator StunAbilityCooldownCoroutine() {
        yield return new WaitForSeconds(_stats.StunAbilityCooldown);
        _stunAbilityReady = true;
    }

    #endregion

    #region BlockAbility

    private bool _blockAbilityToConsume;
    private bool _blockAbilityActive = false;
    private bool _endedBlockAbility = false;
    private bool _blockAbilityReady = true;
    Coroutine _blockAbilityCooldownCoroutine;

    protected virtual void HandleBlockAbility() {
        if (!_stats.AllowBlockAbility) return;

        if (_blockAbilityActive && !_endedBlockAbility && !_frameInput.BlockAbilityHeld) _endedBlockAbility = true; // End detection

        if (_blockAbilityToConsume && _blockAbilityReady) {
            BlockAbilityStart?.Invoke();
            _blockAbilityActive = true;
        }
        if (_endedBlockAbility) {
            BlockAbilityEnd?.Invoke();
            _blockAbilityActive = false;
            _endedBlockAbility = false;
            _blockAbilityReady = false;
            _blockAbilityCooldownCoroutine = StartCoroutine(BlockAbilityCooldownCoroutine());
        }
        _blockAbilityToConsume = false;
    }
    IEnumerator BlockAbilityCooldownCoroutine() {
        yield return new WaitForSeconds(_stats.BlockAbilityCooldown);
        _blockAbilityReady = true;
    }
    #endregion

    #region VineAbility

    private bool _vineAbilityToConsume;
    private bool _vineAbilityActive = false;
    private bool _endedVineAbility = false;
    private bool _vineAbilityReady = true;
    Coroutine _vineAbilityCooldownCoroutine;

    protected virtual void HandlevineAbility() {
        if (!_stats.AllowVineAbility) return;

        if (_vineAbilityActive && !_endedVineAbility && !_frameInput.VineAbilityHeld) _endedVineAbility = true; // End detection

        if (_vineAbilityToConsume && _vineAbilityReady) {
            VineAbilityStart?.Invoke();
            _vineAbilityActive = true;
        }
        if (_endedBlockAbility) {
            VineAbilityEnd?.Invoke();
            _vineAbilityActive = false;
            _endedVineAbility = false;
            _vineAbilityReady = false;
            _vineAbilityCooldownCoroutine = StartCoroutine(vineAbilityCooldownCoroutine());
        }
        _vineAbilityToConsume = false;
    }
    IEnumerator vineAbilityCooldownCoroutine() {
        yield return new WaitForSeconds(_stats.VineAbilityCooldown);
        _vineAbilityReady = true;
    }
    #endregion

    #region Horizontal

    private bool HorizontalInputPressed => Mathf.Abs(_frameInput.Move.x) > _stats.HorizontalDeadzoneThreshold;
    private bool _stickyFeet;

    protected virtual void HandleHorizontal() {
        //if (_dashing) return;

        // Deceleration
        if (!HorizontalInputPressed) {
            var deceleration = _grounded ? _stats.GroundDeceleration * (_stickyFeet ? _stats.StickyFeetMultiplier : 1) : _stats.AirDeceleration;
            _speed.x = Mathf.MoveTowards(_speed.x, 0, deceleration * Time.fixedDeltaTime);
        }
        // Crawling
        /*else if (Crouching && _grounded) {
            var crouchPoint = Mathf.InverseLerp(0, _stats.CrouchSlowdownFrames, _fixedFrame - _frameStartedCrouching);
            var diminishedMaxSpeed = _stats.MaxSpeed * Mathf.Lerp(1, _stats.CrouchSpeedPenalty, crouchPoint);
            _speed.x = Mathf.MoveTowards(_speed.x, _frameInput.Move.x * diminishedMaxSpeed, _stats.GroundDeceleration * Time.fixedDeltaTime);
        }*/
        // Regular Horizontal Movement
        else {
            // Prevent useless horizontal speed buildup when against a wall
            if (_hittingWall.collider && Mathf.Abs(_rb.velocity.x) < 0.01f /*&& !_isLeavingWall*/) _speed.x = 0;

            var xInput = _frameInput.Move.x/* * (ClimbingLadder ? _stats.LadderShimmySpeedMultiplier : 1)*/;
            _speed.x = Mathf.MoveTowards(_speed.x, xInput * _stats.MaxSpeed,/* _currentWallJumpMoveMultiplier * */_stats.Acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Vertical

    protected virtual void HandleVertical() {
        //if (_dashing) return;

        // Ladder
        /*if (ClimbingLadder) {
            var yInput = _frameInput.Move.y;
            _speed.y = yInput * (yInput > 0 ? _stats.LadderClimbSpeed : _stats.LadderSlideSpeed);
        }*/
        // Grounded & Slopes
        /*else*/
        if (_grounded && _speed.y <= 0f) {
            _speed.y = _stats.GroundingForce;
            if (TryGetGroundNormal(out var groundNormal)) {
                GroundNormal = groundNormal;
                if (!Mathf.Approximately(GroundNormal.y, 1f)) {
                    // on a slope
                    _speed.y = _speed.x * -GroundNormal.x / GroundNormal.y;
                    if (_speed.x != 0) _speed.y += _stats.GroundingForce;
                }
            }
        }
        // Wall Climbing & Sliding
        /*else if (_isOnWall && !_isLeavingWall) {
            if (_frameInput.Move.y > 0) _speed.y = _stats.WallClimbSpeed;
            else if (_frameInput.Move.y < 0) _speed.y = -_stats.MaxWallFallSpeed;
            else if (GrabbingLedge) _speed.y = Mathf.MoveTowards(_speed.y, 0, _stats.LedgeGrabDeceleration * Time.fixedDeltaTime);
            else _speed.y = Mathf.MoveTowards(Mathf.Min(_speed.y, 0), -_stats.MaxWallFallSpeed, _stats.WallFallAcceleration * Time.fixedDeltaTime);
        }*/
        // In Air
        else {
            var inAirGravity = _stats.FallAcceleration;
            if (_endedJumpEarly && _speed.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
            _speed.y = Mathf.MoveTowards(_speed.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    #endregion

    protected virtual void HandleRotation() {
        if (_rb.velocity.x > 0 /*&& additional statement for when the player is attacking so they do not rotate with attacks spawned*/) {
            //rotate right
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
        else if (_rb.velocity.x < 0) {
            //rotate left 
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }
    }
    protected virtual void ApplyMovement() {
        if (!_hasControl) return;

        _rb.velocity = _speed + _currentExternalVelocity;
        _currentExternalVelocity = Vector2.MoveTowards(_currentExternalVelocity, Vector2.zero, _stats.ExternalVelocityDecay * Time.fixedDeltaTime);
    }
}










// this exists to get the data for other scripts easier
public interface IPlayer2Controller {
    public event Action<bool, float> GroundedChanged; // true = Landed. false = Left the Ground. float is Impact Speed
    public event Action<bool, Vector2> DashingChanged; // Dashing - Dir
    public event Action<bool> WallGrabChanged;
    public event Action<bool> Jumped; // Is wall jump
    public event Action AirJumped;
    public event Action StunAbility;
    public event Action BlockAbilityStart;
    public event Action BlockAbilityEnd;
    public event Action VineAbilityStart;
    public event Action VineAbilityEnd;
    public ScriptableStats PlayerStats { get; }
    public Vector2 Input { get; }
    public Vector2 Speed { get; }
    public Vector2 Velocity { get; }
    public Vector2 GroundNormal { get; }
    public int WallDirection { get; }
    public void ApplyVelocity(Vector2 vel, PlayerForce forceType);
    public void SetVelocity(Vector2 vel, PlayerForce velocityType);
}
public enum PlayerForce {
    /// <summary>
    /// Added directly to the players movement speed, to be controlled by the standard deceleration
    /// </summary>
    Burst,

    /// <summary>
    /// An external velocity that decays over time, applied additively to the rigidbody's velocity
    /// </summary>
    Decay
}