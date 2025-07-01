using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField]bool isBGM = false;
    private void Start()
    {
        if (isBGM)
        {
            GetComponent<Slider>().value = AudioManager.instance.bgmVolume;
            GetComponent<Slider>().onValueChanged.AddListener((sValue) => { AudioManager.instance.BgmSliderChanged(sValue); });
        }
        else
        {
            GetComponent<Slider>().value = AudioManager.instance.sfxVolume;
            GetComponent<Slider>().onValueChanged.AddListener((sValue) => { AudioManager.instance.SfxSliderChanged(sValue); });
        }
    }
}
