using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _offset = new Vector2(0, -5f);
    [SerializeField] private float _lerpSpeed = 3;

    private bool _inDarkness;
    private bool _lerpMove;
    private float _t = 0;

    private void Start()
    {
        Vector2 leftCameraEdge = _camera.ScreenToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
        transform.position = new Vector3(leftCameraEdge.x + _offset.x, leftCameraEdge.y + _offset.y, transform.position.z);
        transform.parent = _camera.transform;
    }

    private void SetPosition(bool right)
    {
        Vector2 leftCameraEdge = _camera.ScreenToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
        Vector2 rightCameraEdge = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, 0, _camera.nearClipPlane));

        Vector3 leftEdge = new Vector3(leftCameraEdge.x + _offset.x, leftCameraEdge.y + _offset.y, transform.position.z);
        Vector3 rightEdge = new Vector3(rightCameraEdge.x + _offset.x, rightCameraEdge.y + _offset.y, transform.position.z);

        _t += _lerpSpeed * Time.deltaTime;

        Vector3 targetPosition  = right ? rightEdge : leftEdge;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _t);

        if(transform.position == targetPosition)
        {
            _lerpMove = false;
        }
    }

    private void Update()
    {
        if(_lerpMove)
        {
            SetPosition(_inDarkness);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_lerpMove && other.CompareTag("Player"))
        {
            if(!_inDarkness)
            {
                Debug.Log("Player entered darkness");
            }
            _lerpMove = true;
            _inDarkness = !_inDarkness;
            _t = 0;
        }
    }
}