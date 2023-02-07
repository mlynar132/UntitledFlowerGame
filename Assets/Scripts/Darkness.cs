using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    [SerializeField] private BoolEvent _inDarknessEvent;

    private bool _inDarkness;
    private bool _bothPlayersInDarkness;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(_inDarkness && !_bothPlayersInDarkness)
            {
                _bothPlayersInDarkness = true;
            }
            else 
            {
                _inDarkness = !_inDarkness;
                _inDarknessEvent.ChangeValue(_inDarkness);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_bothPlayersInDarkness)
            {
                _bothPlayersInDarkness = false;
            }
            else
            {
                _inDarkness = !_inDarkness;
                _inDarknessEvent.ChangeValue(_inDarkness);
            }
        }
    }
}