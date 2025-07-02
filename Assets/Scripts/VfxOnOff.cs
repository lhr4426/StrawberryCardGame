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

        if (TryGetComponent<Button>(out Button t))
        {
            t.onClick.AddListener(() => { PlayerPrefs.SetInt("vfxOnOFF", PlayerPrefs.GetInt("vfxOnOFF") ==0  ? 1 : 0); });
        }


        action.Invoke(true);
    }

}
