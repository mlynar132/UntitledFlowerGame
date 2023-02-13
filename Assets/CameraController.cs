using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform _p1;
    [SerializeField] private Transform _p2;
    [SerializeField] private float depth;
    private Vector3 _midway;
    private void Update() {
        _midway = new Vector3((_p1.position.x + _p2.position.x) / 2, (_p1.position.y + _p2.position.y) / 2, -depth);
        transform.position = _midway;
    }
}