using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScriptableEvent<T> : ScriptableObject
{
    [SerializeField] protected T _startValue;
    public T currentValue;

    public T startValue => _startValue;

    [NonSerialized] public UnityEvent<T> Event;

    protected virtual void OnEnable()
    {
        currentValue = _startValue;
        Event ??= new UnityEvent<T>();
    }

    public virtual void ChangeValue(T value)
    {
        currentValue = value;
        Event?.Invoke(currentValue);
    }
    public void ResetValue() => currentValue = _startValue;
}