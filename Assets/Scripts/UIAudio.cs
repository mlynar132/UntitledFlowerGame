using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] private EventReference _cancelSound;
    [SerializeField] private EventReference _confirmSound;
    [SerializeField] private EventReference _navigateSound;
    [SerializeField] private EventReference _startGameSound;

    public void PlayCancelSound() => RuntimeManager.PlayOneShot(_cancelSound);
    public void PlayConfirmSound() => RuntimeManager.PlayOneShot(_confirmSound);
    public void PlayNavigateSound() => RuntimeManager.PlayOneShot(_navigateSound);
    public void PlayStartGameSound() => RuntimeManager.PlayOneShot(_startGameSound);
}