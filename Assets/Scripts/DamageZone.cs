using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ReturnPointTrigger;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private Interaction[] _interactions;

    [Serializable]
    public class Interaction
    {
        public LayerMask layerMask;
        public Type type;
        public int damage = 1;
        public bool moveToReturnPosition;
    }

    public enum Type
    {
        Damage,
        InstaDeath
    }

    private Dictionary<GameObject, Vector3> _returnPoints = new Dictionary<GameObject, Vector3>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(Interaction interaction in _interactions)
        {
            if(HelperFunctions.PartOfLayerMask(collision.gameObject, interaction.layerMask))
            {
                switch (interaction.type)
                {
                    case Type.Damage:
                        if (collision.TryGetComponent(out IDamageTarget target))
                        {
                            target.DecreaseHealth(interaction.damage);

                            if(interaction.moveToReturnPosition)
                            {
                                if(_returnPoints.ContainsKey(collision.gameObject))
                                {
                                    collision.transform.position = _returnPoints[collision.gameObject];
                                }
                            }
                        }
                        break;
                    case Type.InstaDeath:
                        if (collision.TryGetComponent(out IDamageTarget killTarget))
                        {
                            killTarget.KillTarget();
                        }
                        break;
                }

                break;
            }
        }

    }

    public void ReturnPointTouched(OnReturnTriggerDelegation delegation)
    {
        GameObject callerObject = delegation.other.gameObject;
        Vector3 callerPosition = delegation.returnPosition.position;

        if (!_returnPoints.ContainsKey(callerObject))
        {
            _returnPoints.Add(callerObject, callerPosition);
        }
        else
        {
            _returnPoints[callerObject] = callerPosition;
        }
    }
}