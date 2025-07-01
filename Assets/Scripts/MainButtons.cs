using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainButtons : MonoBehaviour
{

    public GameObject settingPanel;
    public Button hiddenButton;
    public void OnNormalPlayButton()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
        GameManager.GetInstance.isHardMode = false;
    }

    public void OnHiddenPlayButton()
    {
        SceneManager.LoadScene("HiddenScene");
        Time.timeScale = 1f;
        GameManager.GetInstance.isHardMode = true;

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

    public void OnExitButton()
    {
        Application.Quit();
    }
}
