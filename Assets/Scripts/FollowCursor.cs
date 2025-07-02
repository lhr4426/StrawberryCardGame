using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public float speed = 5f; 
    public float gameOverDistance; // 게임 오브젝트와 마우스 포인터 사이 게임오버 기준 거리
    public float shakeDistance; // 카메라가 흔들리기 시작 할 거리

    public Vector3 originalCameraPosition; // 카메라 원래 위치 저장용

    public GameObject jumpScareImage; // 점프스케어 이미지 오브젝트

    private Tween shaking; // 카메라 흔드는거 저장해놓고 중단할 때 사용

    // Start is called before the first frame update
    void Start()
    {
        originalCameraPosition = Camera.main.transform.position; 
        Vector2 size = GetComponent<SpriteRenderer>().bounds.size; // 스프라이트의 크기
        gameOverDistance = size.x / 2; // 게임 오브젝트의 크기 반으로 설정
        shakeDistance = size.x * 2f;
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
            Invoke("OnExitButton", 0.5f);
        }
        
        UpdateShaking(distance);
    }

    
    public void UpdateShaking(float distance)
    {
        // 가까워질수록 카메라가 흔들리도록 함

        float intensity = Mathf.Clamp01(1f - (distance / shakeDistance));
        float strength = intensity * 2.0f;

        if(intensity <= 0) // intensity가 0보다 작다 = 거리가 멀다
        {
            if (shaking != null)
            {
                shaking.Kill(); 
                Camera.main.transform.position = originalCameraPosition; // 카메라 원래 위치로 되돌리기
                shaking = null;
            }
            return;
        }

        if (shaking != null) shaking.Kill();

        shaking = Camera.main.transform.DOShakePosition(
            duration: 0.5f,
            strength: strength,
            vibrato: 30,
            randomness: 90f,
            snapping: false,
            fadeOut: false
            );
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
