using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OnTrigger2dEvent;

[RequireComponent( typeof( Rigidbody2D ) )]
public class Enemy : MonoBehaviour, IStunable, IDamageTarget
{
    [Header( "Health" )]
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _stunDuration;
    [SerializeField] private bool _requiersCombo;
    [SerializeField] private EventReference _deathSound;

    [Header( "Movement" )]
    [SerializeField]
    private float _movementSpeed = 100;

    [SerializeField] private WalkPattern _walkPattern;
    [SerializeField] private Transform[] _walkPoints;

    [Header("Combat")]
    [SerializeField] private bool _dealDamageOnCollision;
    [SerializeField] private int _damageOnCollision = 1;
    [SerializeField] private float _damageFrequencyInSeconds = 1;

    [SerializeField] private LayerMask _detectionMask;

    private Vector2 _currentTargetPosition;
    private int _health;
    private int _walkPointIndex;
    private float xDir = 1;
    private static readonly int Attacking = Animator.StringToHash( "Attacking" );

    protected Rigidbody2D _rigidbody;
    protected List<GameObject> _targets = new List<GameObject>();
    protected GameObject _currentAttackTarget;
    protected Animator _animator;

    private enum WalkPattern
    {
        TurnOnCollision,
        StandStill,
        WalkBetweenPoints
    }

    protected enum State
    {
        Idle,
        Moving,
        Attacking,
        Stunned,
        Dead
    }

    protected State _state;
    protected State _previousState;

    private Dictionary<IDamageTarget, DamageOverTime> _damageDictionary = new Dictionary<IDamageTarget, DamageOverTime>();

    public class DamageOverTime
    {
        public float startTime;
        public IDamageTarget target;
        public int damage;
        private float _timer;

        private bool _colliding;

        public void Initiate(IDamageTarget target, float startTime, int damage)
        {
            this.target = target;
            this.startTime = startTime;
            this.damage = damage;

            _timer = 0;
            _colliding = true;
        }

        public void Start()
        {
            _colliding = true;
            _timer = 0;
        }

        public void Stop()
        {
            _colliding = false;
            _timer = 0;
        }

        public void Update()
        {
            if(_colliding)
            {
                if(_timer <= 0)
                {
                    target.DecreaseHealth(damage);
                    _timer = startTime;
                }

                _timer -= Time.deltaTime;
            }
        }
    }

    private bool UsingWalkPattern( WalkPattern pattern ) => _state == State.Moving && _walkPattern == pattern;

    private void Start( )
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = _maxHealth;
        _animator = GetComponentInChildren<Animator>();

        if ( _walkPoints.Length > 0 )
        {
            if ( _walkPoints[0] != null )
                _currentTargetPosition = _walkPoints[0].position;
        }

