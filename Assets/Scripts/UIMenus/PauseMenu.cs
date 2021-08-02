using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject winMenuUI;

    // Update is called once per frame
    void Update()
    {
        // If they have pressed Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if the game is paused
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        // Checks if the enemies that you have killed is equal to the win condition
        if(GameManager.Instance.enemiesKilled == GameManager.Instance.killsToWin)
        {
            // sets the win UI to active
            winMenuUI.SetActive(true);
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

    }

    public void Quit()
    {

    }

    public void Replay()
    {
        // Reloads the Scene
        SceneManager.LoadScene(0);
    }
}
