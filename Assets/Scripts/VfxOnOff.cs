using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VfxOnOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("vfxOnOff")) PlayerPrefs.SetInt("vfxOnOFF", 1);

        Toggle tg = GetComponent<Toggle>();
        tg.onValueChanged.AddListener((b) => { PlayerPrefs.SetInt("vfxOnOFF", b? 1 : 0); });
        tg.isOn = PlayerPrefs.GetInt("vfxOnOFF") == 1 ? true : false;

    }

}
