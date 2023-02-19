using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    [SerializeField, Range( 1, 20 )] private int _jumpTarget;
    [SerializeField, Range( 0f, 10f )] private float _attackCooldown;

    private Rigidbody2D _targetRb;
    private Player1InputScript _input1;
    private Player2InputScript _input2;
    private int _currentPlayer;
    private int _jumpCounter;
    private bool _canAttack = true;

    protected override void PlayerDetected( OnTrigger2dEvent.OnTriggerDelegation delegation ) // Limit targets to 1
    {
        if ( _currentAttackTarget || !_canAttack )
        {
            return;
        }

        base.PlayerDetected( delegation );
        _targetRb = delegation.other.attachedRigidbody;

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

        if ( _state == State.Attacking )
        {
            if ( _currentPlayer == 1 && _input1.FrameInput.JumpDown )
            {
                Jumped();
            }
            else if ( _currentPlayer == 2 && _input2.FrameInput.JumpDown )
            {
                Jumped();
            }
        }
    }

    protected override void OnTurn( )
    {
        var scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Attack( )
    {
        _targetRb.constraints = RigidbodyConstraints2D.FreezeAll;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void StopAttack( )
    {
        _targetRb.constraints = RigidbodyConstraints2D.FreezeRotation;

        _jumpCounter = 0;
        _currentPlayer = 0;
        _state = State.Moving;
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
}