using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public float speed = 5f; 
    public float gameOverDistance; // ���� ������Ʈ�� ���콺 ������ ���� ���ӿ��� ���� �Ÿ�

    public GameObject jumpScareImage;
    public float jumpScareDuration = .5f; 

    // Start is called before the first frame update
    void Start()
    {
        Vector2 size = GetComponent<SpriteRenderer>().bounds.size; // ��������Ʈ�� ũ��
        gameOverDistance = size.x / 2; // ���� ������Ʈ�� ũ�� ������ ����
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition; // ���콺 ������ ��������
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z); // ī�޶���� �Ÿ���ŭ Z�� ��ġ ����
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.z = 0; // Z�� ��ġ�� 0���� �����Ͽ� 2D ��鿡�� �����̵��� ��

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance < gameOverDistance)
        {
            // �Ű��
            jumpScareImage.SetActive(true);
            Destroy(this.gameObject); // FollowCursor ��ũ��Ʈ ����
        }
    }

}
