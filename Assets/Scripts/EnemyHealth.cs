using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageTarget
{
    [SerializeField] private int _startHealth = 3;

    [SerializeField] private int _health;

    private void Start()
    {
        _health = _startHealth;
    }

    public void DecreaseHealth(int value)
    {
        _health--;

        if(_health <= 0)
        {
            KillTarget();
        }
    }

    public void KillTarget()
    {
        Destroy(gameObject);
    }
}
