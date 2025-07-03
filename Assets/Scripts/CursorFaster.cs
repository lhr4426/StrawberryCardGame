using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
            GameObject newChaser = Instantiate(horrorChaser, new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-3.0f, 3.0f), 0), Quaternion.identity);
            newChaser.GetComponent<FollowCursor>().speed += speedAdder * stageNumber; // �ӵ� ����
        }
    }


}
