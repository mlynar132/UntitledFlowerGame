using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAbilityObjectScript : MonoBehaviour {
    [SerializeField] private float _attackDuration;
    [SerializeField] private GameObject _attack;
    [SerializeField] private Animator _anim;

    private void Start() {
        _anim = GetComponent<Animator>();
    }
    private void OnEnable() {
        //it shoudl play animation automaticly
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
    public void EndAttack() {
        gameObject.SetActive(false);
    }
}