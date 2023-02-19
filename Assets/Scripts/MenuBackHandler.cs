using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class MenuBackHandler : MonoBehaviour, ICancelHandler
{

    [SerializeField] private Button _backButton;
    
    public void OnCancel( BaseEventData eventData )
    {
        _backButton.onClick.Invoke();
    }
}