using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class ConfigMenu : MonoBehaviour
{
    ConfigMenu[] config;
    [SerializeField] bool spawnedOptions;

    public Slider masterSlider;
    public Slider bgSlider;
    public Slider sfxSlider;

    public Slider rumbleSlider;
    public Toggle tutorialToggle;

    public float masterVolume;
    public float bgVolume;
    public float sfxVolume;

    public float rumbleForce;
    public bool tutorial;

    public AudioMixer mixer;
    [SerializeField] int id;

    bool b;
    private void Awake()
    {
        LoadSettings();
    }
    private void Update()
    {
        if (!b && !spawnedOptions)
        {
            b = true;
            SetMixer();
        }
    }
    private void LoadSettings()
    {
        config = FindObjectsOfType<ConfigMenu>();

        if (!spawnedOptions)
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
            bgVolume = PlayerPrefs.GetFloat("BGVolume", 1f);
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
            rumbleForce = PlayerPrefs.GetFloat("Rumble", 1f);

            SetMixer();
        }

        int tutorialValue = PlayerPrefs.GetInt("Tutorial", 1);

        if (tutorialValue == 1)
        {
            tutorial = true;
        }
        else
        {
            tutorial = false;
        }

        if (spawnedOptions)
        {
            mixer = config[id].mixer;

            masterVolume = config[id].masterVolume;
            bgVolume = config[id].bgVolume;
            sfxVolume = config[id].sfxVolume;
            rumbleForce = config[id].rumbleForce;
            tutorial = config[id].tutorial;

            masterSlider.value = masterVolume;
            bgSlider.value = bgVolume;
            sfxSlider.value = sfxVolume;
            rumbleSlider.value = rumbleForce;
            tutorialToggle.isOn = tutorial;
        }
    }

    public void OnMasterVolumeChanged()
    {
        float volume = masterSlider.value;
        if (spawnedOptions)
        {
            mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MasterVolume", volume);
            masterVolume = volume;
            config[id].masterVolume = volume;
        }

        SaveSettings();
    }
    public void OnBGVolumeChanged()
    {
        float volume = bgSlider.value;
        if (spawnedOptions)
        {
            mixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("BGVolume", volume);
            bgVolume = volume;
            config[id].bgVolume = volume;
        }

        SaveSettings();
    }
    public void OnSFXVolumeChanged()
    {
        float volume = sfxSlider.value;
        if (spawnedOptions)
        {
            mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volume);
            sfxVolume = volume;
            config[id].sfxVolume = volume;
        }

        SaveSettings();
    }
    public void OnRumbleChanged()
    {
        float volume = rumbleSlider.value;
        if (spawnedOptions)
        {
            PlayerPrefs.SetFloat("Rumble", volume);
            rumbleForce = volume;
            config[id].rumbleForce = volume;
        }

        SaveSettings();
    }
    public void OnTutorialToggleChanged()
    {
        if (spawnedOptions)
        {
            if (tutorialToggle.isOn)
            {
                PlayerPrefs.SetInt("Tutorial", 1);
                tutorial = true;
                config[id].tutorial = true;
            }
            else
            {
                PlayerPrefs.SetInt("Tutorial", 0);
                tutorial = false;
                config[id].tutorial = false;
            }
        }

        SaveSettings();
    }
    void SetMixer()
    {
        mixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);
        mixer.SetFloat("BGM", Mathf.Log10(bgVolume) * 20);
        mixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("BGVolume", bgVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.SetFloat("Rumble", rumbleForce);
        PlayerPrefs.SetInt("Tutorial", tutorial ? 1 : 0);

        PlayerPrefs.Save();
    }
}
