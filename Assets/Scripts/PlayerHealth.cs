using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, DamageTarget
{
    [SerializeField] private IntEvent _playerHealthEvent;
    [SerializeField] private DefaultEvent _playerDeathEvent;

    private void OnEnable() => _playerHealthEvent.Event.AddListener(Test);

    private void OnDisable() => _playerHealthEvent.Event.RemoveListener(Test);

    public void DecreaseHealth(int amount)
    {
        int newHealth = _playerHealthEvent.currentValue - amount;

        if(newHealth <= 0)
        {
            _playerHealthEvent.ChangeValue(0);
            PlayerDied();
        }
        else
        {
            _playerHealthEvent.ChangeValue(newHealth);
        }
    }

    public void KillTarget() => PlayerDied();

    private void PlayerDied()
    {
        _playerDeathEvent.InvokeEvent();
        gameObject.SetActive(false);
    }



    [ContextMenu("Test")]
    private void HealthTest()
    {
        DecreaseHealth(20);
    }

    private void Test(int value)
    {
        Debug.Log(value);
    }
}
