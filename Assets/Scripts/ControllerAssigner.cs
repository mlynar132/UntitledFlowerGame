using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerAssigner : MonoBehaviour
{
    [SerializeField, Range( 1, 2 )] private int _character;
    private PlayerInput _input;
    private InputDevice _controller;

    private void Start( )
    {
        _input = GetComponent<PlayerInput>();

        switch ( _character )
        {
            case 1:
                _controller = PlayerDevices.character1Controller;
                break;
            case 2:
                _controller = PlayerDevices.character2Controller;
                break;
        }

        if ( _controller != null )
            _input.SwitchCurrentControlScheme( _controller );
    }
}