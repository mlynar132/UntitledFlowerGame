using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Player Controllers")]
    [SerializeField] private Player1Controller _player1;
    [SerializeField] private Player2Controller _player2;
    private IPlayer1Controller _player1Controller;
    private IPlayer2Controller _player2Controller;

    [Header("Player Stats")]
    [SerializeField] private ScriptableStats _player1Stats;
    [SerializeField] private ScriptableStats _player2Stats;

    [Header("Events")]
    [SerializeField] private IntEvent _player1HealthEvent;
    [SerializeField] private IntEvent _player2HealthEvent;
    [SerializeField] private FloatEvent _playerDarknessEvent;

    [Header("Components")]
    [SerializeField] private Slider _darknessSlider;
    [SerializeField] private Sprite _emptyFrame;

    [SerializeField] private Image[] _player1HealthImages;
    [SerializeField] private Image[] _player2HealthImages;

    [SerializeField] private AbilityUIStats[] _abilityUIStats;
    [Serializable]
    public class AbilityUIStats
    {
        public Ability ability;
        public Image image;
        public Slider slider;
        public TMP_Text text;
        public Sprite sprite;
        [HideInInspector] public bool onCooldown;
        [HideInInspector] public float coolDown;
        [HideInInspector] public float startCooldown;

        public void StartCooldown(float coolDown)
        {
            startCooldown = coolDown;
            this.coolDown = coolDown;

            if (text != null)
                text.text = ((int)coolDown).ToString();

            if (slider != null)
                slider.value = Mathf.InverseLerp(startCooldown, 0, coolDown);
        }

        public void CooldownEvent()
        {
            if (text != null)
                text.text = ((int)coolDown).ToString();

            if (slider != null)
                slider.value = Mathf.InverseLerp(startCooldown, 0, coolDown);

            if(coolDown <= 0)
            {
                ResetValues();
            }

            coolDown -= Time.deltaTime;
        }

        public void ResetValues()
        {
            onCooldown = false;
            coolDown = 0;

            if (text != null)
                text.enabled = false;

            if (image != null)
                image.sprite = sprite;

            if (slider != null)
                slider.value = 0;
        }
    }

    private Dictionary<Ability, AbilityUIStats> _abilityDictionary = new Dictionary<Ability, AbilityUIStats>();

    private void Awake()
    {
        if (_player1 != null)
            _player1Controller = _player1.GetComponent<IPlayer1Controller>();

        if (_player2 != null)
            _player2Controller = _player2.GetComponent<IPlayer2Controller>();
    }

    private void OnEnable()
    {
        if (_player1HealthEvent != null)
            _player1HealthEvent.Event.AddListener(UpdatePlayer1Health);

        if (_player2HealthEvent != null)
            _player2HealthEvent.Event.AddListener(UpdatePlayer2Health);

        if (_playerDarknessEvent != null)
            _playerDarknessEvent.Event.AddListener(UpdateDarknessMeter);

        foreach(AbilityUIStats image in _abilityUIStats)
        {
            if(!_abilityDictionary.ContainsKey(image.ability))
            {
                _abilityDictionary?.Add(image.ability, image);
            }

            image?.ResetValues();
        }

        if(_player1Controller != null)
        {
            _player1Controller.BombUsed += BombMushroomUsed;
            _player1Controller.DashingChanged += DashingChanged;
            _player1Controller.AnchorUsed += AnchorUsed;
        }

        if(_player2Controller != null)
        {
            _player2Controller.StunAbility += StunninngVinesUsed;
            _player2Controller.BlockAbilityEnd += PlatformBuildUsed;
            _player2Controller.VineAbilityChanged += GrapplingVinesUsed;
        }
    }

    private void OnDisable()
    {
        if (_player1HealthEvent != null)
            _player1HealthEvent.Event.RemoveListener(UpdatePlayer1Health);

        if (_player2HealthEvent != null)
            _player2HealthEvent.Event.RemoveListener(UpdatePlayer2Health);

        if (_playerDarknessEvent != null)
            _playerDarknessEvent.Event.RemoveListener(UpdateDarknessMeter);

        if(_player1Controller != null)
        {
            _player1Controller.BombUsed -= BombMushroomUsed;
            _player1Controller.DashingChanged -= DashingChanged;
            _player1Controller.AnchorUsed -= AnchorUsed;
        }

        if (_player2Controller != null)
        {
            _player2Controller.StunAbility -= StunninngVinesUsed;
            _player2Controller.BlockAbilityEnd -= PlatformBuildUsed;
            _player2Controller.VineAbilityChanged -= GrapplingVinesUsed;
        }
    }

    private void Start()
    {
        if (_playerDarknessEvent != null)
            UpdateDarknessMeter(_playerDarknessEvent.currentValue);
        if (_player1HealthEvent != null)
            UpdatePlayer1Health(_player1HealthEvent.currentValue);
        if (_player2HealthEvent != null)
            UpdatePlayer2Health(_player2HealthEvent.currentValue);
    }

    private void UpdatePlayer1Health(int amount) => UpdateHealth(_player1HealthImages, amount);

    private void UpdatePlayer2Health(int amount) => UpdateHealth(_player2HealthImages, amount);

    private void UpdateHealth(Image[] images, int health)
    {
        images[0].enabled = health > 0;
        images[1].enabled = health > 1;
        images[2].enabled = health > 2;

        foreach(Image image in images)
        {
            if (image != null)
                image.color = health > 0 ? Color.red : image.color;
            if (image != null)
                image.color = health > 1 ? Color.yellow : image.color;
            if (image != null)
                image.color = health > 2 ? Color.green : image.color;
        }
    }


    #region Event Functions
    private void UpdateDarknessMeter(float amount) => _darknessSlider.value = amount;
    private void DashingChanged(bool valueChanged, Vector2 vector2) => AbilityUsed(Ability.Dash, _player1Stats.DashCooldown);
    private void BombMushroomUsed(float f) => AbilityUsed(Ability.BombMushroom, _player1Stats.BombCooldown);
    private void AnchorUsed(Vector2 vector2) => AbilityUsed(Ability.Anchor, _player1Stats.AnchorCooldown);
    private void GrapplingVinesUsed(bool valueChanged, Vector2 vector2)
    {
        if(!valueChanged)
        {
            AbilityUsed(Ability.GrapplingVine, _player2Stats.VineAbilityCooldown);
        }
    }

    private void PlatformBuildUsed() => AbilityUsed(Ability.PlatformBuild, _player2Stats.BlockAbilityCooldown);
    private void StunninngVinesUsed() => AbilityUsed(Ability.StunningVines, _player2Stats.StunAbilityCooldown);
    #endregion

    private void AbilityUsed(Ability ability, float coolDown)
    {
        AbilityUIStats currentAbility = _abilityDictionary[ability];

        currentAbility?.StartCooldown(coolDown);

        if (currentAbility != null)
            currentAbility.image.sprite = _emptyFrame;
        if (currentAbility != null)
            currentAbility.text.enabled = true;
        if (currentAbility != null)
            currentAbility.onCooldown = true;
    }

    private void Update()
    {
        foreach(AbilityUIStats stat in _abilityUIStats)
        {
            if(stat.onCooldown)
            {
                stat?.CooldownEvent();
            }
        }
    }
}