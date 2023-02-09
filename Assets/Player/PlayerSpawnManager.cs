using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _player1Prefab;
    [SerializeField] private GameObject _player2Prefab;
    [SerializeField] private PlayerInputManager _playerInputManager;
    void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("ASD:" + playerInput.playerIndex);
        if (playerInput.playerIndex == 0) {
            _playerInputManager.playerPrefab = _player1Prefab;
        }

    }
}
