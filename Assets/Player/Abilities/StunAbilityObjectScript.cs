using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAbilityObjectScript : MonoBehaviour
{
    [SerializeField] private float _attackDuration;
    [SerializeField] private GameObject _attack;
    private float _decay;
    private void OnEnable() {
        _decay = _attackDuration;  
    }
    private void FixedUpdate()
    {
        _decay -= Time.deltaTime;
        if (_decay<=0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IStunable targetInterface = collision.transform.GetComponent<IStunable>();
        if (targetInterface != null)
        {
            Debug.Log("Stunned: " + collision.name);
            targetInterface.Stun();
        }
    }
}
