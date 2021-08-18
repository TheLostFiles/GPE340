using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    public GameObject settingsMenu;

    public Dropdown resolutionDropdown;

    public AudioMixer masterMixer;

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Makes the master slider move the with slider on screen
    public void MasterVolSlider(float volume)
    {
        masterMixer.SetFloat("MasterVolume", volume);
    }

    //Makes the Music slider move the with slider on screen
    public void MusicVolSider(float volume)
    {
        masterMixer.SetFloat("MusicVolume", volume);
    }

    // Makes the SFX slider move the with slider on screen
    public void SXFVolSlider(float volume)
    {
        masterMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    // Sets the correct resolution
    public void ResolutiondDropdown(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Gets the correct graphics from the dropdown menu
    public void GraphicsDropdown(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Toggles the fullscreen
    public void FullscreenToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // takes the player back
    public void BackButton()
    {
        settingsMenu.SetActive(false);
    }
}