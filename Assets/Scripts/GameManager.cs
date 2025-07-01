using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public GameObject endPanel;

    public Text clearTxt;
    public Text bestTxt;

    public int cardCount = 0;

    public bool isGameDone;
    float time = 30.0f;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameDone)time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if(time <= 0.0f)
        {
            GameEnd();
        }
    }

    public void GameEnd(bool isHardMode = false)
    {
        DOTween.Clear(true);
        endPanel.SetActive(true);
        Invoke("OnInteractable", 1f);

        float clearTime = 30f - time;
        clearTxt.text = $"{clearTime:N2}초";

        // 모드별로 다른 키 사용
        string bestKey = isHardMode ? "BestTime_Hidden" : "BestTime_Normal";
        float bestTime = PlayerPrefs.GetFloat(bestKey, float.MaxValue);

        if (clearTime < bestTime)
        {
            bestTime = clearTime;
            PlayerPrefs.SetFloat(bestKey, bestTime);
            PlayerPrefs.Save();
            bestTxt.text = $"{bestTime:N2}초";
        }
        else
        {
            bestTxt.text = $"{bestTime:N2}초";
        }
    }

    public void Matched(bool isHardMode = false) 
    {
        if (firstCard.idx == secondCard.idx)
        {
            AudioManager.instance.MatchSound();
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if(cardCount == 0)
            {
                isGameDone = true;
                Invoke("GameEnd",0.8f);
            }
        }
        else
        {
            if (isHardMode)
            {
                time -= 1.0f;
            }
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }

    public void OnInteractable()
    {
        endPanel.GetComponent<Button>().interactable = true;
        Time.timeScale = 0f;
    }
}
