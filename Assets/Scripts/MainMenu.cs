using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{

    public Slider slider;
    public float sliderValue;
    public Image panelBrightness;


    public AudioMixer audioMixer;
    
    public void StartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        slider.value = PlayerPrefs.GetFloat("brightness", 0.5f);
        panelBrightness.color = new Color(panelBrightness.color.r, panelBrightness.color.g, panelBrightness.color.b, slider.value);
    }


    private void Update()
    {
   

    }

    public void changeSlider(float value)
    {

        sliderValue = value;

        PlayerPrefs.SetFloat("brightness", sliderValue);
        panelBrightness.color = new Color(panelBrightness.color.r, panelBrightness.color.g, panelBrightness.color.b, slider.value);
    }

    public void QuitGame()
    {

       
        Application.Quit();

    }

    public void SetFullScreen( bool isFullScreen)
    {

        Screen.fullScreen = isFullScreen;

    }


    public void SetVolume(float volume)
    {

        audioMixer.SetFloat("volume", volume);

    }




}
