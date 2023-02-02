using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private Type _type;
    [SerializeField] private int _damage = 5;
    [SerializeField] private float _damageTime = 0.5f;
    private float t;
    private Collision _collision = null;
    private bool _colliding;
    private enum Type
    {
        Damage,
        InstaDeath
    }

    private void OnCollisionEnter(Collision collision)
    {
        t = 0;
        _colliding = true;
        _collision = collision;
    }

    private void OnCollisionExit(Collision collision)
    {
        _colliding = false;
        _collision = null;
    }

    private void Update()
    {
        if (!_colliding) return;

        switch (_type)
        {
            case Type.Damage:
                if (_collision.gameObject.TryGetComponent(out DamageTarget target))
                {
                    t += Time.deltaTime;

                    if(t >= _damageTime)
                    {
                        target.DecreaseHealth(5);
                        t = 0;
                    }
                }
                break;
            case Type.InstaDeath:
                if (_collision.gameObject.TryGetComponent(out DamageTarget killTarget))
                {
                    killTarget.KillTarget();
                }
                break;
        }        
    }
}