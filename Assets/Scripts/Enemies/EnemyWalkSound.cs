using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkSound : MonoBehaviour
{
    [SerializeField] private EventReference _walkSound;

    public void PlayWalkSound()
    {
        Debug.Log("Walk Sound");
        RuntimeManager.PlayOneShotAttached(_walkSound, gameObject);
    }
}
