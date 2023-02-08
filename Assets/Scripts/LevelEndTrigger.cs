using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private DefaultEvent _openWinScreenCanvas;
    [SerializeField] private DefaultEvent _lastLevelCleared;
    [SerializeField] private IntEvent _levelEvent;
    [SerializeField] private Levels _levels;
    [SerializeField] private float _levelEndTimer = 1;
    private bool _playerTouching;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (_playerTouching)
            {
                StartCoroutine(LevelEndTimer());
            }
            else
            {
                _playerTouching = true;
            }
        }
    }

    private IEnumerator LevelEndTimer()
    {
        yield return new WaitForSeconds(_levelEndTimer);

        if(_levels.LevelAssets.Length > _levelEvent.currentValue + 1)
        {
            _openWinScreenCanvas.InvokeEvent();
        }
        else
        {
            _lastLevelCleared.InvokeEvent();
        }

    }
}
