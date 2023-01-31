using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField] private GameObject _ropeO;
    [SerializeField] private Camera _camera;
    [SerializeField] private Rope _rope;

    private void Start( )
    {
        _ropeO.SetActive( false );
    }

    private void Update( )
    {
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            // Get where in the world we want to grapple
            var mousePosition = ( Vector2 )Input.mousePosition;
            var ray = _camera.ScreenPointToRay( mousePosition );

            if ( Physics.Raycast( ray, out var hit, 100f ) )
            {
                var position = hit.point;
                position.z = 0;
                _rope.MoveRope( transform.position, position );
                _ropeO.SetActive( true );

            }
        }

        if ( !Input.GetMouseButton( 0 ) )
        {
            _ropeO.SetActive( false );
        }
    }
}