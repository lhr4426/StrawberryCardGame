using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HiddenBoard : MonoBehaviour
{

    public GameObject card;
    private void Awake()
    {
        GameManager.instance.isHardMode = true;
        GameManager.instance.isGameDone = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
        arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();

        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(card, this.transform);
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 4.0f;

            go.transform.position = new Vector3(x, y, 0);
            go.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.instance.cardCount = arr.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
