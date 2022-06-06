using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour {
    public UnityEngine.UI.Slider slider;
    int LevelToLoad;
    public GameObject OptionsMenu;
    public GameObject AboutMenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        LevelToLoad = (SceneManager.GetActiveScene().buildIndex + 1);

        SceneManager.LoadScene(LevelToLoad);

    }


    public void VolumeSlider()
    {
        AudioListener.volume = slider.value;
      
    }

    public void ShowOptions()
    {

        OptionsMenu.SetActive(true);

    }

    public void HideOptions()
    {

        OptionsMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();


    }

    public void ShowAbout()
    {

        AboutMenu.SetActive(true);
    }

    public void HideAbout()
    {

        AboutMenu.SetActive(false);
    }
}
