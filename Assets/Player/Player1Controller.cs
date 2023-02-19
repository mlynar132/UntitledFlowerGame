using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player1Controller : MonoBehaviour, IPlayer1Controller {
    [SerializeField] private ScriptableStats _stats;

    #region internal

    private Rigidbody2D _rb;
    [SerializeField] private CapsuleCollider2D _col;
    private Player1InputScript _input = null;
    private bool _cachedTriggerSetting;

    private Player1FrameInput _frameInput;
    private Vector2 _speed;
    private Vector2 _currentExternalVelocity;
    private Vector2 _lookDir = Vector2.right;
    private int _fixedFrame;
    private bool _hasControl = true;

    #endregion

    #region external

    public event Action<bool, float> GroundedChanged;
    public event Action<bool, Vector2> DashingChanged;
    public event Action<bool> WallGrabChanged;
    public event Action<bool> Jumped;
    public event Action Attacked;
    public event Action<float> BombUsed;
    public event Action<Vector2> AnchorUsed;
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

    #endregion

    protected void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<Player1InputScript>();
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

        if (_frameInput.BombDown) _bombToConsume = true;
        if (_frameInput.AnchorDown) _anchorToConsume = true;//max duration?
        if (_frameInput.DashDown) _dashToConsume = true;
    }
    protected void FixedUpdate() {
        _fixedFrame++;

        //functions
        CheckCollisions();
        HandleCollisions();

        HandleJump();
        HandleBomb();
        HandleAnchor();
        HandleDash();

        HandleHorizontal();
        HandleVertical();
        HandleRotation();
        ApplyMovement();
    }

    #region Collisions

    private readonly RaycastHit2D[] _groundHits = new RaycastHit2D[2];
    private readonly RaycastHit2D[] _ceilingHits = new RaycastHit2D[2];
    private Collider2D[] _enemyHits = new Collider2D[5];
    private readonly Collider2D[] _wallHits = new Collider2D[5];
    private readonly Collider2D[] _ladderHits = new Collider2D[1];
    private RaycastHit2D _hittingWall;
    private int _groundHitCount;
    private int _ceilingHitCount;
    private int _wallHitCount;
    private int _ladderHitCount;
    private int _frameLeftGrounded = int.MinValue;
    private bool _grounded;
    private Vector2 _skinWidth = new(0.02f, 0.02f);

    protected virtual void CheckCollisions() {
        Physics2D.queriesHitTriggers = false;

        // Ground and Ceiling
        _groundHitCount = Physics2D.CapsuleCastNonAlloc(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _groundHits, _stats.GrounderDistance, ~(_stats.PlayerLayer | LayerMask.GetMask("Enemy") | LayerMask.GetMask("Bomb")));
        _ceilingHitCount = Physics2D.CapsuleCastNonAlloc(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _ceilingHits, _stats.GrounderDistance, ~(_stats.PlayerLayer | LayerMask.GetMask("Enemy") | LayerMask.GetMask("Bomb")));

        //do some stuff for collision during dash so it can pass thourh enemys and kill them

        //dash colision
        if (_dashing) {
            _enemyHits = Physics2D.OverlapCapsuleAll(_col.bounds.center, _col.size, _col.direction, 0, LayerMask.GetMask("Enemy"));
            for (int i = 0; i < _enemyHits.Length; i++) {
                if (_enemyHits[i].TryGetComponent(out IStunable enemyStunable)) {
                    if (enemyStunable.IsStuned() && enemyStunable.P2hit) {
                        if (_enemyHits[i].TryGetComponent(out IDamageTarget enemyKillabe)) {
                            enemyKillabe.DecreaseHealth(1);
                        }
                    }
                    else {
                        enemyStunable.Stun();
                        enemyStunable.P1hit = true;
                    }
                }
            }
        }


        Physics2D.queriesHitTriggers = _cachedTriggerSetting;
    }
    protected virtual bool TryGetGroundNormal(out Vector2 groundNormal) {
        Physics2D.queriesHitTriggers = false;
        var hit = Physics2D.Raycast(_rb.position, Vector2.down, _stats.GrounderDistance * 2, ~_stats.PlayerLayer);
        Physics2D.queriesHitTriggers = _cachedTriggerSetting;
        groundNormal = hit.normal; // defaults to Vector2.zero if nothing was hit
        return hit.collider;
    }
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
            ResetDash();
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
    private bool CanAirJump => !_grounded && _airJumpsRemaining > 0;

    protected virtual void HandleJump() {
        if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true; // Early end detection

        if (!_jumpToConsume && !HasBufferedJump) return;
        if (_grounded || CanUseCoyote) NormalJump();
        else if (_jumpToConsume && CanAirJump) AirJump();

        _jumpToConsume = false; // Always consume the flag
    }
    protected virtual void NormalJump() {
        _endedJumpEarly = false;
        _frameJumpWasPressed = 0; // prevents double-dipping 1 input's jumpToConsume and buffered jump for low ceilings
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _speed.y = _stats.JumpPower;
        Jumped?.Invoke(false);
    }
    protected virtual void AirJump() {
        _endedJumpEarly = false;
        _airJumpsRemaining--;
        _speed.y = _stats.JumpPower;
        _currentExternalVelocity.y = 0; // optional. test it out with a Bouncer if this feels better or worse
        Jumped?.Invoke(true);
    }
    protected virtual void ResetJump() {
        _coyoteUsable = true;
        _bufferedJumpUsable = true;
        _endedJumpEarly = false;
        ResetAirJumps();
    }
    protected virtual void ResetAirJumps() => _airJumpsRemaining = _stats.MaxAirJumps;

    #endregion

    #region Bomb

    private bool _bombToConsume;
    private bool _bombReady = true;
    Coroutine _bombCooldownCoroutine;

    protected virtual void HandleBomb() {
        if (!_stats.AllowBomb) return;
        if (_bombToConsume && _bombReady) {
            BombUsed?.Invoke(Mathf.Sign(_lookDir.x));

            _bombReady = false;
            _bombCooldownCoroutine = StartCoroutine(BombCooldownCoroutine());
        }
        _bombToConsume = false;
    }
    IEnumerator BombCooldownCoroutine() {
        yield return new WaitForSeconds(_stats.BombCooldown);
        _bombReady = true;
    }

    #endregion

    #region Anchor

    private bool _anchorToConsume;
    private bool _anchorReady = true;
    Coroutine _anchorCooldownCoroutine;

    protected virtual void HandleAnchor() {
        if (!_stats.AllowAnchor) return;
        if (_anchorToConsume && _anchorReady) {
            if (_frameInput.Aim == Vector2.zero) {
                AnchorUsed?.Invoke(_lookDir);
            }
            else {
                AnchorUsed?.Invoke(_frameInput.Aim);
            }
            _anchorReady = false;
            _anchorCooldownCoroutine = StartCoroutine(AnchorCooldownCoroutine());
        }
        _anchorToConsume = false;
    }
    IEnumerator AnchorCooldownCoroutine() {
        yield return new WaitForSeconds(_stats.StunAbilityCooldown);
        _anchorReady = true;
    }

    #endregion

    #region Dash

    private bool _dashToConsume;
    private bool _canDash;
    private Vector2 _dashVel;
    private bool _dashing;
    private int _startedDashing;

    protected virtual void HandleDash() {
        if (_dashToConsume && _canDash) {

            Vector2 dir = new Vector2(_frameInput.Move.x, Mathf.Max(_frameInput.Move.y, 0f)).normalized; //max so that you can't dash down
            if (dir == Vector2.zero) {
                dir = _lookDir;
                //DashingChanged?.Invoke(true, _lookDir);
                //return;
            }
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            _dashVel = dir * _stats.DashVelocity;
            _dashing = true;
            _canDash = false;
            _startedDashing = _fixedFrame;
            DashingChanged?.Invoke(true, dir);

            _currentExternalVelocity = Vector2.zero; // Strip external buildup
        }

        if (_dashing) {
            _speed = _dashVel;
            // Cancel when the time is out or we've reached our max safety distance
            if (_fixedFrame > _startedDashing + _stats.DashDurationFrames) {
                _dashing = false;
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
                DashingChanged?.Invoke(false, Vector2.zero);
                _speed.y = Mathf.Min(0, _speed.y);
                _speed.x *= _stats.DashEndHorizontalMultiplier;
                if (_grounded) ResetDash();
            }
        }

        _dashToConsume = false;
    }

    protected virtual void ResetDash() {
        _canDash = true;
    }
    #endregion

    #region Horizontal

    private bool HorizontalInputPressed => Mathf.Abs(_frameInput.Move.x) > _stats.HorizontalDeadzoneThreshold;
    private bool _stickyFeet;

    protected virtual void HandleHorizontal() {
        if (_dashing) return;

        // Deceleration
        if (!HorizontalInputPressed) {
            var deceleration = _grounded ? _stats.GroundDeceleration * (_stickyFeet ? _stats.StickyFeetMultiplier : 1) : _stats.AirDeceleration;
            _speed.x = Mathf.MoveTowards(_speed.x, 0, deceleration * Time.fixedDeltaTime);
        }

        // Regular Horizontal Movement
        else {
            // Prevent useless horizontal speed buildup when against a wall
            if (_hittingWall.collider && Mathf.Abs(_rb.velocity.x) < 0.01f) _speed.x = 0;

            var xInput = _frameInput.Move.x;
            _speed.x = Mathf.MoveTowards(_speed.x, xInput * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Vertical

    protected virtual void HandleVertical() {
        //if (_dashing) return;

        // Grounded & Slopes
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
            _lookDir = Vector2.right;
        }
        else if (_rb.velocity.x < 0) {
            //rotate left 
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            _lookDir = Vector2.left;
        }
    }

    protected virtual void ApplyMovement() {
        if (!_hasControl) return;

        _rb.velocity = _speed + _currentExternalVelocity;
        //_rb.velocity = new Vector2(_rb.velocity.x + _speed.x + _currentExternalVelocity.x, _rb.velocity.y + _currentExternalVelocity.y);
        _currentExternalVelocity = Vector2.MoveTowards(_currentExternalVelocity, Vector2.zero, _stats.ExternalVelocityDecay * Time.fixedDeltaTime);
    }
}







// this exists to get the data for other scripts easier
public interface IPlayer1Controller {
    public event Action<bool, float> GroundedChanged; // true = Landed. false = Left the Ground. float is Impact Speed
    public event Action<bool, Vector2> DashingChanged; // Dashing - Dir
    public event Action<bool> WallGrabChanged;
    public event Action<bool> Jumped; // Is AirJump
    public event Action<float> BombUsed;
    public event Action<Vector2> AnchorUsed;
    public ScriptableStats PlayerStats { get; }
    public Vector2 Input { get; }
    public Vector2 Speed { get; }
    public Vector2 Velocity { get; }
    public Vector2 GroundNormal { get; }
    public int WallDirection { get; }
    public void ApplyVelocity(Vector2 vel, PlayerForce forceType);
    public void SetVelocity(Vector2 vel, PlayerForce velocityType);
}