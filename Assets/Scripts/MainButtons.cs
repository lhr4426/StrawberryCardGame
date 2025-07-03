using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainButtons : MonoBehaviour
{

    public GameObject settingPanel;
    public Button hiddenButton;
    public GameObject checkPanel;
    public GameObject SettingBtn;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void OnNormalPlayButton()
    {
        AudioManager.instance.NormalClickSound();
        SceneManager.LoadScene("MainScene");
    }

    public void OnTitleButton()
    {
        AudioManager.instance.NormalClickSound();
        SceneManager.LoadScene("StartScene");
    }

    public void OnHiddenPlayButton()
    {
        AudioManager.instance.NormalClickSound();
        SceneManager.LoadScene("HiddenScene");

    }
    public void OnStartHorrorButton()
    {
        AudioManager.instance.bgmAudioSource.Stop();
        AudioManager.instance.bgmAudioSource.mute = false;
        AudioManager.instance.sfxAudioSource.mute = false;
        AudioManager.instance.bgmAudioSource.volume = 1f;
        AudioManager.instance.sfxAudioSource.volume = 1f;
        AudioManager.instance.bgmAudioSource.loop = true;
        AudioManager.instance.bgmAudioSource.PlayOneShot(AudioManager.instance.horrorTitleBgm);
        SceneManager.LoadScene("StartHorrorScene");
    }
    public void OnHorrorPlayButton()
    {
        AudioManager.instance.HorrorClickSound();
        SceneManager.LoadScene("FakeMainScene");
    }

    public void OnSettingButton()
    {
        AudioManager.instance.NormalClickSound();
        settingPanel.SetActive(true);
        hiddenButton.interactable = false;
    }

    public void OnSettingCloseButton()
    {
        AudioManager.instance.NormalClickSound();
        settingPanel.SetActive(false);
        hiddenButton.interactable = true;
    }
    public void OnCheckButton()
    {
        AudioManager.instance.NormalClickSound();
        checkPanel.SetActive(true);
    }
    public void OnChecClosekButton()
    {
        AudioManager.instance.NormalClickSound();
        checkPanel.SetActive(false);
    }

    public void OnExitButton()
    {
        AudioManager.instance.NormalClickSound();
        Application.Quit();
    }

    public void OnHorrorSettingButton()
    {
        AudioManager.instance.HorrorClickSound();

        GameObject caller = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        caller.SetActive(false);
    }
}
