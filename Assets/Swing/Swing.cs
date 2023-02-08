using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Swing : MonoBehaviour
{
    [SerializeField] private GameObject _ropeObject;
    [SerializeField] private Rope _rope;
    [SerializeField, Range( 1, 100 )] private float _range;

    private Vector2 _currentAim;

    private void Start( )
    {
        _ropeObject.SetActive( false );
    }

    public void Aim( InputAction.CallbackContext context )
    {
        _currentAim = context.ReadValue<Vector2>();
    }

    public void Throw( InputAction.CallbackContext context )
    {
        if ( context.ReadValueAsButton() )
        {
            var position = transform.position;

            if ( _rope.Grapple( position, _currentAim, _range, true ) != Vector2.zero )
            {
                _ropeObject.SetActive( true );
            }
        }
        else
        {
            _ropeObject.SetActive( false );
        }
    }
}