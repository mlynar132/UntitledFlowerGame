using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IPowered
{
    private enum OpeningMode
    {
        Move,
        Shrink
    }

    [SerializeField, Range( 0, 100 )] private float _openDistance;
    [SerializeField, Range( 0.01f, 10 )] private float _openTime;
    [SerializeField] private OpeningMode _openingMode;

    private float _currentOpeningTime;
    private Vector3 _startPos;
    private Vector3 _startPoslocal;
    private Vector3 _startingScale;
    private bool _opening;
    private Mesh _mesh;

    private void Start( )
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        _startPos = transform.position;
        _startPoslocal = transform.localPosition;
        _startingScale = transform.localScale;
        _currentOpeningTime = _openTime;
    }

    private void Update( )
    {
        switch ( _openingMode )
        {
            case OpeningMode.Move:
                if ( _opening )
                {
                    _currentOpeningTime -= Time.deltaTime;
                }
                else
                {
                    _currentOpeningTime += Time.deltaTime;
                }

                _currentOpeningTime = Mathf.Clamp( _currentOpeningTime, 0, _openTime );

                var newHeight = Mathf.Lerp( _openDistance, 0,
                    _currentOpeningTime / _openTime );

                transform.position = _startPos + transform.up * newHeight;
                break;

            case OpeningMode.Shrink:
                if ( _opening )
                {
                    _currentOpeningTime -= Time.deltaTime;
                }
                else
                {
                    _currentOpeningTime += Time.deltaTime;
                }

                _currentOpeningTime = Mathf.Clamp( _currentOpeningTime, 0, _openTime );

                var newScale = Mathf.Lerp( _startingScale.y, 0,
                    _currentOpeningTime / _openTime );

                var height = Mathf.Lerp( _mesh.bounds.size.y, 0,
                    _currentOpeningTime / _openTime );

                transform.localPosition = _startPoslocal + transform.up * ( height * _startingScale.y / 2 );
                var scale = _startingScale;
                scale.y -= newScale;
                transform.localScale = scale;
                break;
        }
    }


    public void OnPress( )
    {
        _opening = true;
    }

    public void OnReleace( )
    {
        _opening = false;
    }
}