using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject winMenuUI;

    public GameObject settingsMenu;

    public bool settingsOpen;

    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        // If they have pressed Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if the game is paused
            if (GameIsPaused && !settingsOpen)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if(settingsMenu.activeSelf == false)
        {
            settingsOpen = false;
        }
    }

    public void Resume()
    {
        // Reverses the Pause
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        // Sets the Menu to active
        pauseMenuUI.SetActive(true);

        // Sets the time scale to 0
        Time.timeScale = 0f;

        // Makes the bool flip
        GameIsPaused = true;
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        settingsOpen = true;
    }

    public void Menu()
    {   
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }
}
