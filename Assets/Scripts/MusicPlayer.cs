using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private bool _playMusic = true;
    [SerializeField] private EventReference _levelMusic;


    [Header("Ambience")]
    [SerializeField] private bool _playAmbience = true;
    [SerializeField] private EventReference _ambienceEvent;

    private EventInstance _music;
    private EventInstance _ambience;

    void Start()
    {
        if(_playMusic)
        {
            _music = RuntimeManager.CreateInstance(_levelMusic);
            _music.set3DAttributes(RuntimeUtils.To3DAttributes(Camera.main.transform.position));
            _music.start();
            _music.release();
        }

        if(_playAmbience)
        {
            _ambience = RuntimeManager.CreateInstance(_ambienceEvent);
            _ambience.set3DAttributes(RuntimeUtils.To3DAttributes(Camera.main.transform.position));
            _ambience.start();
            _ambience.release();
        }
    }

    private void OnDestroy()
    {
        _music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _ambience.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
