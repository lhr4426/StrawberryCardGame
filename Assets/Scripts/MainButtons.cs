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

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void OnNormalPlayButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnHiddenPlayButton()
    {
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
        SceneManager.LoadScene("FakeMainScene");
    }

    public void OnSettingButton()
    {
        settingPanel.SetActive(true);
        hiddenButton.interactable = false;
    }

    public void OnSettingCloseButton()
    {
        settingPanel.SetActive(false);
        hiddenButton.interactable = true;
    }
    public void OnCheckButton()
    {
        checkPanel.SetActive(true);
    }
    public void OnChecClosekButton()
    {
        checkPanel.SetActive(false);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
