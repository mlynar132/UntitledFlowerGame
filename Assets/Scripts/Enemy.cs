using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IStunable, IKillable
{
    [SerializeField] private float _movementSpeed = 100;
    [SerializeField] private int _maxHealth;
    private int _health;
    private bool _isStuned = false;
    [SerializeField]private float _stundDuration;

    private float xDir = 1;

    private Rigidbody2D _rigidbody;

    private Coroutine _stunCoroutine;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!_isStuned)
        {
            _rigidbody.velocity = new Vector2(_movementSpeed * xDir * Time.deltaTime, _rigidbody.velocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (collision.contacts[0].normal.y == 0)
            {
                xDir = -xDir;
            }
        }
    }
    public void Stun()
    {
        _isStuned = true;
        _stunCoroutine = StartCoroutine(StunCoroutine());
    }
    IEnumerator StunCoroutine()
    {
        yield return new WaitForSeconds(_stundDuration);
        _isStuned = false;
    }
    public void TakeDamage(int damage)
    {
        if (_isStuned)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Death();
            }
        }
    }
    public void Death()
    {
        if (_health <= 0)
        {
            Destroy(this);
        }
    }
}