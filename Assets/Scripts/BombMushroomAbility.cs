using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombMushroomAbility : MonoBehaviour
{
    private IPlayer1Controller _playerInterface;
    [SerializeField] private FloatEvent _cooldownEvent;

    [SerializeField] private Transform _throwLocation;
    [SerializeField] private ExplodingObject _explodingObject;
    [SerializeField] private Transform _aim;

    [SerializeField] [Range( 0, 15 )] private float _force = 6;

    [SerializeField] private Vector2 _angle;

    [SerializeField] private float _rotationSpeed = 500;

    private float _rotateDir;

    private bool _cooldown;

    private void Awake( )
    {
        _playerInterface = GetComponent<IPlayer1Controller>();
    }

    private void Start( )
    {
        _playerInterface.BombUsed += ThrowMushroom;

        _cooldownEvent.ResetValue();
    }

    private void ThrowMushroom( float dir )
    {
        var angle = _angle.normalized;
        angle.x *= dir;

        ExplodingObject explodingObject = Instantiate( _explodingObject );
        explodingObject.transform.position = ( Vector3 )angle + _throwLocation.position;

        var force = angle * _force;
        force.x += _playerInterface.Velocity.x;

        explodingObject.AddForce( force );
    }
}