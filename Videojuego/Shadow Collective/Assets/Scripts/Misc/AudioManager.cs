using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer mixer;

    // Main SFX
    [SerializeField] AudioSource shootSource;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioSource moveSource;
    [SerializeField] AudioClip move;

    // Hack
    [SerializeField] AudioSource hackSource;
    [SerializeField] AudioClip hack;

    // Shield
    [SerializeField] AudioSource shieldSource;
    [SerializeField] AudioClip shield;

    // Stealth
    [SerializeField] AudioSource stealthSource;
    [SerializeField] AudioClip stealth;

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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

    public void PlayShootSound()
    {
        // Making up for bad timing
        shootSource.time = 0.1f;
        shootSource.clip = shoot;
        shootSource.Play();
    }

    public void PlayMoveSound()
    {
        moveSource.time = 0.3f;
        moveSource.clip = move;
        moveSource.Play();
    }

    public void PlayStealthSound()
    {
        stealthSource.clip = stealth;
        stealthSource.Play();
    }

    public void PlayHackSound()
    {
        hackSource.clip = hack;
        hackSource.Play();
    }

    public void PlayShieldSound()
    {
        shieldSource.clip = shield;
        shieldSource.Play();
    }
}
