using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlateMode
{
    PlayerOne,
    PlayerTwo,
    Both,
    Either
}

public class PPlate : MonoBehaviour
{
    [SerializeField] private PlateMode _plateMode;
    [SerializeField] private GameObject[] _poweredObjects;

    private IPowered[] _powereds;
    private Collider2D[] _currentColliders = new Collider2D[2];

    private void Start( )
    {
        _powereds = new IPowered[_poweredObjects.Length];

        for ( int i = 0; i < _poweredObjects.Length; i++ )
        {
            _powereds[i] = _poweredObjects[i].GetComponent<IPowered>();
        }
    }

    // WARNING! Lost of ugly logic below. Im truly sorry

    private bool PlayerCheck( int i )
    {
        if ( i == 0 ) // The collision wasnt a player, so ignore it. Should never happen tbh
        {
            return false;
        }

        var correctPlayer = false;

        switch ( _plateMode ) // Check if the player is needed for the plate to be active
        {
            case PlateMode.PlayerOne:
                if ( i == 1 )
                {
                    correctPlayer = true;
                }

                break;
            case PlateMode.PlayerTwo:
                if ( i == 2 )
                {
                    correctPlayer = true;
                }

                break;
            case PlateMode.Both:
                if ( ( i == 1 && _currentColliders[1] ) || ( i == 2 && _currentColliders[0] ) )
                {
                    correctPlayer = true;
                }

                break;
            case PlateMode.Either:
                if ( ( i == 1 && !_currentColliders[1] ) || ( i == 2 && !_currentColliders[0] ) )
                {
                    correctPlayer = true;
                }

                break;
        }

        return correctPlayer;
    }

    private bool PlayerAdd( Collider2D player )
    {
        var thisPlayer = 0;

        if ( player.CompareTag( "Player 1" ) )
        {
            _currentColliders[0] = player;
            thisPlayer = 1;
        }
        else if ( player.CompareTag( "Player 2" ) )
        {
            _currentColliders[1] = player;
            thisPlayer = 2;
        }

        return PlayerCheck( thisPlayer ); // Player that was added turned the plate on
    }

    private bool PlayerRemove( Collider2D player )
    {
        var thisPlayer = 0;

        if ( player.CompareTag( "Player 1" ) )
        {
            _currentColliders[0] = null;
            thisPlayer = 1;
        }
        else if ( player.CompareTag( "Player 2" ) )
        {
            _currentColliders[1] = null;
            thisPlayer = 2;
        }

        return PlayerCheck( thisPlayer ); // Player that left changes the state of the plate
    }


    private void OnTriggerEnter2D( Collider2D other )
    {
        if ( PlayerAdd( other ) )
        {
            foreach ( var powered in _powereds )
            {
                powered.OnPress();
            }
        }
    }

    private void OnTriggerStay2D( Collider2D other )
    {
        var thisPlayer = 0;

        if ( other.CompareTag( "Player 1" ) )
        {
            thisPlayer = 1;
        }
        else if ( other.CompareTag( "Player 2" ) )
        {
            thisPlayer = 2;
        }

        if ( PlayerCheck( thisPlayer ) )
        {
            foreach ( var powered in _powereds )
            {
                powered.OnHold();
            }
        }
    }

    private void OnTriggerExit2D( Collider2D other )
    {
        if ( PlayerRemove( other ) )
        {
            foreach ( var powered in _powereds )
            {
                powered.OnReleace();
            }
        }
    }
}