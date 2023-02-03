using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, DamageTarget
{
    [Header("Events")]
    [SerializeField] private IntEvent _playerHealthEvent;
    [SerializeField] private DefaultEvent _playerDeathEvent;
    [SerializeField] private BoolEvent _inDarknessEvent;
    [SerializeField] private FloatEvent _playerDarknessEvent;
    [SerializeField] private BoolEvent _oxygenLossEvent;

    [Header("Stats")]
    [SerializeField] private float _darknessTimerLength = 10;

    [SerializeField] private GameObject _player1;
    [SerializeField] private GameObject _player2;

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
        _playerHealthEvent.ResetValue();

        _playerDarknessEvent.ResetValue();
        _inDarknessEvent.ResetValue();

        _player1.SetActive(false);
        _player2.SetActive(false);
    }

    private void Update()
    {
        if(_inDarknessEvent.currentValue == true || _oxygenLossEvent.currentValue == true)
        {
            float t = Mathf.Lerp(0, _darknessTimerLength, _playerDarknessEvent.currentValue) + Time.deltaTime;

            float lerpedValue = Mathf.InverseLerp(0, _darknessTimerLength, t);

            if (_playerDarknessEvent.currentValue >= 1)
            {
                _playerDarknessEvent.ChangeValue(1);
                PlayerDied();
            }
            else
            {
                _playerDarknessEvent.ChangeValue(lerpedValue);
            }
        }
    }
}
