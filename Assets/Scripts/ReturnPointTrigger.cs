using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ReturnPointTrigger : MonoBehaviour
{
    [SerializeField] private Transform _returnPosition;

    private Collider2D _caller;

    private void Awake() => _caller = GetComponent<Collider2D>();

    [SerializeField] private UnityEvent<OnReturnTriggerDelegation> _enter;
    [SerializeField] private UnityEvent<OnReturnTriggerDelegation> _exit;

    private void OnTriggerEnter2D(Collider2D other) => _enter.Invoke(new OnReturnTriggerDelegation(_caller, other, _returnPosition));

    private void OnTriggerExit2D(Collider2D other) => _exit.Invoke(new OnReturnTriggerDelegation(_caller, other, _returnPosition));

    public struct OnReturnTriggerDelegation
    {
        public Collider2D caller { get; private set; }

        public Collider2D other { get; private set; }

        public Transform returnPosition { get; private set; }

        public OnReturnTriggerDelegation(Collider2D caller, Collider2D other, Transform returnPosition)
        {
            this.caller = caller;
            this.other = other;
            this.returnPosition = returnPosition;
        }
    }
}