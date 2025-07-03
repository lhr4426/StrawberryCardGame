using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFaster : MonoBehaviour
{
    // �̱��� ���� ���
    public static CursorFaster instance;


    private float speedAdder = 2.0f; // ���� �� ������

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }   
        DontDestroyOnLoad(gameObject);
    }
    public void HorrorSceneAgain()
    {
        GameObject cursor = GameObject.FindWithTag("FollowCursor");
        cursor.GetComponent<FollowCursor>().speed += speedAdder;
    }
}
