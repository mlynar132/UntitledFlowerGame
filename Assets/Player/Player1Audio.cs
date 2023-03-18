using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IPlayer1Controller))]
public class Player1Audio : MonoBehaviour
{
    [SerializeField] private EventReference _dashSound;
    [SerializeField] private EventReference _jumpSound;

    private IPlayer1Controller _controller;
    private void Start()
    {
        _controller = GetComponent<IPlayer1Controller>();
        _controller.DashingChanged += DashUsed;
        _controller.Jumped += Jumped;
    }

    private void DashUsed(bool change, Vector2 v)
    {
        if(change == false)
            RuntimeManager.PlayOneShot(_dashSound);
    }

    private void Jumped(bool b) => RuntimeManager.PlayOneShot(_jumpSound);
}
