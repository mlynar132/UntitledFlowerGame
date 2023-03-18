using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour, IPowered
{
    [SerializeField, Range( 0, 100 )] private float _openDistance;
    [SerializeField, Range( 0.01f, 10 )] private float _openTime;

    [SerializeField] private Transform _topPiece;
    [SerializeField] private GameObject _support;

    private float _currentOpeningTime;
    private Vector3 _startingScale;
    private Vector3 _startPos;
    private bool _opening;
    private Mesh _mesh;

    private void Start( )
    {
        _mesh = _support.GetComponent<MeshFilter>().mesh;
        _startingScale = _support.transform.localScale;
        _startPos = _topPiece.position;
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

        var newScale = Mathf.Lerp( _openDistance, 0,
            _currentOpeningTime / _openTime );

        var scale = _startingScale;
        scale.y += newScale * _startingScale.y;
        _support.transform.localScale = scale;

        var pos = _startPos;
        pos.y += ( _mesh.bounds.size.y * ( scale.y - _startingScale.y ) );

        _topPiece.position = pos;
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