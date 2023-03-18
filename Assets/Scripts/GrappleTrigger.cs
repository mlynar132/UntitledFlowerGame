using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class GrappleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectToEnable;
    [SerializeField] private Player1Controller _player1Controller;
    private IPlayer1Controller _player1;
    private bool _isActive;

    private void Start()
    {
        if(_player1Controller != null)
            _player1 = _player1Controller.GetComponent<IPlayer1Controller>();

        _player1.AnchorUsed += CheckEvent;
        _gameObjectToEnable.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player 1"))
            _isActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player 1"))
            _isActive = false;
    }

    private void CheckEvent(Vector2 v)
    {
        if(_isActive)
            _gameObjectToEnable.SetActive(true);
    }
}