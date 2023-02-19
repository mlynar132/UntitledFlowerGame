using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField] private int _healthValue = 1;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth playerHealth))
        {
            if(!playerHealth.HasMaxHealth())
            {
                playerHealth.IncreaeseHealth(_healthValue);
                Destroy(gameObject);
            }
        }
    }
}
