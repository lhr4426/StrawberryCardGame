using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CursorFaster : MonoBehaviour
{
    // �̱��� ���� ���
    public static CursorFaster instance;
    public int stageNumber = 0; // �������� ��ȣ
    public GameObject horrorChaser; // ���� �߰��� ������Ʈ

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
        stageNumber++;
        for(int i= 0; i<stageNumber; i++)
        {
            GameObject newChaser = Instantiate(horrorChaser, new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(-10.0f, -16.0f), 0), Quaternion.identity);
            FollowCursor cursor = newChaser.transform.GetChild(0).GetComponent<FollowCursor>();
            cursor.speed += Random.Range(speedAdder, speedAdder*stageNumber); // �ӵ� ����
        }
    }


}
