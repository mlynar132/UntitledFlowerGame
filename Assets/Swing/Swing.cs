using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Swing : MonoBehaviour {
    private IPlayer2Controller _playerInterface;

    [SerializeField] private GameObject _ropePrefab;
    [SerializeField, Range(1, 100)] private float _range;

    private Vector2 _currentAim;
    private Rope _rope;
    private GameObject _ropeObject;
    private void Awake() {
        _playerInterface = GetComponent<IPlayer2Controller>();
    }
    private void Start() {
        _playerInterface.VineAbilityChanged += Throw;

        if (_ropeObject == null) {
            _ropeObject = Instantiate(_ropePrefab);
        }
        _rope = _ropeObject.GetComponent<Rope>();

        _rope.BindRB(gameObject.GetComponent<Rigidbody2D>());

        _ropeObject.SetActive(false);
    }

    public void Aim(InputAction.CallbackContext context) {
        _currentAim = context.ReadValue<Vector2>();
    }
    public void Throw(bool state, Vector2 dir) {
        if (state) {
            var position = transform.position + new Vector3(0, 0.5f, 0);
            Debug.Log(position);
            if (_rope.Grapple(position, dir, _range, true) != Vector2.zero) {
                _ropeObject.SetActive(true);
            }
        }
        else {
            _ropeObject.SetActive(false);
        }
    }
}