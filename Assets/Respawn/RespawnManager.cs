using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Manager { get; private set; }
    public static Vector2 RespawnPoint { get; private set; }
    public static string SceneName { get; private set; }

    private void Start( )
    {
        if ( !Manager )
        {
            Manager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public static void ActivatePoint( Vector2 respawnPoint, string sceneName )
    {
        RespawnPoint = respawnPoint;
        SceneName = sceneName;
    }

    public static void ResetSpawn( )
    {
        RespawnPoint = Vector2.zero;
        SceneName = string.Empty;
    }

    public static void Respawn( )
    {
        SceneManager.LoadScene( SceneName );
    }
    
    
}
