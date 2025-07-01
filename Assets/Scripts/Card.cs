using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  Card : MonoBehaviour
{
    public GameObject front;
    public GameObject back;

    //public Animator anim;

    public SpriteRenderer frontImage;

    public int idx = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"normal{idx}");
    }

    public virtual void OpenCard()
    {
        AudioManager.instance.FlipSound();
        //anim.SetBool("isOpen", true);
        //front.SetActive(true);
        //back.SetActive(false);

        // firstCard == null => firstCard에 정보 넘기기
        // else => secondCard에 정보 넘기기
        // matched 부르기

        ReverseCard(true);

        if (GameManager.instance.firstCard ==  null)
        {
            GameManager.instance.firstCard = this;
        }
        else
        {
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched();
        }
    }

    public virtual void ReverseCard(bool isOpen)
    {
        transform.DORotate(isOpen? new Vector3(0, 180, 0) : Vector3.zero, 0.2f);
    }


    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public virtual void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        //anim.SetBool("isOpen", false);
        //front.SetActive(false);
        //back.SetActive(true);
        ReverseCard(false);
    }
}
