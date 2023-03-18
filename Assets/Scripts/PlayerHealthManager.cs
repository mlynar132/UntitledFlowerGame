using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private BoolEvent _inDarknessEvent;
    [SerializeField] private FloatEvent _playerDarknessEvent;
    [SerializeField] private BoolEvent _oxygenLossEvent;
    [SerializeField] private IntEvent _player1HealthEvent;
    [SerializeField] private IntEvent _player2HealthEvent;
    [SerializeField] private DefaultEvent _playerDeathEvent;

    [Header("Stats")]
    [SerializeField] private float _darknessTimerLength = 10;

    private void Awake()
    {
        _playerDarknessEvent.ResetValue();
        _inDarknessEvent.ResetValue();
        _player1HealthEvent.ResetValue();
        _player2HealthEvent.ResetValue();
        _playerDeathEvent.Event.AddListener(PlayerDied);
    }

    private void PlayerDied() => StartCoroutine(RespawnTimer());

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(1);
        RespawnManager.Respawn();
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
