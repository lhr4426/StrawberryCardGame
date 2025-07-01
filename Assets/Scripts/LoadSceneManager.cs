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
        // ���ø����̼� ����
        Debug.Log("���ø����̼� ����");
        Application.Quit(); // ����� ���ø����̼ǿ��� ����

    }
}
