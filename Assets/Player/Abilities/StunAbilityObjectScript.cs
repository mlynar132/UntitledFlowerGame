using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAbilityObjectScript : MonoBehaviour {
    [SerializeField] private float _attackDuration;
    [SerializeField] private GameObject _attack;
    private float _decay;
    private void OnEnable() {
        _decay = _attackDuration;
    }
    private void FixedUpdate() {
        _decay -= Time.deltaTime;
        if (_decay <= 0) {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out IStunable enemyStunable)) {
            if (enemyStunable.IsStuned() && enemyStunable.P1hit) {
                if (collision.TryGetComponent(out IDamageTarget enemyKillabe)) {
                    enemyKillabe.DecreaseHealth(1);
                    Debug.Log("dsa");
                }
            }
            else {
                enemyStunable.Stun();
                enemyStunable.P2hit = true;
            }
        }
    }
}