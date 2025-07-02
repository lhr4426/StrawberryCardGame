using DG.Tweening;
using SpriteGlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public Text[] timeTxt;
    public GameObject endPanel;

    public Queue<Text> looseQueue = new Queue<Text>();
    public Text timeLoosePrefab;
    public Text GetLooseText
    {
        get 
        {
            if (looseQueue.Count > 0)
            {
                Text text = looseQueue.Dequeue();
                return text;
            }
            if (timeLoosePrefab == null)
            {
                timeLoosePrefab = GameObject.Instantiate(timeTxt[0].gameObject).GetComponent<Text>();
                timeLoosePrefab.gameObject.SetActive(false);
                timeLoosePrefab.transform.parent = timeTxt[0].transform.parent;
            }
            Text t = GameObject.Instantiate(timeLoosePrefab);
            t.transform.parent = timeTxt[0].transform.parent;


            return t;
        }
    }


    public Text clearTxt;
    public Text bestTxt;

    public int cardCount = 0;

    public bool isGameDone;
    float time = 30.0f;

    public bool isHardMode;
    public bool isHorrorMode = false;


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
        if (!isGameDone) time -= Time.deltaTime;
        string timeStr = time.ToString("N2");
        foreach (var txt in timeTxt)
        {
            if (txt != null)
                txt.text = timeStr;
        }
        if (time <= 0.0f)
        {
            GameEnd();
        }
    }

    public void CallLoose()
    {
        Text looseT = GetLooseText;
        looseT.gameObject.SetActive(true);
        looseT.text = "-1";
        looseT.fontSize = 50;
        looseT.color = Color.red;

        looseT.transform.position = timeTxt[0].transform.position + (Vector3.right * 150f);
        looseT.transform.DOLocalMoveY(looseT.transform.position.y+100, 3f).OnComplete(() => { looseT.gameObject.SetActive(false); looseQueue.Enqueue(looseT); });
    }
    public void GameEnd()
    {
        DOTween.Clear(true);
        if(isHorrorMode == false)
        {
            Invoke("OnInteractable", 1f);
        }
        
        float clearTime = 30f - time;
        if(time < 0.0f)
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
        if (isHorrorMode == true)
        {
            Invoke("LoadHorrorScene", 5f);
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
                CallLoose();
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
        bool isOn = PlayerPrefs.GetInt("vfxOnOFF") == 1 ? true : false;

        if (isOn)
        {
            first.OutlineWidth = 1;
            second.OutlineWidth = 1;
            first.GlowBrightness = 4;
            second.GlowBrightness = 4;

            do
            {
                first.GlowBrightness -= Time.deltaTime * 10;
                second.GlowBrightness -= Time.deltaTime * 10;
                yield return new WaitForEndOfFrame();
            } while (first.GlowBrightness > 1f);
        }
        else
        {
            first.OutlineWidth = 0;
            second.OutlineWidth = 0;

        }
    }


    public void LoadHorrorScene()
    {
        AudioManager.instance.bgmAudioSource.Stop();
        AudioManager.instance.HorrorGameBgm();
        SceneManager.LoadScene("HorrorGameScene");
    }
}
