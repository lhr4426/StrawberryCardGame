using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : SettingOrder
{
    public bool isBGM;
    public override void Init(Action<bool> action)
    {
        if (isBGM)
        {
            GetComponent<Toggle>().onValueChanged.AddListener((b) =>
            {
                AudioManager.instance.BgmToggleChanged(b);
                AudioManager.instance.SettingToggle();
            });
        }
        else
        {
            GetComponent<Toggle>().onValueChanged.AddListener((b) =>
            {
                AudioManager.instance.SfxToggleChanged(b);
                AudioManager.instance.SettingToggle();
            });
        }

            

        action.Invoke(true);
    }
}
