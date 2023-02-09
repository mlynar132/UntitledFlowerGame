using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleTerrain : MonoBehaviour, IDamageTarget
{
    [SerializeField] private int _starthealth = 5;
    [SerializeField] private int _health = 5;

    private void Start() => _health = _starthealth;

    public void DecreaseHealth(int value)
    {
        _health--;

        if (_health <= 0)
        {
            KillTarget();
        }
    }

    public void KillTarget() => Destroy(gameObject);
}
