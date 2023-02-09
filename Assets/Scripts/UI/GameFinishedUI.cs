using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishedUI : MonoBehaviour
{
    [SerializeField] private DefaultEvent _restartGameEvent;

    public void MainMenu() => Debug.Log("Go to main menu");

    public void Restart() => _restartGameEvent.InvokeEvent();

    public void QuitGame() => Application.Quit();
}