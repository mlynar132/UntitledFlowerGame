using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuSanya : MonoBehaviour
{
    
    public static bool isPaused = false;

    public GameObject pauseMenuController;






    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }


    }

    public void Resume()
    {
        pauseMenuController.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;

    }


    public void Pause()
    {
        pauseMenuController.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;

    }

    public void LoadMenu()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();

    }
}
