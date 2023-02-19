using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageTarget
{
    [SerializeField] private DefaultEvent _playerDeathEvent;
    [SerializeField] private IntEvent _playerHealthEvent;
    [SerializeField] private EventReference _deathSound;

    public void KillTarget( ) => PlayerDied();

    public bool HasMaxHealth() => _playerHealthEvent.currentValue == _playerHealthEvent.startValue;

    private void PlayerDied( )
    {
        RuntimeManager.PlayOneShot(_deathSound, transform.position);
        StartCoroutine(RespawnTimer());
        // _playerDeathEvent.InvokeEvent();
        // gameObject.SetActive(false);
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(1);
        RespawnManager.Respawn();
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

    public void IncreaeseHealth(int amount)
    {
        int newHealth = _playerHealthEvent.currentValue + amount;

        if(newHealth >= _playerHealthEvent.startValue)
        {
            _playerHealthEvent.ChangeValue(_playerHealthEvent.startValue);
        }
        else
        {
            _playerHealthEvent.ChangeValue(newHealth);
        }
    }
}