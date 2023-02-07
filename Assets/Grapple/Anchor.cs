using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Anchor : MonoBehaviour
{
    [SerializeField, Range( 1f, 100f )] private float _range;
    [SerializeField, Range( 0f, 10f )] private float _cooldown;
    [SerializeField, Range( 1, 10 )] private int _maxAnchors;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _anchorPrefab;

    private int _nextAnchor;
    private Vector2 _aim;
    private float _delay;

    private GameObject[] _anchors;

    private void Start( )
    {
        _anchors = new GameObject[_maxAnchors];
    }

    private void Update( )
    {
        _delay -= Time.deltaTime;
    }

    private void PlaceAnchor( )
    {
        var hit = Physics2D.Raycast( transform.position, _aim, _range,
            _layerMask );

        if ( hit )
        {
            _delay = _cooldown;

            if ( _anchors[_nextAnchor] == null )
            {
                _anchors[_nextAnchor] = Instantiate( _anchorPrefab );
            }

            _anchors[_nextAnchor].transform.position = hit.point;
            _anchors[_nextAnchor].transform.rotation = Quaternion.FromToRotation( Vector2.up, hit.normal );
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

    public void Place( InputAction.CallbackContext context )
    {
        if ( context.ReadValueAsButton() && context.started && _delay <= 0 )
        {
            PlaceAnchor();
        }
    }
}