        _state = State.Moving;
    }

    void FixedUpdate( )
    {
        if ( UsingWalkPattern( WalkPattern.TurnOnCollision ) )
        {
            _rigidbody.velocity = new Vector2( _movementSpeed * xDir * Time.deltaTime, _rigidbody.velocity.y );
        }
        else if ( UsingWalkPattern( WalkPattern.WalkBetweenPoints ) )
        {
            _currentTargetPosition = _walkPoints[_walkPointIndex].position;

            float xDir = Mathf.Sign( _currentTargetPosition.x - transform.position.x );
            Vector3 scale = transform.localScale;

            if ( Mathf.Sign( scale.x ) != xDir ) // flip scale instead of setting to 1 or -1
            {
                scale.x *= -1;
            }

            transform.localScale = scale;

            _rigidbody.velocity = new Vector2( Time.deltaTime * xDir * _movementSpeed, _rigidbody.velocity.y );
        }
    }

    protected virtual void Update( )
    {
        if ( UsingWalkPattern( WalkPattern.WalkBetweenPoints ) )
        {
            if ( Mathf.Abs( transform.position.x - _currentTargetPosition.x ) <= 0.05f )
                _walkPointIndex = ( _walkPointIndex + 1 > _walkPoints.Length - 1 ) ? 0 : ++_walkPointIndex;
        }

        if ( _state == State.Attacking && _currentAttackTarget )
        {
            if ( _animator )
            {
                _animator.SetBool( Attacking, true );
            }

            float xDir = Mathf.Sign( _currentAttackTarget.transform.position.x - transform.position.x );
            Vector3 scale = transform.localScale;

            if ( Mathf.Sign( scale.x ) != xDir ) // flip scale instead of setting to 1 or -1
            {
                scale.x *= -1;
            }

            transform.localScale = scale;
        }
        else if ( _animator )
        {
            _animator.SetBool( Attacking, false );
        }

        if(_dealDamageOnCollision)
        {
            foreach(KeyValuePair<IDamageTarget, DamageOverTime> target in _damageDictionary)
                target.Value.Update();        }
    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        if ( UsingWalkPattern( WalkPattern.TurnOnCollision ) )
        {
            // if ( !HelperFunctions.PartOfLayerMask( collision.gameObject, _detectionMask ) )
            // {
            // if (collision.gameObject.CompareTag("Wall"))
            // {
            if ( collision.contacts[0].normal.y == 0 )
            {
                xDir = -xDir;
                OnTurn();
            }
            // }
            // }
        }

        if(HelperFunctions.PartOfLayerMask(collision.gameObject, _detectionMask))
        {
            if(_dealDamageOnCollision)
            {
                if (collision.gameObject.TryGetComponent(out IDamageTarget damageTarget))
                {
                    if(!_damageDictionary.ContainsKey(damageTarget))
                    {
                        DamageOverTime damageOverTime = new DamageOverTime();
                        damageOverTime.Initiate(damageTarget, _damageFrequencyInSeconds, _damageOnCollision);

                        _damageDictionary.Add(damageTarget, damageOverTime);
                    }
                    else
                    {
                        _damageDictionary[damageTarget].Start();
                    }
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (HelperFunctions.PartOfLayerMask(collision.gameObject, _detectionMask))
        {
            if (_dealDamageOnCollision)
            {
                if (collision.gameObject.TryGetComponent(out IDamageTarget damageTarget))
                {
                    if (_damageDictionary.ContainsKey(damageTarget))
                        _damageDictionary[damageTarget].Stop();
                }
            }
        }
    }

    protected virtual void OnTurn( )
    {
    }

    public void ObjectDetected( OnTriggerDelegation delegation )
    {
        if ( HelperFunctions.PartOfLayerMask( delegation.other.gameObject, _detectionMask ) )
        {
            PlayerDetected( delegation );
        }
    }

    protected virtual void PlayerDetected( OnTriggerDelegation delegation )
    {
        _state = State.Attacking;

        if ( _targets.Count >= 0 )
            _currentAttackTarget = delegation.other.gameObject;

        _targets.Add( delegation.other.gameObject );
    }

    public virtual void ObjectLost( OnTriggerDelegation delegation )
    {
        if ( HelperFunctions.PartOfLayerMask( delegation.other.gameObject, _detectionMask ) )
        {
            _targets.Remove( delegation.other.gameObject );

            if ( _targets.Count > 0 )
            {
                _currentAttackTarget = _targets[0];
            }
            else
            {
                _currentAttackTarget = null;
            }
        }
    }

    private bool _p1hit = false;
    private bool _p2hit = false;

    public bool P1hit
    {
        get { return _p1hit; }
        set { _p1hit = value; }
    }

    public bool P2hit
    {
        get { return _p2hit; }
        set { _p2hit = value; }
    }

    public bool IsStuned( )
    {
        if ( _requiersCombo ) return _state == State.Stunned;
        return false;
    }

    public void Stun( )
    {
        if ( !_requiersCombo )
        {
            KillTarget();
            return;
        }

        if ( _state == State.Stunned ) return;

        _previousState = _state;
        _state = State.Stunned;

        StartCoroutine( StunCoroutine() );
    }

    IEnumerator StunCoroutine( )
    {
        yield return new WaitForSeconds( _stunDuration );
        // _state = _previousState;
        _state = State.Moving; // Else it will start attacking again after being stunned.
                               // It should lose the players if they leave during the stun. <3
        _p1hit = false;
        _p2hit = false;
    }

    public void DecreaseHealth( int damage )
    {
        _health -= damage;

        if ( _health <= 0 )
            KillTarget();
    }

    public void KillTarget( )
    {
        RuntimeManager.PlayOneShot( _deathSound, Camera.main.transform.position );
        Destroy( gameObject );
    }
}