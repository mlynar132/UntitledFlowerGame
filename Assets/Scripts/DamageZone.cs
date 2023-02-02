using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private Type _type;
    [SerializeField] private int _damage = 5;
    [SerializeField] private Transform[] _returnPoints;
    private Transform _returnPoint;
    private enum Type
    {
        Damage,
        InstaDeath
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (_type)
        {
            case Type.Damage:
                if (collision.gameObject.TryGetComponent(out DamageTarget target))
                {
                    target.DecreaseHealth(_damage);
                }
                break;
            case Type.InstaDeath:
                if (collision.gameObject.TryGetComponent(out DamageTarget killTarget))
                {
                    killTarget.KillTarget();
                }
                break;
        }
    }
}