using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformAbilitie : MonoBehaviour {
    private IPlayer2Controller _playerInterface;
    [SerializeField] private GameObject _gameObject;
    private GameObject _player;
    private Player2Controller _player2Controller;
    private void Awake() {
        _playerInterface = GetComponent<IPlayer2Controller>();
        _player = this.gameObject;
        _player2Controller = GetComponent<Player2Controller>();
    }
    private void Start() {
        _playerInterface.BlockAbilityStart += PlaceBlock;
        _playerInterface.BlockAbilityEnd += Cancel;
    }
    public void PlaceBlock() {
        //make restriction for spawning 
        //if (Physics.OverlapBox(worldPos, Vector3.one * 0.45f, Quaternion.identity/*layer masks for condition*/).Length!=0) //can't be 0.5 cuz edges will colide {   
        //  return;
        //}
        _player2Controller.Deactivate(true); //also reset velocity
        _gameObject.SetActive(true);
        _gameObject.transform.GetComponent<MeshRenderer>().material.color = Color.green;

    }
    public void Cancel() {
        _gameObject.SetActive(false);
        _player2Controller.Activate();
    }
}