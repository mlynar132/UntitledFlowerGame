using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField] private GameObject _ropeObject;
    [SerializeField] private Camera _camera;
    [SerializeField] private Rope _rope;
    [SerializeField, Range( 1, 100 )] private float _range;

    private void Start( )
    {
        _ropeObject.SetActive( false );
    }

    private void Update( )
    {
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            Throw();
        }

        if ( !Input.GetMouseButton( 0 ) )
        {
            Release();
        }

        var playerPos = transform.position;
        var camTransform = _camera.transform;
        var camPos = camTransform.position;
        playerPos.z = camPos.z;

        camTransform.position = playerPos;
    }

    public void Throw( )
    {
        // Get where in the world we want to grapple
        var mousePosition = ( Vector2 )Input.mousePosition;
        var ray = _camera.ScreenPointToRay( mousePosition );

        if ( Physics.Raycast( ray, out var backHit, 100f ) )
        {
            var backHitPoint = backHit.point;
            backHitPoint.z = 0;

            var position = transform.position;

            _rope.Grapple( position, ( backHitPoint - position ).normalized, _range, true );
            _ropeObject.SetActive( true );
        }
    }

    public void Release( )
    {
        _ropeObject.SetActive( false );
    }
}