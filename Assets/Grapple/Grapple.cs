using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grapple : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField, Range( 1f, 100f )] private float _range;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private LayerMask _anchorMask;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private GameObject _selectorObject;
    [SerializeField] private LineRenderer _line;

    private Vector2 _selector;
    private Vector2 _aim;
    private RaycastHit2D _lastHit;


    private void Update( )
    {
        Select();

        _selectorObject.transform.position = _selector;

        _line.SetPosition( 0, transform.position );

        if ( _lastHit )
        {
            _line.SetPosition( 1, _lastHit.point );
        }
        else
        {
            _line.SetPosition( 1, ( Vector3 )( _aim * _range ) + transform.position );
        }
    }


    private void Select( )
    {
        if ( _aim != Vector2.zero )
        {
            var position = transform.position;
            _lastHit = Physics2D.Raycast( position, _aim, _range, _collisionMask );

            if ( _lastHit )
            {
                Vector3 point = _lastHit.point;

                point.z = -1;

                var hits = Physics2D.GetRayIntersectionAll( new Ray( point, Vector3.forward ), 1f, _anchorMask );
                var minDist = float.PositiveInfinity;
                Vector2 target = Vector2.zero;

                foreach ( var hit2 in hits )
                {
                    var distance = Vector2.Distance( hit2.point, hit2.transform.position );

                    if ( distance < minDist )
                    {
                        minDist = distance;
                        target = hit2.transform.position;
                    }
                }

                _selector = target;
                return;
            }
        }

        _selector = Vector2.zero;
    }


    private void Pull( Vector2 destination )
    {
        if ( destination == Vector2.zero )
        {
            return; // Ignore zero
        }

        Vector2 position = transform.position;
        var xDis = Mathf.Abs( destination.x - position.x );
        var yDis = Mathf.Abs( destination.y - position.y );
        var direction = ( destination - position ).normalized;

        var velocity = Vector2.zero;
        velocity.x = direction.x * ( Mathf.Sqrt( xDis ) * _force );
        velocity.y = Mathf.Sign( direction.y ) * ( Mathf.Sqrt( yDis ) * _force );

        var oldVelocity = _rb.velocity;

        if ( Mathf.Abs( velocity.x + oldVelocity.x ) >= velocity.x )
        {
            velocity.x += oldVelocity.x;
        }

        if ( Mathf.Abs( velocity.y + oldVelocity.y ) >= velocity.y )
        {
            velocity.y += oldVelocity.y;
        }

        _rb.velocity = velocity;
    }


    // Controlls
    public void Aim( InputAction.CallbackContext context )
    {
        _aim = context.ReadValue<Vector2>();
    }

    public void Grab( InputAction.CallbackContext context )
    {
        if ( context.ReadValueAsButton() && context.started )
        {
            Pull( _selector );
        }
    }
}