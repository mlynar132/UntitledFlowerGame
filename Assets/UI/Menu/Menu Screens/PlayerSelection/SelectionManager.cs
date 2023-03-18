using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectionManager : MonoBehaviour
{
    [Serializable]
    private class SelectionEvent : UnityEvent
    {
    }

    [SerializeField] private SelectionEvent _onStart;
    [SerializeField] private SelectionEvent _onReady;
    [SerializeField] private SelectionEvent _onUnready;

    public int Readys
    {
        get => _readys;
        set
        {
            _readys = value;

            if ( _readys >= 2 )
            {
                ReadyGame();
            }
        }
    }

    private int _readys;

    public void StartGame( )
    {
        if ( _readys >= 2 )
        {
            _readys = 0;
            _onStart.Invoke();
        }
    }

    private void ReadyGame( )
    {
        _onReady.Invoke();
    }

    public void UnreadyGame( )
    {
        _onUnready.Invoke();
    }
}