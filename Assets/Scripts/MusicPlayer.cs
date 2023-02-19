using FMOD;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Header("Ambience")]
    [SerializeField] private EventReference _ambienceEvent;

    private EventInstance _ambience;

    void Start()
    {
        _ambience = RuntimeManager.CreateInstance(_ambienceEvent);
        _ambience.set3DAttributes(RuntimeUtils.To3DAttributes(Camera.main.transform.position));
        _ambience.start();
        _ambience.release();
    }

}
