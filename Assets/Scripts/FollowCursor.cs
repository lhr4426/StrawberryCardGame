using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public float speed = 5f; 
    public float gameOverDistance; // 게임 오브젝트와 마우스 포인터 사이 게임오버 기준 거리

    public GameObject jumpScareImage;
    public float jumpScareDuration = .5f; 

    // Start is called before the first frame update
    void Start()
    {
        Vector2 size = GetComponent<SpriteRenderer>().bounds.size; // 스프라이트의 크기
        gameOverDistance = size.x / 2; // 게임 오브젝트의 크기 반으로 설정
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition; // 마우스 포지션 가져오기
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z); // 카메라와의 거리만큼 Z축 위치 설정
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.z = 0; // Z축 위치를 0으로 설정하여 2D 평면에서 움직이도록 함

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < gameOverDistance)
        {
            // 놀래키기
            jumpScareImage.SetActive(true);
            Destroy(this.gameObject); // FollowCursor 스크립트 제거
        }
    }

}
