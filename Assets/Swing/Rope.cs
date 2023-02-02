using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class Rope : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _boundRb;
    [SerializeField] private GameObject _grapple;
    [SerializeField] private List<Vector2> _refPoints;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LineRenderer _line;

    private void FixedUpdate( )
    {
        var playerPos = _boundRb.position;
        var grapplePos = ( Vector2 )_grapple.transform.position;


        for ( var i = 0; i < _refPoints.Count; i++ )
        {
            var point = _refPoints[i];
            var pointDist = Vector2.Distance( playerPos, point ) * 0.99f; // To avoid it getting stuck, do slightly less
            var refHit = Physics2D.Raycast( playerPos, ( point - playerPos ).normalized, pointDist, _layerMask );

            if ( !refHit )
            {
                grapplePos = point;

                _refPoints.RemoveRange( i, _refPoints.Count - i );
            }
        }


        var distance = Vector2.Distance( grapplePos, playerPos );
        var hit = Physics2D.Raycast( playerPos, ( grapplePos - playerPos ).normalized, distance, _layerMask );

        if ( hit )
        {
            _refPoints.Add( grapplePos );
            grapplePos = hit.point;
        }

        _grapple.transform.position = grapplePos;
    }

    private void LateUpdate( )
    {
        // Player and grapple adds 2
        _line.positionCount = _refPoints.Count + 2;

        var points = new List<Vector3>();
        points.Add( _boundRb.position );
        points.Add( _grapple.transform.position );

        foreach ( var point in _refPoints )
        {
            points.Add( point );
        }

        _line.SetPositions( points.ToArray() );
    }

    public void Grapple( Vector2 target )
    {
        _grapple.transform.position = target;
        _refPoints.Clear();
    }
}