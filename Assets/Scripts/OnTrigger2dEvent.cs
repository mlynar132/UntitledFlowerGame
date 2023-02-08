using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class OnTrigger2dEvent : MonoBehaviour
{
    private Collider2D _caller;

    private void Awake() => _caller = GetComponent<Collider2D>();

    [SerializeField] private UnityEvent<OnTriggerDelegation> _enter;
    [SerializeField] private UnityEvent<OnTriggerDelegation> _exit;

    private void OnTriggerEnter2D(Collider2D other) => _enter.Invoke(new OnTriggerDelegation(_caller, other));

    private void OnTriggerExit2D(Collider2D other) => _exit.Invoke(new OnTriggerDelegation(_caller, other));

    public struct OnTriggerDelegation
    {
        public Collider2D caller { get; private set; }
        public Collider2D other { get; private set; }

        public OnTriggerDelegation(Collider2D caller, Collider2D other)
        {
            this.caller = caller;
            this.other = other;
        }
    }
}