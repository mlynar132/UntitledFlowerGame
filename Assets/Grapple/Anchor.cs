using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Anchor : MonoBehaviour {
    private IPlayer1Controller _playerInterface;

    [SerializeField, Range(1f, 100f)] private float _range;
    [SerializeField, Range(0f, 10f)] private float _cooldown;
    [SerializeField, Range(1, 10)] private int _maxAnchors;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _anchorPrefab;

    private int _nextAnchor;
    //private Vector2 _aim;
    //private float _delay;

    private GameObject[] _anchors;
    private void Awake() {
        _playerInterface = GetComponent<IPlayer1Controller>();
    }
    private void Start() {
        _playerInterface.AnchorUsed += PlaceAnchor;

        _anchors = new GameObject[_maxAnchors];
    }

    /*private void Update( )
    {
        _delay -= Time.deltaTime;
    }*/

    private void PlaceAnchor(Vector2 dir) {
        var hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0)/*so it shoots from center*/, dir, _range, _layerMask);

        if (hit) {
            //_delay = _cooldown;

            if (_anchors[_nextAnchor] == null) {
                _anchors[_nextAnchor] = Instantiate(_anchorPrefab);
            }

            _anchors[_nextAnchor].transform.position = hit.point;
            _anchors[_nextAnchor].transform.rotation = Quaternion.FromToRotation(Vector2.up, hit.normal);

            //color
            _anchors[_nextAnchor].GetComponent<MeshRenderer>().material.color = Color.yellow;

            _nextAnchor++;

            if (_nextAnchor >= _anchors.Length) // Make it loop around
            {
                _nextAnchor = 0;
            }
        }
    }


    // Controlls
    /*public void Aim( InputAction.CallbackContext context )
    {
        _aim = context.ReadValue<Vector2>();
    }

    public void Place( InputAction.CallbackContext context )
    {
        if ( context.ReadValueAsButton() && context.started && _delay <= 0 )
        {
            PlaceAnchor(_aim);
        }
    }*/
}