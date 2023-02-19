using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player2Animator : MonoBehaviour {
    private IPlayer2Controller _player;
    private Animator _anim;

    private void Awake() {
        _player = GetComponentInParent<IPlayer2Controller>();
        _anim = GetComponent<Animator>();
    }

    private void Start() {
        _player.GroundedChanged += OnGroundedChanged;
        _player.DashingChanged += OnDashingChanged;
        _player.Jumped += OnJumped;
        _player.VineAbilityChanged += OnSwing;
    }

    private void Update() {
        HandleFlipping();
        HandleAnimations();
    }

    private void HandleFlipping() {
        if (Mathf.Abs(_player.Input.x) > 0.1f) /*do the flip*/;
    }

    #region Ground Movement

    [Header("GROUND MOVEMENT")]
    [SerializeField] private EventReference _footstepClips;

    // Called from AnimationEvent
    public void PlayFootstepSound() {
        RuntimeManager.PlayOneShot(_footstepClips, transform.position);
    }

    #endregion

    #region Dash

   // [Header("DASHING")]
    //[SerializeField] private AudioClip _dashClip;

    private void OnDashingChanged(bool dashing, Vector2 dir) {
        if (dashing) {
         //   PlaySound(_dashClip, 0.1f);
        }
    }

    #endregion

    #region Jumping and Landing

    [Header("JUMPING")]
    [SerializeField] private float _minImpactForce = 20;
    [SerializeField] private float _maxImpactForce = 40;
    [SerializeField] private float _landAnimDuration = 0.36f;
    [SerializeField] private float _jumpAnimDuration = 0.4f;
    //[SerializeField] private AudioClip _landClip, _jumpClip, _doubleJumpClip;

    private bool _jumpTriggered;
    private bool _landed;
    private bool _grounded;

    private void OnJumped(bool airJump) {
        _jumpTriggered = true;
        if (airJump) {
            //PlaySound(_doubleJumpClip, 0.1f);
            return;
        }
        //PlaySound(_jumpClip, 0.05f, Random.Range(0.98f, 1.02f));
    }

    private void OnGroundedChanged(bool grounded, float impactForce) {
        _grounded = grounded;

        if (impactForce >= _minImpactForce) {
            var p = Mathf.InverseLerp(_minImpactForce, _maxImpactForce, impactForce);
            _landed = true;
            //PlaySound(_landClip, p * 0.1f);
        }
    }

    #endregion

    #region Attack

    [Header("ATTACK")]
    [SerializeField] private float _attackAnimTime = 0.25f;
    //[SerializeField] private AudioClip _attackClip;
    private bool _attacked;

    private void OnAttacked() => _attacked = true;

    // Called from AnimationEvent
    //public void PlayAttackSound() => PlaySound(_attackClip, 0.1f, Random.Range(0.97f, 1.03f));

    #endregion

    #region Swing

    [Header("Swing")]
    //[SerializeField] private AudioClip _attackClip;
    private bool _swinging = false;
    private void OnSwing(bool isSwinging, Vector2 trash) {
        _swinging = isSwinging;
    } 

    // Called from AnimationEvent
    //public void PlayAttackSound() => PlaySound(_attackClip, 0.1f, Random.Range(0.97f, 1.03f));

    #endregion

    #region Animation

    private float _lockedTill;

    private void HandleAnimations() {

        var state = GetState();
        ResetFlags();
        if (state == _currentState) return;

        //_anim.Play(state, 0); 
        _anim.CrossFade(state, 0, 0);
        _currentState = state;

        int GetState() {
            if (Time.time < _lockedTill) return _currentState;

            if (_swinging) return Swing;

            if (_attacked) return LockState(Attack, _attackAnimTime);

            if (_landed) return LockState(Land, _landAnimDuration);

            if (_jumpTriggered) return LockState(Jump, _jumpAnimDuration);

            if (_grounded) return _player.Input.x == 0 ? Idle : Walk;

            return Fall;

            int LockState(int s, float t) {
                _lockedTill = Time.time + t;
                return s;
            }
        }

        void ResetFlags() {
            _jumpTriggered = false;
            _landed = false;
            _attacked = false;
        }


        /*
        var state = GetState();
        Debug.Log(state);
        ResetFlags();
        if (state == _currentState) return;

        //_anim.Play(state, 0); 
        _anim.CrossFade(state, 0, 0);
        _currentState = state;

        int GetState() {
            if (Time.time < _lockedTill) return _currentState;

            if (_attacked) return LockState(Attack, _attackAnimTime);

            if (_landed) return LockState(Land, _landAnimDuration);
            if (_jumpTriggered) return Jump;

            if (_grounded) return _player.Input.x == 0 ? Idle : Walk;
            //if (_player.Speed.y > 0) return _wallJumped ? Backflip : Jump;

            return Fall;
            // TODO: If WallDismount looks/feels good enough to keep, we should add clip duration (0.167f) to Stats


            int LockState(int s, float t) {
                _lockedTill = Time.time + t;
                return s;
            }
        }

        void ResetFlags() {
            _jumpTriggered = false;
            _landed = false;
            _attacked = false;
        }*/
    }

    private void UnlockAnimationLock() => _lockedTill = 0f;

    #region Cached Properties

    private int _currentState;

    private static readonly int Idle = Animator.StringToHash("Base Layer.Idle");
    private static readonly int Walk = Animator.StringToHash("Base Layer.Walk");

    private static readonly int Jump = Animator.StringToHash("Base Layer.Jump");
    private static readonly int Fall = Animator.StringToHash("Base Layer.Fall");
    private static readonly int Land = Animator.StringToHash("Base Layer.Land");

    private static readonly int Attack = Animator.StringToHash("Base Layer.Attack");
    private static readonly int Swing = Animator.StringToHash("Base Layer.Swing");
    #endregion

    #endregion

    #region Audio

    private void PlaySound(AudioClip clip, float volume = 1, float pitch = 1) {
       // _source.pitch = pitch;
       // _source.PlayOneShot(clip, volume);
    }

    #endregion
}
