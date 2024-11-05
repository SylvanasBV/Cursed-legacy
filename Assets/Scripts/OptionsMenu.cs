using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string> { "960x600" };
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            float aspectRatio = (float)resolutions[i].width / resolutions[i].height;
            if ((resolutions[i].width == 1280 && resolutions[i].height == 800) ||
                (resolutions[i].width == 1440 && resolutions[i].height == 900) ||
                (resolutions[i].width == 1600 && resolutions[i].height == 1000) ||
                (resolutions[i].width == 1920 && resolutions[i].height == 1200))
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResIndex = options.Count - 1;
                }
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Lerp(-80f, 0f, volume));
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
