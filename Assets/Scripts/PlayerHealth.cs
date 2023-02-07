using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageTarget
{
    [SerializeField] private DefaultEvent _playerDeathEvent;
    [SerializeField] private IntEvent _playerHealthEvent;

    public void KillTarget() => PlayerDied();

    private void PlayerDied()
    {
        _playerDeathEvent.InvokeEvent();
        gameObject.SetActive(false);
    }

    public void DecreaseHealth(int amount)
    {
        int newHealth = _playerHealthEvent.currentValue - amount;

        if (newHealth <= 0)
        {
            _playerHealthEvent.ChangeValue(0);
            PlayerDied();
        }
        else
        {
            _playerHealthEvent.ChangeValue(newHealth);
        }
    }
}
