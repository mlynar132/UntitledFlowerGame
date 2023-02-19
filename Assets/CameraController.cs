using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _p1;
    [SerializeField] private Transform _p2;
    [SerializeField] private float _depth;
    [SerializeField] private float _pading;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _minDepth;
    [SerializeField] private Camera _camera;
    private Vector3 _midway;

    private void OnValidate( )
    {
        if ( _p1 && _p2 ) // Shows camera how it will look during runtime in editor
        {
            _depth = ( ( Mathf.Abs( _p1.position.x - _p2.position.x ) * _pading ) / 2 ) *
                     ( 1 / ( Mathf.Tan( ( _camera.fieldOfView * Mathf.Deg2Rad ) / 2 ) ) );
            _depth = Mathf.Max( _depth, _minDepth );
            _midway = new Vector3( ( _p1.position.x + _p2.position.x ) / 2, ( _p1.position.y + _p2.position.y ) / 2,
                -_depth );
            transform.position = _midway + _offset;
        }
    }

    private void Update( )
    {
        _depth = ( ( Mathf.Abs( _p1.position.x - _p2.position.x ) * _pading ) / 2 ) *
                 ( 1 / ( Mathf.Tan( ( _camera.fieldOfView * Mathf.Deg2Rad ) / 2 ) ) );
        _depth = Mathf.Max( _depth, _minDepth );
        _midway = new Vector3( ( _p1.position.x + _p2.position.x ) / 2, ( _p1.position.y + _p2.position.y ) / 2,
            -_depth );
        transform.position = _midway + _offset;
    }
}