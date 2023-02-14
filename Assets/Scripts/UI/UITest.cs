using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITest : MonoBehaviour
{
    [SerializeField] private IntEvent _playerHealthEvent;
    [SerializeField] private TMP_Text _playerHealthText;
    [SerializeField] private FloatEvent _playerDarknessEvent;
    [SerializeField] private Slider _darknessSlider;

    private void Start()
    {
        UpdatePlayerHealthText(_playerHealthEvent.currentValue);
        UpdateDarknessMeter(_playerDarknessEvent.currentValue);
    }

    //private void OnEnable()
    //{
    //    _playerHealthEvent.Event.AddListener(UpdatePlayerHealthText);
    //    _playerDarknessEvent.Event.AddListener(UpdateDarknessMeter);
    //}

    private void OnDisable()
    {
        _playerHealthEvent.Event.RemoveListener(UpdatePlayerHealthText);
        _playerDarknessEvent.Event.RemoveListener(UpdateDarknessMeter);
    }

    private void UpdatePlayerHealthText(int amount) => _playerHealthText.text = amount.ToString();

    private void UpdateDarknessMeter(float amount)
    {
        _darknessSlider.value = amount;
    }
}
