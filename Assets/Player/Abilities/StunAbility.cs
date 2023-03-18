using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StunAbility : MonoBehaviour {
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
    private IPlayer2Controller _playerInterface;
    [SerializeField] private GameObject _weapon;
    private void Awake() {
        _playerInterface = GetComponent<IPlayer2Controller>();
    }
    private void Start() {
        _playerInterface.StunAbility += WhipAttack;
    }
    public void WhipAttack() {
        //spawn an attack and atack will handle all the stuff
        _weapon.SetActive(true);
    }
}
