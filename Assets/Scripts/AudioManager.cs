using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;
    public AudioClip bgmClip;
    public AudioClip matchClip;
    public AudioClip flipClip;
    public AudioClip horrorTitleBgm;
    public AudioClip normalClick;
    public AudioClip settingToggleClip;
    public AudioClip horrorGameBgm;
    public AudioClip horrorClick;


    public float bgmVolume;
    public float sfxVolume;
    public bool isBgmMuted;
    public bool isSfxMuted;

    public Toggle bgmToggle;
    public Toggle sfxToggle;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PrefCheck();
    }

    // Start is called before the first frame update
    void Start()
    {
        bgmAudioSource.clip = this.bgmClip;
        bgmAudioSource.volume = bgmVolume;
        sfxAudioSource.volume = sfxVolume;
        bgmAudioSource.mute = isBgmMuted;
        sfxAudioSource.mute = isSfxMuted;
        bgmToggle.isOn = isBgmMuted;
        sfxToggle.isOn = isSfxMuted;
        bgmAudioSource.Play();
        bgmAudioSource.loop = true;
    }

    private void Update()
    {
        // bgmAudioSource.volume = bgmVolume;
    }

    public void PrefCheck()
    {
        if (PlayerPrefs.HasKey("bgmVolume"))
        {
            bgmVolume = PlayerPrefs.GetFloat("bgmVolume");
        }
        else
        {
            bgmVolume = 1.0f;
        }

        if(PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        }
        else
        {
            sfxVolume = 1.0f;
        }

        if (PlayerPrefs.HasKey("isBgmMuted"))
        {
            isBgmMuted = PlayerPrefs.GetInt("isBgmMuted") == 1 ? true : false;
        }
        else
        {
            isBgmMuted = false;
        }

        if (PlayerPrefs.HasKey("isSfxMuted"))
        {
            isSfxMuted = PlayerPrefs.GetInt("isSfxMuted") == 1 ? true : false;
        }
        else
        {
            isSfxMuted = false;
        }

    }

    public void BgmSliderChanged(float changedData)
    {
        bgmVolume = changedData;
        bgmAudioSource.volume = bgmVolume;
        PlayerPrefs.SetFloat("bgmVolume", bgmVolume);
    }

    public void SfxSliderChanged(float changedData)
    {
        sfxVolume = changedData;
        sfxAudioSource.volume = sfxVolume;
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
    }

    public void BgmToggleChanged(bool changedData)
    {
        isBgmMuted = changedData;
        bgmAudioSource.mute = isBgmMuted;
        PlayerPrefs.SetInt("isBgmMuted", changedData ? 1 : 0);
    }

    public void SfxToggleChanged(bool changedData)
    {
        isSfxMuted = changedData;
        sfxAudioSource.mute = isSfxMuted;
        PlayerPrefs.SetInt("isSfxMuted", changedData ? 1 : 0);
    }

    public void FlipSound()
    {
        sfxAudioSource.PlayOneShot(flipClip);
    }

    public void MatchSound()
    {
        sfxAudioSource.PlayOneShot(matchClip);
    }

    public void NormalClickSound()
    {
        sfxAudioSource.PlayOneShot(normalClick);
    }

    public void SettingToggle()
    {
        sfxAudioSource.PlayOneShot(settingToggleClip);
    }

    public void HorrorGameBgm()
    {
        sfxAudioSource.PlayOneShot(horrorGameBgm);
    }

    public void HorrorClickSound()
    {
        sfxAudioSource.PlayOneShot(horrorClick);
    }
}
