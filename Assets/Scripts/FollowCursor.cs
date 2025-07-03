using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public float speed = 0f; 
    public float gameOverDistance; // ���� ������Ʈ�� ���콺 ������ ���� ���ӿ��� ���� �Ÿ�
    public float shakeDistance; // ī�޶� ��鸮�� ���� �� �Ÿ�

    private bool stopShake;

    public VignetteCtrl vignetter;

    public Vector3 originalCameraPosition; // ī�޶� ���� ��ġ �����

    public GameObject jumpScareImage; // �������ɾ� �̹��� ������Ʈ

    private Tween shaking; // ī�޶� ���°� �����س��� �ߴ��� �� ���

    public GameObject horrorChaser;

    private bool gameOver = false; // ���� ���� ���¸� ��Ÿ���� ���� : �Ҹ� �� ���� ����ϱ� ���� ���

    // Start is called before the first frame update
    void Start()
    {
        originalCameraPosition = Camera.main.transform.position; 
        Vector2 size = GetComponent<SpriteRenderer>().bounds.size; // ��������Ʈ�� ũ��
        gameOverDistance = size.x / 2; // ���� ������Ʈ�� ũ�� ������ ����
        shakeDistance = size.x * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition; // ���콺 ������ ��������
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z); // ī�޶���� �Ÿ���ŭ Z�� ��ġ ����
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.z = 0; // Z�� ��ġ�� 0���� �����Ͽ� 2D ��鿡�� �����̵��� ��

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if(!stopShake)vignetter.MouseTraceVignette(transform.position);
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < gameOverDistance && !gameOver)
        {
            Debug.Log("Game Over!");  
            // �Ű��
            jumpScareImage.SetActive(true);
            stopShake = true;
            // StartCoroutine(DestroyDirection());
            gameOver = true; // ���� ���� ���·� ����
            /*
            ���� : �׽�Ʈ �� �� ���⼭ ����� null �������� Destroy �ȵ˴ϴ�.
            */
            AudioManager.instance.HorrorGameOverSound(); 
            Destroy(this.gameObject);
        }

        UpdateShaking(distance);

        if (horrorChaser != null)
        {
            SpriteRenderer sr = horrorChaser.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                float t = Time.time;
                if((t * 0.4f) / 1 %2 < 1)
                {
                    Color newColor = Color.HSVToRGB(1f, (t * 0.4f) % 1f, 1f);
                    newColor.a = (t * 0.4f) % 1f;
                    sr.color = newColor;
                }
                else
                {
                    Color newColor = Color.HSVToRGB(1f, 1f - (t * 0.4f) % 1f, 1f);
                    newColor.a = 1f - (t * 0.4f) % 1f;
                    sr.color = newColor;
                }
                
            }
        }
    }
    public void OnDestroy()
    {
        Invoke("OnExitButton", 0.5f);
    }

    public void UpdateShaking(float distance)
    {
        
        if (stopShake) 
        {
            Camera.main.transform.position = originalCameraPosition; // ī�޶� ���� ��ġ�� �ǵ�����
            return; 
        }
        // ����������� ī�޶� ��鸮���� ��

        float intensity = Mathf.Clamp01(1f - (distance / shakeDistance));
        float strength = intensity * 2.0f;

        if(intensity <= 0) // intensity�� 0���� �۴� = �Ÿ��� �ִ�
        {
            if (shaking != null)
            {
                shaking.Kill(); 
                Camera.main.transform.position = originalCameraPosition; // ī�޶� ���� ��ġ�� �ǵ�����
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
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }
    IEnumerator DestroyDirection()
    {
        Debug.Log("Destroy");
        float curr = 0;
        do
        {
            curr += Time.deltaTime;
            yield return null;
        } while (curr <0.5f);
        Destroy(gameObject);
    }

}
