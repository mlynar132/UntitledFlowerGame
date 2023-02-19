using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public static class RespawnManager
{
    public static Vector2[] RespawnPoints { get; private set; } = new Vector2[2];

    public static Vector2 RespawnPoint => RespawnPoints[NextPoint];

    public static string SceneName { get; private set; }
    public static bool IsEmpty { get; private set; } = true;

    public static int NextPoint
    {
        get
        {
            if ( ++nextPoint > 1 )
            {
                nextPoint = 0;
            }

            return nextPoint;
        }
    }

    private static int nextPoint;


    public static void ActivatePoint( Transform[] respawnPoints, string sceneName )
    {
        RespawnPoints[0] = respawnPoints[0].position;
        RespawnPoints[1] = respawnPoints[1].position;
        SceneName = sceneName;
        IsEmpty = false;
    }

    public static void ResetSpawn( )
    {
        RespawnPoints = new Vector2[2];
        SceneName = string.Empty;
        IsEmpty = true;
    }

    public static void Respawn( )
    {
        SceneManager.LoadScene( SceneName );
    }
}