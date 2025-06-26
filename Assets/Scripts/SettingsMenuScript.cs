using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{

    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetMasterVolume(float masterVolume)
    {
        Debug.Log(masterVolume);
        audioMixer.SetFloat("masterVolume", masterVolume);
    }

    public void setGraphics(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
