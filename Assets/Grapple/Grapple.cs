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
    [SerializeField] private GameObject _selectorPrefab;

    private GameObject _selectorObject;
    [SerializeField] private LineRenderer _line;

    private Vector2 _selector;
    private Vector2 _aim;
    private RaycastHit2D _lastHit;

    [SerializeField] private Player2Controller _p2;
    private IPlayer2Controller _playerInterface;

    private void Awake( )
    {
        _playerInterface = GetComponent<IPlayer2Controller>();
    }

    private void Start( )
    {
        if ( !_selectorObject )
        {
            _selectorObject = Instantiate( _selectorPrefab );
        }

        _playerInterface.GrappleAim += Aim;
        _playerInterface.GrappleStart += Pull;
        Physics2D.queriesStartInColliders = false;
    }

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
            var position = transform.position + new Vector3( 0, 0.5f, 0 );

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
                        target = hit2.collider.transform.position;
                    }
                }

                _selector = target;

                if ( _selector != Vector2.zero )
                {
                    _selectorObject.SetActive( true );
                }
                else
                {
                    _selectorObject.SetActive( false );
                }

                return;
            }
        }
        else
        {
            _lastHit = new RaycastHit2D();
        }

        _selector = Vector2.zero;
        _selectorObject.SetActive( false );
    }


    private void Pull( )
    {
        if ( !_selectorObject.activeSelf )
        {
            return; // Ignore if its off
        }

        //_p2.TakeAwayControl(false);
        Vector2 position = transform.position;
        var xDis = Mathf.Abs( _selector.x - position.x );
        var yDis = Mathf.Abs( _selector.y - position.y );
        var direction = ( _selector - position ).normalized;

        var velocity = Vector2.zero;
        velocity.x = Mathf.Sign( direction.x ) * ( Mathf.Sqrt( xDis ) * _force );
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

        _p2.ApplyVelocity( velocity, PlayerForce.Decay );
        //_rb.velocity = velocity;
    }


    // Controlls
    public void Aim( Vector2 dir )
    {
        _aim = dir.normalized;
    }

    public void TestAim( InputAction.CallbackContext context )
    {
        //_aim = context.ReadValue<Vector2>().normalized;
    }

    /*public void Grab( InputAction.CallbackContext context )
    {
        if ( context.ReadValueAsButton() && context.started )
        {
            Pull( _selector );
        }
        if (context.canceled) {
          //  _p2.ReturnControl();
        }
    }*/
}