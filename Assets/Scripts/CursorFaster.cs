using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CursorFaster : MonoBehaviour
{
    // 싱글톤 패턴 사용
    public static CursorFaster instance;
    public int stageNumber = 0; // 스테이지 번호
    public GameObject horrorChaser; // 공포 추격자 오브젝트

    private float speedAdder = 2.0f; // 점점 더 빠르게

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
            cursor.speed += Random.Range(speedAdder, speedAdder*stageNumber); // 속도 증가
        }
    }


}
