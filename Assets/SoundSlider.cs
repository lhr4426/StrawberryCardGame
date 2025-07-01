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
        if (isBGM) GetComponent<Slider>().onValueChanged.AddListener((sValue) => { AudioManager.instance.BgmSliderChanged(sValue); });
        else GetComponent<Slider>().onValueChanged.AddListener((sValue) => { AudioManager.instance.SfxSliderChanged(sValue); });
    }
}
