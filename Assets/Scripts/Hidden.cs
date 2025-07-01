using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Hidden : Card
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Setting(int number)
    {
        idx = number;
        Debug.Log(number);
        frontImage.sprite = Resources.Load<Sprite>($"normal{idx}");
        
    }

    public override void OpenCard()
    {

        ReverseCard(true);

        if (GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
        }
        else
        {
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched(true); 
        }
    }

    public override void CloseCard()
    {
        base.CloseCard();
    }
}
