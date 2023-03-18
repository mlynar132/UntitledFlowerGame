using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class CharacterSelection : MonoBehaviour
{
    [Serializable]
    private class OnBack : UnityEvent
    {
    }

    [SerializeField] private GameObject _leftSelector;
    [SerializeField] private GameObject _rightSelector;
    [SerializeField] private GameObject _centerSelector;
    [SerializeField] private GameObject _c1Ready;
    [SerializeField] private GameObject _c2Ready;
    [SerializeField] private CharacterSelection _otherSelector;
    [SerializeField] private OnBack _backEvent;
    [SerializeField] private int _player;

    private bool Ready
    {
        get => _ready;
        set
        {
            _ready = value;

            if ( _ready )
            {
                _manager.Readys++;
            }
            else
            {
                _manager.Readys--;
            }
        }
    }

    private bool _ready;
    private int _currentSelected;
    private PlayerInput _playerInput;
    private SelectionManager _manager;

    private void OnEnable( )
    {
        _currentSelected = 0;
        _c1Ready.SetActive( false );
        _c2Ready.SetActive( false );
        ChangeSelect( _currentSelected );
    }

    private void Start( )
    {
        _playerInput = GetComponent<PlayerInput>();
        _manager = GetComponentInParent<SelectionManager>();
    }

    public void Select( InputAction.CallbackContext context )
    {
        if ( context.started && context.ReadValueAsButton() )
        {
            if ( Ready )
            {
                _manager.StartGame();
            }
            else
            {
                switch ( _currentSelected )
                {
                    case -1:
                        _c1Ready.SetActive( true );
                        Ready = true;
                        // PlayerDevices.character1Controller = _playerInput.GetDevice<Gamepad>(); // Removed so it works with keyboard while testing
                        PlayerDevices.character1Controller = _playerInput.devices[0];
                        PlayerDevices.char1Player = _player;
                        break;
                    case 1:
                        _c2Ready.SetActive( true );
                        Ready = true;
                        // PlayerDevices.character2Controller = _playerInput.GetDevice<Gamepad>(); // Removed so it works with keyboard while testing
                        PlayerDevices.character2Controller = _playerInput.devices[0];
                        PlayerDevices.char2Player = _player;
                        break;
                }
            }
        }
    }

    public void Move( InputAction.CallbackContext context )
    {
        if ( !context.started || Ready )
            return;


        var dir = Mathf.RoundToInt( Mathf.Sign( context.ReadValue<float>() ) );

        _currentSelected += dir;

        _currentSelected = Mathf.Clamp( _currentSelected, -1, 1 );

        if ( _otherSelector._currentSelected == _currentSelected && _currentSelected != 0 )
        {
            _currentSelected -= dir;

            _currentSelected = Mathf.Clamp( _currentSelected, -1, 1 );
        }

        ChangeSelect( _currentSelected );
    }

    public void Back( InputAction.CallbackContext context )
    {
        if ( context.started && context.ReadValueAsButton() )
        {
            _manager.UnreadyGame();

            if ( Ready )
            {
                switch ( _currentSelected )
                {
                    case -1:
                        _c1Ready.SetActive( false );
                        break;
                    case 1:
                        _c2Ready.SetActive( false );
                        break;
                }

                Ready = false;
            }
            else
            {
                Ready = false;
                _manager.Readys = 0;
                _backEvent.Invoke();
            }
        }
    }

    private void ChangeSelect( int i )
    {
        _leftSelector.SetActive( i == -1 );
        _centerSelector.SetActive( i == 0 );
        _rightSelector.SetActive( i == 1 );
    }
}