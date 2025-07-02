using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : SettingOrder
{
    [SerializeField]bool isBGM = false;
    public override void Init(System.Action<bool> action) 
    {
        Slider slider = GetComponent<Slider>();
        if (isBGM)
        {
            slider.value = AudioManager.instance.bgmVolume;
            AudioManager.instance.BgmSliderChanged(slider.value);
            slider.onValueChanged.AddListener((sValue) => { AudioManager.instance.BgmSliderChanged(sValue); });
        }
        else
        {
            slider.value = AudioManager.instance.sfxVolume;
            AudioManager.instance.SfxSliderChanged(slider.value);
            slider.onValueChanged.AddListener((sValue) => { AudioManager.instance.SfxSliderChanged(sValue); });
        }
        action.Invoke(true);
    }
}
public class SettingOrder : MonoBehaviour
{
    public virtual void Init(System.Action<bool> action) { }
}