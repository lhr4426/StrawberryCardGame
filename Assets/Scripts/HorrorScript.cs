using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HorrorScript : MonoBehaviour
{
    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public GameObject endPanel;

    public Text clearTxt;
    public Text bestTxt;

    public int cardCount = 0;

    public bool isGameDone;
    float time = 30.0f;

    public bool isHardMode;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameDone) time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if (time <= 0.0f)
        {
            GameEnd();
        }
    }

    public void GameEnd()
    {
        DOTween.Clear(true);

        float clearTime = 30f - time;
        if (time < 0.0f)
        {
            clearTxt.text = "실패!";
        }
        else
        {
            clearTxt.text = $"{clearTime:N2}초";
        }

        string bestKey = isHardMode ? "BestTime_Hidden" : "BestTime_Normal";
        float bestTime = PlayerPrefs.GetFloat(bestKey, 30f);

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

        endPanel.SetActive(true);

        Invoke("LoadHorrorScene", 5f);
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            AudioManager.instance.MatchSound();

            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if (cardCount == 0)
            {
                isGameDone = true;  
                GameEnd();
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }

    public void LoadHorrorScene()
    {
        SceneManager.LoadScene("HorrorGameScene");
    }

}
