using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IPlayer2Controller))]
public class Player2Audio : MonoBehaviour
{
    [SerializeField] private EventReference _grappleSound;
    [SerializeField] private EventReference _vineSound;
    [SerializeField] private EventReference _platformSound;
    [SerializeField] private EventReference _jumpSound;


    private IPlayer2Controller _controller;
    private void Start()
    {
        _controller = GetComponent<IPlayer2Controller>();
        _controller.VineAbilityChanged += GrappleUsed;
        _controller.StunAbility += VineUsed;
        _controller.BlockAbilityStart += PlatformUsed;
        _controller.Jumped += Jumped;
    }

    private void GrappleUsed(bool valueChanged, Vector2 vector2)
    {
        if(!valueChanged)
            RuntimeManager.PlayOneShot(_grappleSound);
    }

    private void VineUsed() => RuntimeManager.PlayOneShot(_vineSound);
    private void PlatformUsed() => RuntimeManager.PlayOneShot(_platformSound);
    private void Jumped(bool b) => RuntimeManager.PlayOneShot(_jumpSound);
}
