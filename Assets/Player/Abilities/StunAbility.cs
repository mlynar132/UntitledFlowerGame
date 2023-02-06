using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StunAbility : MonoBehaviour
{
    //final idea
    //spawn an object 
    //stuf that happens in object:
        //play an animation
        //at specific time turn on the hitbox (hit boxes can be specified in the animation so that they actually move)
        //do the logic for hitbox
        //turn of the hitbox
        //end of animation
        //destroy the object

    //prototype
    [SerializeField] GameObject _weapon;
    [SerializeField] GameObject _player;
    private PlayerInputAction _playerInputAction;
    Coroutine _attackCoroutine;

    [SerializeField] private float _attackCooldown;
    private bool _isAttacking = false;
    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
    }
    private void WhipAttack(InputAction.CallbackContext context)
    {
        //spawn an attack and atack will handle all the stuff
        if (!_isAttacking && context.started)
        {
            Attack();
            GameObject inst = Instantiate(_weapon, _player.transform); 
        }
    }

    public void Attack()
    {
        _isAttacking = true;
        _attackCoroutine = StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _isAttacking = false;
    }
  //  private void OnEnable()
   // {
      //  _playerInputAction.Player2.WhipAttack.Enable();
      //  _playerInputAction.Player2.WhipAttack.started += WhipAttack;
    }
   // private void OnDisable()
   // {
   //     _playerInputAction.Player2.WhipAttack.Disable();
       // _playerInputAction.Player2.WhipAttack.started -= WhipAttack;
  //  }
