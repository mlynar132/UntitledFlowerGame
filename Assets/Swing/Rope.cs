using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class Rope : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _boundRb;
    [SerializeField] private Transform _swingPoint;
    [SerializeField] private DistanceJoint2D _joint;
    [SerializeField] private List<Vector3> _refPoints; // Z value is which way the point faces
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LineRenderer _line;
    [SerializeField, Range( 0.1f, 1f )] private float _radius;

    private void OnValidate( )
    {
        _line.startWidth = _radius;
        _joint.connectedBody = _boundRb;
    }

    private void FixedUpdate( )
    {
        Vector2 playerPos = _swingPoint.position;
        Vector2 grapplePos = transform.position;

        var distance = Vector2.Distance( grapplePos, playerPos );
        var direction = ( grapplePos - playerPos ).normalized;

        var newGrapple = Grapple( playerPos, direction, distance, false );

        if ( newGrapple != Vector2.zero )
        {
            grapplePos = newGrapple;
        }


        for ( var i = _refPoints.Count - 1; i >= 0; i-- )
        {
            Vector2 point = _refPoints[i];
            var pointFacing = _refPoints[i].z; // -1 for counter clock, 1 for clock
            var pointDist = Vector2.Distance( playerPos, point );

            // Raycast instead of circle cast gives breathing room
            var refHit = Physics2D.Raycast( playerPos, ( point - playerPos ).normalized, pointDist, _layerMask );

            if ( !refHit )
            {
                var forwards = Vector2.Perpendicular( ( point - grapplePos ).normalized );

                var playerFwd = ( grapplePos - playerPos ).normalized;

                // Check if player is on the correct side of the rope to do the next point
                var dotProd = Vector2.Dot( forwards, playerFwd );
                dotProd *= pointFacing;

                if ( dotProd >= 0 )
                {
                    // Debug.Log( "Forwards: " + forwards );
                    // Debug.Log( "Player Forwards: " + playerFwd );
                    // Debug.Log( "Dot: " + dotProd );

                    grapplePos = point;
                    _refPoints.RemoveAt( i );
                }
            }
            else
            {
                break; // If cant connect to point, dont check the rest of them
            }
        }

        transform.position = grapplePos;
    }

    private void LateUpdate( )
    {
        // Player and grapple adds 2
        _line.positionCount = _refPoints.Count + 2;

        var points = new List<Vector3>();
        points.Add( _boundRb.position );
        points.Add( transform.position );

        for ( var i = _refPoints.Count - 1; i >= 0; i-- )
        {
            points.Add( _refPoints[i] );
        }

        _line.SetPositions( points.ToArray() );
    }

    public Vector2 Grapple( Vector2 origin, Vector2 direction, float distance, bool clear )
    {
        var grapplePos = Vector2.zero;

        var hit = Physics2D.CircleCast( origin, _radius, direction, distance, _layerMask );

        if ( hit )
        {
            grapplePos = transform.position;

            var centerHit = ( hit.distance * direction ) + origin; // Center of circle that did the hit

            if ( Vector2.Distance( centerHit, grapplePos ) >= _radius ) // Dont care if its the same point as before
            {
                Vector3 newPoint = grapplePos;
                grapplePos = centerHit;
                var forwards = Vector2.Perpendicular( ( ( Vector2 )newPoint - grapplePos ).normalized );
                newPoint.z = Vector2.Dot( _boundRb.velocity.normalized, forwards );
                _refPoints.Add( newPoint );
                transform.position = grapplePos;
            }
            else
            {
                grapplePos = Vector2.zero; // Lie and say it missed
            }
        }

        if ( clear )
        {
            _refPoints.Clear();
            // Debug.Log( grapplePos );
        }

        return grapplePos;
    }
}