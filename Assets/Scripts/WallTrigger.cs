using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class WallTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private GameObject _wallOfDeath;

    private GameObject _playerTouched;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HelperFunctions.PartOfLayerMask(other.gameObject, _playerMask))
        {
            if (other.gameObject != _playerTouched)
            {
                _wallOfDeath.SetActive(true);
            }
            else
            {
                _playerTouched = other.gameObject;
            }
        }
    }
}