using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Loads the main menu scene
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Reloads the Game scene
    public void Replay()
    {
        // Reloads the Scene
        SceneManager.LoadScene("Game");
    }
}
