using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class SettingsMenu : MonoBehaviour
{
    //public TMP_Dropdown
    public TMP_Dropdown resolutionDropdown;
   // public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public AudioMixer audioMixer;
    public AudioMixer sfxMixer;
    public Slider sfxSlider;
    public Slider mainVolumeSlider;
    public Slider textSpeed;
    public Slider autoSpeed;
    public Toggle fullscreen;
    public TMP_Dropdown quality;
    public void PreSetup() {
        textSpeed.value = PlayerPrefs.GetFloat("textSpeed");
        SetTextSpeed(PlayerPrefs.GetFloat("textSpeed"));

        autoSpeed.value = PlayerPrefs.GetFloat("autoSpeed");
        SetAutoSpeed(PlayerPrefs.GetFloat("autoSpeed"));

        mainVolumeSlider.value = PlayerPrefs.GetFloat("mainVol");
        SetVolume(PlayerPrefs.GetFloat("mainVol"));

        sfxSlider.value = PlayerPrefs.GetFloat("sfxVol");
        SetSFXVolume(PlayerPrefs.GetFloat("sfxVol"));

        bool ticked = false;
        if (PlayerPrefs.GetInt("fullscreen") == 1)
        {
            ticked = true;
        }

        SetFullscreen(ticked);

        fullscreen.isOn = ticked;
        int qualityLvl = PlayerPrefs.GetInt("quality");

        QualitySettings.SetQualityLevel(qualityLvl);

        quality.value = qualityLvl;
        SetQuality(qualityLvl);
        
    }
    void Start() {
        PreSetup();
        GetResolutions();
       

        
    }
    public void GetResolutions() {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        

        List<string> options = new List<string>();
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate+"Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && resolutions[i].refreshRate==Screen.currentResolution.refreshRate)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
        int dropdownVal = PlayerPrefs.GetInt("resIndex");
        resolutionDropdown.value = dropdownVal;
        SetResolution(dropdownVal);

    }

    public void SetResolution(int resIndex) {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen, res.refreshRate);
        PlayerPrefs.SetInt("resIndex",resIndex);
    }
    public void SetVolume(float volume) {
        audioMixer.SetFloat("volume",volume);
        PlayerPrefs.SetFloat("mainVol", volume);
    }
    public void SetSFXVolume(float volume)
    {
        sfxMixer.SetFloat("sfx", volume);
        PlayerPrefs.SetFloat("sfxVol", volume);
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
        if (isFullscreen == false)
        {
            PlayerPrefs.SetInt("fullscreen", 0);
        }
        else {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        
    }
    public void SetTextSpeed(float speed) {
        GameVars.delay = speed;
        PlayerPrefs.SetFloat("textSpeed", speed);
    }
    public void SetAutoSpeed(float speed)
    {
        GameVars.autoSpeed = speed;
        PlayerPrefs.SetFloat("autoSpeed", speed);
    }
}
