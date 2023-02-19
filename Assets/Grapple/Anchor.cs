using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent( typeof( LineRenderer ) )]
public class Anchor : MonoBehaviour
{
    private IPlayer1Controller _playerInterface;

    [SerializeField, Range( 1f, 100f )] private float _range;
    [SerializeField, Range( 1, 10 )] private int _maxAnchors;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _anchorPrefab;
    [SerializeField] private Color _color;

    private int _nextAnchor;
    private LineRenderer _line;
    private Vector2 _aim;

    private GameObject[] _anchors;

    private void Awake( )
    {
        _playerInterface = GetComponent<IPlayer1Controller>();
        _line = GetComponent<LineRenderer>();
    }

    private void Start( )
    {
        _playerInterface.AnchorUsed += PlaceAnchor;
        _line.SetPosition( 0, transform.position );

        _anchors = new GameObject[_maxAnchors];
    }

    private void LateUpdate( )
    {
        var pos = transform.position + new Vector3( 0, 0.5f, 0 );
        _line.SetPosition( 0, pos );

        var hit = Physics2D.Raycast( pos, _aim, _range, _layerMask );

        if ( hit )
        {
            _line.SetPosition( 1, hit.point );
        }
        else
        {
            _line.SetPosition( 1, ( Vector3 )( _aim * _range ) + pos );
        }
    }

    private void PlaceAnchor( Vector2 dir )
    {
        // _aim = dir; // Need aim to be constantly updated for the linerenderer every time the r-stick is changed. <3
        // Should maybe add a center transform to the players for easier finding of their center.
        var hit = Physics2D.Raycast( transform.position + new Vector3( 0, 0.5f, 0 ), _aim,
            _range, _layerMask );

        if ( hit )
        {
            if ( _anchors[_nextAnchor] == null )
            {
                _anchors[_nextAnchor] = Instantiate( _anchorPrefab );
            }

            _anchors[_nextAnchor].transform.position = hit.point;
            _anchors[_nextAnchor].transform.rotation = Quaternion.FromToRotation( Vector2.up, hit.normal );

            //color
            _anchors[_nextAnchor].GetComponent<MeshRenderer>().material.color = _color;

            _nextAnchor++;

            if ( _nextAnchor >= _anchors.Length ) // Make it loop around
            {
                _nextAnchor = 0;
            }
        }
    }


    // Controlls
    public void Aim( InputAction.CallbackContext context )
    {
        _aim = context.ReadValue<Vector2>();
    }

    // public void Place( InputAction.CallbackContext context )
    // {
    //     if ( context.ReadValueAsButton() && context.started && _delay <= 0 )
    //     {
    //         PlaceAnchor(_aim);
    //     }
    // }
}