using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class Board : MonoBehaviour
{
    public GameObject card;
    public bool isHorrorMode = false;

    private void Awake()
    {
        GameManager.instance.isHardMode = false;
        GameManager.instance.isGameDone = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for(int i = 0; i < 16; i++)
        {
            GameObject go = Instantiate(card, this.transform);
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            if (!isHorrorMode)
            {
                go.transform.position = Vector3.zero;
                go.transform.DOMove(new Vector3(x, y, 0), 0.4f);
            }
            else
            {
                go.transform.position = new Vector3(x, y, 0);
            }
                go.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.instance.cardCount = arr.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
