using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour
{
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

    public void OnExitButton()
    {
        Application.Quit();
    }
}
