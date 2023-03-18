using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu()]
public class VolumeControls : ScriptableObject
{
    [SerializeField][Range(0, 1)] private float _masterVolume;
    [SerializeField][Range(0, 1)] private float _musicVolume;
    [SerializeField][Range(0, 1)] private float _sfxVolume;
    [SerializeField][Range(0, 1)] private float _ambienceVolume;

    public float MasterVolume => _masterVolume;
    public float MusicVolume => _musicVolume;
    public float SFXVolume => _sfxVolume;
    public float AmbienceVolume => _ambienceVolume;

    public void SetMasterVolume(float volume) => _masterVolume = volume;
    public void SetMusicVolume(float volume) => _musicVolume = volume;
    public void SetSFXVolume(float volume) => _sfxVolume = volume;
    public void SetAmbienceVolume(float volume) => _ambienceVolume = volume;
}