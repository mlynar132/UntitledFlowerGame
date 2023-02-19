using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _firstSelected;
    [SerializeField] private List<GameObject> _disableOnPause;

    public static bool GameIsPaused = false;

    public void PausePressed( InputAction.CallbackContext context )
    {
        if ( context.started && context.ReadValueAsButton() )
        {
            if ( GameIsPaused )
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume( )
    {
        gameObject.SetActive( false );
        Time.timeScale = 1f;
        GameIsPaused = false;

        foreach ( var ui in _disableOnPause )
        {
            ui.SetActive( true );
        }
    }

    private void Pause( )
    {
        _firstSelected.Select();
        gameObject.SetActive( true );
        Time.timeScale = 0f;
        GameIsPaused = true;

        foreach ( var ui in _disableOnPause )
        {
            ui.SetActive( false );
        }
    }

    public void RestartLevel( )
    {
        Resume();
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

    public void LoadMenu( )
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene( 0 );
    }

    public void SelectLevel( )
    {
    }
}