using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessMovement : MonoBehaviour
{
    [SerializeField] private MovementDirection _direction = MovementDirection.Right;
    [SerializeField] [Range(0, 10f)] private float _movementSpeed = 1f;

    private Vector2 _moveDirection = new Vector2(0, 0);

    private enum MovementDirection
    {
        None,
        Right,
        Left,
        Up,
        Down,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _moveDirection = _direction switch
        {
            MovementDirection.Right => new Vector2(1, 0),
            MovementDirection.Left => new Vector2(-1, 0),
            MovementDirection.Up => new Vector2(0, 1),
            MovementDirection.Down => new Vector2(0, -1),
            MovementDirection.UpRight => new Vector2(0.7f, 0.7f),
            MovementDirection.UpLeft => new Vector2(-0.7f, 0.7f),
            MovementDirection.DownRight => new Vector2(0.7f, -0.7f),
            MovementDirection.DownLeft => new Vector2(-0.7f, -0.7f),
            _ => Vector2.zero,
        };

        transform.position += new Vector3(_movementSpeed * _moveDirection.x * Time.deltaTime, _movementSpeed * _moveDirection.y * Time.deltaTime, 0);
    }
}
