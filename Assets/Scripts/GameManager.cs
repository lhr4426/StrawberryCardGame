using DG.Tweening;
using SpriteGlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static GameManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }
    }

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

    public void GameEnd()
    {
        DOTween.Clear(true);
        endPanel.SetActive(true);
        Invoke("OnInteractable", 1f);

        float clearTime = 30f - time;
        clearTxt.text = $"{clearTime:N2}초";


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

            StartCoroutine(CardEffect(firstCard.clowCtrl, secondCard.clowCtrl));
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
    IEnumerator CardEffect(SpriteGlowEffect first,SpriteGlowEffect second)
    {
        first.OutlineWidth = 1;
        second.OutlineWidth = 1;
        first.GlowBrightness = 4;
        second.GlowBrightness = 4;

        do
        {
            first.GlowBrightness -= Time.deltaTime*10;
            second.GlowBrightness -= Time.deltaTime*10;
            yield return new WaitForEndOfFrame();
        } while (first.GlowBrightness >1f);


    }
}
