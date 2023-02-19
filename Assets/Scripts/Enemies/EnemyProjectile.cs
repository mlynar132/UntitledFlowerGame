using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private LayerMask _damageLayers;
    private float _movementSpeed;
    private int _damage = 1;

    public void SetStats(float speed, int damage)
    {
        _movementSpeed = speed;
        _damage = damage;
    }

    private void Update()
    {
        transform.position += _movementSpeed * Time.deltaTime * transform.forward;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(HelperFunctions.PartOfLayerMask(collision.gameObject, _damageLayers))
        {
            if(collision.TryGetComponent(out IDamageTarget damageTarget))
                damageTarget.DecreaseHealth(_damage);
        }

        Destroy(gameObject);
    }
}