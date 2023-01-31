using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 100;

    private float xDir = 1;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_movementSpeed * xDir * Time.deltaTime, _rigidbody.velocity.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            if (collision.contacts[0].normal.y == 0)
            {
                xDir = -xDir;
            }
        }
    }
}