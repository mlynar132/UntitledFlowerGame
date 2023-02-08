using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerSpawnManager : MonoBehaviour
{
    void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("ASD:" + playerInput.playerIndex);   
    }
}
