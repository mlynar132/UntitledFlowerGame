using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private VolumeControls _volumeControls;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _ambienceSlider;

    private VCA _masterVcaController;
    private VCA _musicVcaController;
    private VCA _sfxVcaController;
    private VCA _ambienceVcaController;


    private void Start()
    {
        _masterVcaController = RuntimeManager.GetVCA("vca:/Master");
        _musicVcaController = RuntimeManager.GetVCA("vca:/Music");
        _sfxVcaController = RuntimeManager.GetVCA("vca:/SFX");
        _sfxVcaController = RuntimeManager.GetVCA("vca:/Ambience");

        SetSliderValue(_masterSlider, _volumeControls.MasterVolume);
        SetSliderValue(_musicSlider, _volumeControls.MusicVolume);
        SetSliderValue(_sfxSlider, _volumeControls.SFXVolume);
        SetSliderValue(_ambienceSlider, _volumeControls.AmbienceVolume);

        SetMasterVolume(_volumeControls.MasterVolume);
        SetMusicVolume(_volumeControls.MusicVolume);
        SetSfxVolume(_volumeControls.SFXVolume);
        SetAmbienceVolume(_volumeControls.AmbienceVolume);
    }

    private void SetSliderValue(Slider slider, float value)
    {
        if(slider != null)
            slider.value = value;
    }

    public void SetMasterVolume(float volume)
    {
        _masterVcaController.setVolume(volume);
        _volumeControls.SetMasterVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        _musicVcaController.setVolume(volume);
        _volumeControls.SetMusicVolume(volume);
    }

    public void SetSfxVolume(float volume)
    {
        _sfxVcaController.setVolume(volume);
        _volumeControls.SetSFXVolume(volume);
    }
    public void SetAmbienceVolume(float volume)
    {
        _ambienceVcaController.setVolume(volume);
        _volumeControls.SetAmbienceVolume(volume);
    }
}
