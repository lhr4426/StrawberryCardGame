using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneManager : MonoBehaviour
{
    public void StartSceneLoad()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void NormalSceneLoad()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void HiddneSceneLoad()
    {
        SceneManager.LoadScene("HiddenScene");
    }

    public void ApplicationQuit()
    {
        // 애플리케이션 종료
        Debug.Log("애플리케이션 종료");
        Application.Quit(); // 빌드된 애플리케이션에서 종료

    }
}
