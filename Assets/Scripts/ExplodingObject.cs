using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ExplodingObject : MonoBehaviour
{
    [SerializeField] private LayerMask _hitLayers;
    [SerializeField] private float _explosionRadius = 1f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private ParticleSystem _particles;
    private Rigidbody2D _rigidbody;

    private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

    public void AddForce(Vector2 force) => _rigidbody.AddForce(force, ForceMode2D.Impulse);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] hitArea = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach(Collider2D collider in hitArea)
        {
            if(HelperFunctions.PartOfLayerMask(collider.gameObject, _hitLayers))
            {
                if(collider.TryGetComponent(out IDamageTarget damageTarget))
                {
                    damageTarget.DecreaseHealth(_damage);
                }
            }
        }

        Instantiate(_particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos() => Gizmos.DrawWireSphere(transform.position, _explosionRadius);
}
