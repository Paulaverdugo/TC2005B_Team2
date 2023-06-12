using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

// Script to load the volume preferences.

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] AudioMixer audioMixer;

    public const string MUSIC_VOLUME = "MusicVolume";
    public const string SFX_VOLUME = "SFXVolume";

    // Start is called before the first frame update
    void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        SFXSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, SFXSlider.value);
    }

    private void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(MUSIC_VOLUME, Mathf.Log10(volume) * 20);
    }

    private void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(SFX_VOLUME, Mathf.Log10(volume) * 20);
    }
}
