using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private DefaultEvent _playerDeathEvent;
    [SerializeField] private float _playerRespawnTime = 1;

    private void OnEnable() => _playerDeathEvent.Event.AddListener(PlayerDied);

    private void OnDisable() => _playerDeathEvent.Event.RemoveListener(PlayerDied);

    private void PlayerDied() => StartCoroutine(RespawnTimer());

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(_playerRespawnTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
