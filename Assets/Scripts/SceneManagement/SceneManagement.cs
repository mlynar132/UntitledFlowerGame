using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [Header( "Events" )] [SerializeField] private IntEvent _levelEvent;
    [SerializeField] private DefaultEvent _restartGameEvent;


    [Header( "Scenes" )] [SerializeField] private Levels _levels;
    [SerializeField] private SceneInformation[] _scenes;


    [Serializable]
    public class SceneInformation
    {
        public DefaultEvent eventToLoadScene;
        public SceneType type;
        public int sceneIndex;

        public void Subscribe( ) => eventToLoadScene.Event.AddListener( LoadScene );

        public void Unsubscribe( ) => eventToLoadScene.Event.RemoveListener( LoadScene );

        public void LoadScene()
        {
            RespawnManager.ResetSpawn();
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public enum SceneType
    {
        LoadFromBuildIndex,
        NextLevel,
        ReloadScene
    }

    private void OnEnable( )
    {
        foreach ( SceneInformation sceneData in _scenes )
        {
            switch ( sceneData.type )
            {
                case SceneType.NextLevel:
                    sceneData.eventToLoadScene.Event.AddListener( LoadNextLevel );
                    break;
                case SceneType.ReloadScene:
                    sceneData.eventToLoadScene.Event.AddListener( ReloadScene );
                    break;
                case SceneType.LoadFromBuildIndex:
                    sceneData.Subscribe();
                    break;
            }
        }

        _restartGameEvent.Event.AddListener( RestartGame );
    }

    private void OnDisable( )
    {
        foreach ( SceneInformation sceneData in _scenes )
        {
            switch ( sceneData.type )
            {
                case SceneType.NextLevel:
                    sceneData.eventToLoadScene.Event.RemoveListener( LoadNextLevel );
                    break;
                case SceneType.ReloadScene:
                    sceneData.eventToLoadScene.Event.RemoveListener( ReloadScene );
                    break;
                case SceneType.LoadFromBuildIndex:
                    sceneData.Unsubscribe();
                    break;
            }
        }

        _restartGameEvent.Event.RemoveListener( RestartGame );
    }

    private void RestartGame( )
    {
        RespawnManager.ResetSpawn();
        _levelEvent.ChangeValue( 0 );
    }

    private void LoadNextLevel( )
    {
        if ( _levels.LevelIndexes.Length > _levelEvent.currentValue + 1 )
        {
            _levelEvent.currentValue++;
            int levelToLoad = _levels.LevelIndexes[_levelEvent.currentValue];

            if ( levelToLoad >= 0 )
            {
                RespawnManager.ResetSpawn();
                SceneManager.LoadScene( levelToLoad );
            }
        }
    }

    private void ReloadScene( )
    {
        RespawnManager.ResetSpawn();
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }
}