using System;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageTarget
{
    [SerializeField] private DefaultEvent _playerDeathEvent;
    [SerializeField] private IntEvent _playerHealthEvent;
    [SerializeField] private EventReference _deathSound;

    private ParticleSystem _particle;

    public int Health => _playerHealthEvent.currentValue;

    private void Awake( )
    {
        _particle = GetComponent<ParticleSystem>();
    }

    public void KillTarget( ) => PlayerDied();

    public bool HasMaxHealth( ) => _playerHealthEvent.currentValue == _playerHealthEvent.startValue;

    private void PlayerDied( )
    {
        RuntimeManager.PlayOneShot( _deathSound, Camera.main.transform.position );
        _playerDeathEvent.InvokeEvent();
        _particle.Play();
        transform.GetChild( 0 ).gameObject.SetActive( false );
    }

    public void DecreaseHealth( int amount )
    {
        int newHealth = _playerHealthEvent.currentValue - amount;

        if ( newHealth <= 0 )
        {
            _playerHealthEvent.ChangeValue( 0 );
            PlayerDied();
        }
        else
        {
            _playerHealthEvent.ChangeValue( newHealth );
        }
    }

    public void IncreaeseHealth( int amount )
    {
        int newHealth = _playerHealthEvent.currentValue + amount;

        if ( newHealth >= _playerHealthEvent.startValue )
        {
            _playerHealthEvent.ChangeValue( _playerHealthEvent.startValue );
        }
        else
        {
            _playerHealthEvent.ChangeValue( newHealth );
        }
    }
}