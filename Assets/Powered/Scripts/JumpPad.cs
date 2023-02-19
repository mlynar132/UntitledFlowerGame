using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour, IPowered
{
    [SerializeField, Range( 1, 100 )] private float _power;
    [SerializeField] private bool _alwaysActive;

    private bool _active;
    private List<Rigidbody2D> _currentBodies = new List<Rigidbody2D>();


    private void FixedUpdate( )
    {
        if ( _active || _alwaysActive )
        {
            foreach ( var body in _currentBodies )
            {
                if (body.TryGetComponent(out Player1Controller lol)) {
                    lol.ApplyVelocity(_power * ((Vector2)transform.up).normalized, PlayerForce.Decay);
                    Debug.Log("DAS");
                }
                else {
                    body.velocity += _power * ( ( Vector2 )transform.up ).normalized;
                }
                
            }
        }
    }


    private void OnCollisionEnter2D( Collision2D col )
    {
        _currentBodies.Add( col.rigidbody );
    }

    private void OnCollisionExit2D( Collision2D other )
    {
        _currentBodies.Remove( other.rigidbody );
    }

    public void OnPress( )
    {
        _active = true;
    }

    public void OnReleace( )
    {
        _active = false;
    }
}