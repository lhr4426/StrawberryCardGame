using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneManager
{
    public static LoadSceneManager instance;

    public LoadSceneManager()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartSceneLoad()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("StartScene");
    }

    public void NormalSceneLoad()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainScene");
        GameManager.GetInstance.isHardMode = false; 
    }

    public void HiddneSceneLoad()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("HiddenScene");
        GameManager.GetInstance.isHardMode = true; 
    }

    public void ApplicationQuit()
    {
        // 애플리케이션 종료
        Debug.Log("애플리케이션 종료");
        Application.Quit(); // 빌드된 애플리케이션에서 종료

    }
}
