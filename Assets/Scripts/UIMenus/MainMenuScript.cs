using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
