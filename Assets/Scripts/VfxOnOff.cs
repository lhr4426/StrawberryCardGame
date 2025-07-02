using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VfxOnOff : SettingOrder
{
    public override void Init(Action<bool> action)
    {
        if (!PlayerPrefs.HasKey("vfxOnOff")) PlayerPrefs.SetInt("vfxOnOFF", 1);

        if (TryGetComponent<Toggle>(out Toggle t))
        {
            t.onValueChanged.AddListener((b) => { PlayerPrefs.SetInt("vfxOnOFF", b ? 1 : 0); });
            t.isOn = PlayerPrefs.GetInt("vfxOnOFF") == 1 ? true : false;
        }


        action.Invoke(true);
    }

}
