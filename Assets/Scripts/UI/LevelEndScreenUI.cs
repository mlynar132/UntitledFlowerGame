using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndScreenUI : MonoBehaviour
{
    [SerializeField] private DefaultEvent _levelEndEvent;
    [SerializeField] private DefaultEvent _openLevelEndScreenCanvas;
    [SerializeField] private DefaultEvent _goToMainMenu;

    [SerializeField] private Canvas _levelEndCanvas;

    private void OnEnable() => _openLevelEndScreenCanvas.Event.AddListener(OpenWinScreenCanvas);

    private void OnDisable() => _openLevelEndScreenCanvas.Event.RemoveListener(OpenWinScreenCanvas);

    private void Start() => _levelEndCanvas.gameObject.SetActive(false);

    private void OpenWinScreenCanvas() => _levelEndCanvas.gameObject.SetActive(true);

    public void GotoNextLevel() => _levelEndEvent.InvokeEvent();

    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void MainMenu() => _goToMainMenu.InvokeEvent();
}