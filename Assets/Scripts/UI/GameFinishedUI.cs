using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishedUI : MonoBehaviour
{
    [SerializeField] private DefaultEvent _restartGameEvent;

    public void MainMenu() => SceneManager.LoadScene(0);

    public void Restart() => _restartGameEvent.InvokeEvent();

    public void QuitGame() => Application.Quit();
}