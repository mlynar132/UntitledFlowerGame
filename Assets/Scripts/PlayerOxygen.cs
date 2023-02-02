using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
    [SerializeField] private Transform _player1;
    [SerializeField] private float _detectionRadius = 10;
    [SerializeField] private BoolEvent _oxygenLossEvent;

    private void Update()
    {
        if(Vector3.Distance(transform.position, _player1.position) > _detectionRadius && _oxygenLossEvent.currentValue == false)
        {
            _oxygenLossEvent.ChangeValue(true);
        }
        else if (Vector3.Distance(transform.position, _player1.position) <= _detectionRadius && _oxygenLossEvent.currentValue == true)
        {
            _oxygenLossEvent.ChangeValue(false);
        }
    }
}