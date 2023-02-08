using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IPowered
{
    [SerializeField, Range( 0, 100 )] private float _openDistance;
    [SerializeField, Range( 0.01f, 10 )] private float _openTime;

    private float _currentOpeningTime;
    private Vector3 _startPos;
    private bool _opening;

    private void Start( )
    {
        _startPos = transform.position;
        _currentOpeningTime = _openTime;
    }

    private void Update( )
    {
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

        transform.position = _startPos + ( transform.up * newHeight );
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