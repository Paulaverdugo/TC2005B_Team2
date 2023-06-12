using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";
    // Start is called before the first frame update
    void Awake()
    {
        LoadVolume();

    }

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        // Load the volume preferences.
        mixer.SetFloat(VolumeSettings.MUSIC_VOLUME, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.SFX_VOLUME, Mathf.Log10(sfxVolume) * 20);

    }
}
