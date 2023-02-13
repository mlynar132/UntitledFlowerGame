using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndScreenUI : MonoBehaviour
{
    [SerializeField] private DefaultEvent _levelEndEvent;
    [SerializeField] private DefaultEvent _openLevelEndScreenCanvas;

    [SerializeField] private Canvas _levelEndCanvas;

    private void OnEnable() => _openLevelEndScreenCanvas.Event.AddListener(OpenWinScreenCanvas);

    private void OnDisable() => _openLevelEndScreenCanvas.Event.RemoveListener(OpenWinScreenCanvas);

    private void Start() => _levelEndCanvas.enabled = false;

    private void OpenWinScreenCanvas() => _levelEndCanvas.enabled = true;

    public void GotoNextLevel() => _levelEndEvent.InvokeEvent();

    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void MainMenu() => Debug.Log("Go to main menu");
}