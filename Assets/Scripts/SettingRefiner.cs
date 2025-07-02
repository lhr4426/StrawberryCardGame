using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SettingRefiner : MonoBehaviour
{
    [SerializeField]SettingOrder[] settings;
    public void Start()
    {

        bool[] d = new bool[settings.Length];
        for (int i = 0; i < settings.Length; i++)
        {
            int tempNum = i;//closureÀÌ½´ ¹æÁö¿ë
            settings[i].Init((result) =>
            {
                d[tempNum] = true;
                if (d.All((x)=>x))
                {
                    gameObject.SetActive(false);
                }
            });
        }
    }
    private void Reset()
    {
        List<SettingOrder> set = new List<SettingOrder>(); 
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent<SettingOrder>(out SettingOrder orders))
            {
                set.Add(orders);
            }
            if (transform.GetChild(i).name == "CheckPanel")
            {
                set.Add(transform.GetChild(i).Find("YesBtn").GetComponent<SettingOrder>());
            }
        }
        settings = set.ToArray();
        set = null;
    }
}
