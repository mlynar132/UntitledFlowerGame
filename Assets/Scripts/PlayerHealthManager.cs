using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private DefaultEvent _playerDeathEvent;
    [SerializeField] private BoolEvent _inDarknessEvent;
    [SerializeField] private FloatEvent _playerDarknessEvent;
    [SerializeField] private BoolEvent _oxygenLossEvent;
    [SerializeField] private IntEvent _playerHealthEvent;

    [Header("Stats")]
    [SerializeField] private float _darknessTimerLength = 10;

    [SerializeField] private GameObject _player1;
    [SerializeField] private GameObject _player2;

    private void Start()
    {
        _playerDarknessEvent.ResetValue();
        _inDarknessEvent.ResetValue();
        _playerHealthEvent.ResetValue();
    }

    private void PlayerDied()
    {
        _playerDeathEvent.InvokeEvent();

        if(_player1 != null)
            _player1.SetActive(false);

        if(_player2 != null)
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
