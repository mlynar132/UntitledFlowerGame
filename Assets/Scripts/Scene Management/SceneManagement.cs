using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private IntEvent _levelEvent;
    [SerializeField] private DefaultEvent _restartGameEvent;


    [Header("Scenes")]
    [SerializeField] private Levels _levels;
    [SerializeField] private SceneInformation[] _scenes;


    [Serializable]
    public class SceneInformation
    {
        public DefaultEvent eventToLoadScene;
        public SceneType type;
        public SceneAsset sceneAsset;

        public void Subscribe() => eventToLoadScene.Event.AddListener(LoadScene);

        public void Unsubscribe() => eventToLoadScene.Event.RemoveListener(LoadScene);

        public void LoadScene() => SceneManagement.LoadScene(sceneAsset.name);
    }

    public enum SceneType
    {
        LoadFromSceneAsset,
        NextLevel,
        ReloadScene
    }

    private void OnEnable()
    {
        foreach(SceneInformation sceneData in _scenes)
        {
            switch (sceneData.type)
            {
                case SceneType.NextLevel:
                    sceneData.eventToLoadScene.Event.AddListener(LoadNextLevel);
                    break;
                case SceneType.ReloadScene:
                    sceneData.eventToLoadScene.Event.AddListener(ReloadScene);
                    break;
                case SceneType.LoadFromSceneAsset:
                    sceneData.Subscribe();
                    break;
            }
        }

        _restartGameEvent.Event.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        foreach (SceneInformation sceneData in _scenes)
        {
            switch (sceneData.type)
            {
                case SceneType.NextLevel:
                    sceneData.eventToLoadScene.Event.RemoveListener(LoadNextLevel);
                    break;
                case SceneType.ReloadScene:
                    sceneData.eventToLoadScene.Event.RemoveListener(ReloadScene);
                    break;
                case SceneType.LoadFromSceneAsset:
                    sceneData.Unsubscribe();
                    break;
            }
        }

        _restartGameEvent.Event.RemoveListener(RestartGame);
    }

    private void RestartGame()
    {
        _levelEvent.ChangeValue(0);
    }

    private void LoadNextLevel()
    {
        if(_levels.LevelAssets.Length > _levelEvent.currentValue + 1)
        {
            _levelEvent.currentValue++;
            string levelToLoad = _levels.LevelAssets[_levelEvent.currentValue].name;

            if(SceneUtility.GetBuildIndexByScenePath(levelToLoad) >= 0)
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }

    private void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    private static void LoadScene(string name) => SceneManager.LoadScene(name);
}
