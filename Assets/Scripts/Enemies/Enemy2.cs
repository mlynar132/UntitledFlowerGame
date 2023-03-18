using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    [SerializeField, Range( 1, 20 )] private int _jumpTarget;
    [SerializeField, Range( 0f, 10f )] private float _attackCooldown;
    [SerializeField, Range( 0f, 5f )] private float _secPerDamage;

    [SerializeField] private int _damage;
    [SerializeField] private Transform _playerGrabPos;

    private Rigidbody2D _targetRb;
    private Collider2D _targetCollider;
    private PlayerHealth _playerHealth;
    private Player1InputScript _input1;
    private Player2InputScript _input2;
    private int _currentPlayer;
    private int _jumpCounter;
    private bool _canAttack = true;
    private bool _currentlyAttacking;

    protected override void PlayerDetected( OnTrigger2dEvent.OnTriggerDelegation delegation ) // Limit targets to 1
    {
        if ( _currentAttackTarget || !_canAttack )
        {
            return;
        }

        base.PlayerDetected( delegation );
        _targetRb = delegation.other.attachedRigidbody;
        _targetCollider = delegation.other;
        _playerHealth = delegation.other.GetComponent<PlayerHealth>();

        if ( delegation.other.CompareTag( "Player 1" ) )
        {
            _input1 = delegation.other.GetComponent<Player1InputScript>();
            _currentPlayer = 1;
        }
        else if ( delegation.other.CompareTag( "Player 2" ) )
        {
            _input2 = delegation.other.GetComponent<Player2InputScript>();
            _currentPlayer = 2;
        }

        if ( _state == State.Attacking )
        {
            Attack();
        }
    }

    protected override void Update( )
    {
        base.Update();

        Vector2 grabPos = _playerGrabPos.position;


        if ( _state == State.Attacking )
        {
            _targetRb.transform.position = grabPos;

            if ( _currentPlayer == 1 && _input1.FrameInput.JumpDown )
            {
                Jumped();
            }

            if ( _currentPlayer == 2 && _input2.FrameInput.JumpDown )
            {
                Jumped();
            }

            if ( _state != State.Attacking )
            {
                return;
            }

            if ( !_currentlyAttacking )
            {
                _currentlyAttacking = true;
                StartCoroutine( AttackCoroutine() );
            }

            if ( _playerHealth.Health <= 0 )
            {
                StopAttack();
            }
        }

        if ( _state == State.Stunned )
        {
            if ( _currentAttackTarget )
            {
                StopAttack();
                _state = State.Stunned;
            }
        }
    }

    private void OnDestroy( )
    {
        StopAttack();
    }

    protected override void OnTurn( )
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Attack( )
    {
        _targetCollider.enabled = false;
        _targetRb.constraints = RigidbodyConstraints2D.FreezeAll;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void StopAttack( )
    {
        _targetRb.constraints = RigidbodyConstraints2D.FreezeRotation;

        _jumpCounter = 0;
        _currentPlayer = 0;
        _state = State.Moving;
        _currentlyAttacking = false;
        _targetCollider.enabled = true;
        _currentAttackTarget = null;
        StopCoroutine( AttackCoroutine() );
        StartCoroutine( CooldownCoroutine() );
    }

    private void Jumped( )
    {
        if ( ++_jumpCounter > _jumpTarget )
        {
            StopAttack();
        }
    }

    private IEnumerator CooldownCoroutine( )
    {
        _canAttack = false;
        yield return new WaitForSeconds( _attackCooldown );
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _canAttack = true;
    }

    private IEnumerator AttackCoroutine( )
    {
        yield return new WaitForSeconds( _secPerDamage );
        if ( _currentlyAttacking )
            _playerHealth.DecreaseHealth( _damage );
        _currentlyAttacking = false;
    }
}