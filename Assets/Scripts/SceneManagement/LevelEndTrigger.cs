using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private Levels _levels;
    [SerializeField] private DefaultEvent _lastLevelCleared;
    [SerializeField] private DefaultEvent _openWinScreenCanvas;
    [SerializeField] private IntEvent _levelEvent;
    [SerializeField] private float _levelEndTimer = 1;
    private bool _playerTouching;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(HelperFunctions.PartOfLayerMask(other.gameObject, _playerMask))
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (HelperFunctions.PartOfLayerMask(other.gameObject, _playerMask))
        {
            _playerTouching = false;
        }
    }

    private IEnumerator LevelEndTimer()
    {
        yield return new WaitForSeconds(_levelEndTimer);

        if (_levels.LevelIndexes.Length > _levelEvent.currentValue + 1)
        {
            Debug.Log ("Works");
            _openWinScreenCanvas.InvokeEvent();
        }
        else
        {
            _lastLevelCleared.InvokeEvent();
        }
    }
}
