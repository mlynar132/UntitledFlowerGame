using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [SerializeField] [Range(0, 1)] private float _masterVolume = 1;
    [SerializeField] [Range(0, 1)] private float _musicVolume = 1;
    [SerializeField] [Range(0, 1)] private float _sfxVolume = 1;

    private VCA _masterVcaController;
    private VCA _musicVcaController;
    private VCA _sfxVcaController;

    private void Start()
    {
        _masterVcaController = RuntimeManager.GetVCA("vca:/Master");
        _musicVcaController = RuntimeManager.GetVCA("vca:/Music");
        _sfxVcaController = RuntimeManager.GetVCA("vca:/SFX");

        SetSliderValue(_masterSlider, _masterVolume);
        SetSliderValue(_musicSlider, _musicVolume);
        SetSliderValue(_sfxSlider, _sfxVolume);

        SetMasterVolume(_masterVolume);
        SetMusicVolume(_musicVolume);
        SetSfxVolume(_sfxVolume);
    }

    private void SetSliderValue(Slider slider, float value)
    {
        if(slider != null)
        {
            slider.value = value;
        }
    }

    private void Update()
    {
        SetMasterVolume(_masterVolume);
        SetMusicVolume(_musicVolume);
        SetSfxVolume(_sfxVolume); 
    }

    public void SetMasterVolume(float volume) => _masterVcaController.setVolume(volume);
    public void SetMusicVolume(float volume) => _musicVcaController.setVolume(volume);
    public void SetSfxVolume(float volume) => _sfxVcaController.setVolume(volume);
}
