using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScroll : MonoBehaviour
{
    [SerializeField] private Vector2 _scrollDirection = new Vector2(1, 0);
    [SerializeField] private float _scrollSpeed = 1f;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        transform.position += new Vector3(_scrollSpeed * _scrollDirection.x * Time.deltaTime, _scrollSpeed * _scrollDirection.y * Time.deltaTime, 0);
    }
}
