using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private bool _startSpawnPoint;
    [SerializeField] private Transform[] _spawningPositions;

    private bool _player1Check;
    private bool _player2Check;
    private bool _activated;

    private void Awake( )
    {
        if ( !_startSpawnPoint || _activated ) return;

        if ( RespawnManager.IsEmpty )
        {
            Debug.Log( _spawningPositions[0].position );
            RespawnManager.ActivatePoint( _spawningPositions, SceneManager.GetActiveScene().name );
        }

        _activated = true;
    }

    private void OnTriggerEnter2D( Collider2D col )
    {
        if ( _activated ) return;

        if ( col.CompareTag( "Player 1" ) )
        {
            _player1Check = true;
        }

        if ( col.CompareTag( "Player 2" ) )
        {
            _player2Check = true;
        }

        if ( _player1Check && _player2Check )
        {
            _activated = true;
            RespawnManager.ActivatePoint( _spawningPositions, SceneManager.GetActiveScene().name );
        }
    }
}