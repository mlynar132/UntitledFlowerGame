using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITest : MonoBehaviour
{
    [SerializeField] private IntEvent _playerHealthEvent;
    [SerializeField] private TMP_Text _playerHealthText;

    private void Start() => UpdatePlayerHealthText(_playerHealthEvent.currentValue);

    private void OnEnable() => _playerHealthEvent.Event.AddListener(UpdatePlayerHealthText);

    private void OnDisable() => _playerHealthEvent.Event.RemoveListener(UpdatePlayerHealthText);

    private void UpdatePlayerHealthText(int amount) => _playerHealthText.text = amount.ToString();
}
