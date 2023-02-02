using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Default Event")]
public class DefaultEvent : ScriptableEvent<object>
{
    [NonSerialized]
    new public UnityEvent Event;

    protected override void OnEnable() => Event ??= new UnityEvent();

    public void InvokeEvent() => Event?.Invoke();

    public override void ChangeValue(object value) => Debug.LogWarning("Event does not have a value");
}