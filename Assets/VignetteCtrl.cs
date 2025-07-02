using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Linq.Expressions;
using Unity.PlasticSCM.Editor.WebApi;
using System;


public class VignetteCtrl : MonoBehaviour
{
    Vignette vignette;
    public float curr = 0;
    readonly float min = 0.1f;
    readonly float max = 0.5f;
    float res;
    bool vigTrigger;

    bool rt;// reverseTrigger

    public Transform testTR;
    void Start()
    {
        if (GetComponent<Volume>().profile.TryGet<Vignette>(out Vignette vgt)) vignette = vgt;
        curr = vignette.intensity.value;
        res = (Screen.width+Screen.height)/2f;
    }
    /// <summary>
    /// worldPos기준으로 마우스를 추격하는 함수
    /// </summary>
    /// <param name="pos"></param>
    public void MouseTraceVignette(Vector3 pos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(pos);
        Vector2 mousePos = Input.mousePosition;



        curr = 1f-Vector2.Distance(screenPos, mousePos)/(res);

        vignette.intensity.value = curr;
    }

    public void VfxOff()
    {
        if (vignette == null&&GetComponent<Volume>().profile.TryGet<Vignette>(out Vignette vgt)) vignette = vgt;
        curr = vignette.intensity.value;


    }
    public void TimePerVignette()
    {
        if (curr >= max)
        {
            curr = max - 0.001f;
            rt = true;
        }
        else if (curr <= min)
        {
            curr = min - 0.001f;
            rt = false;
        }
        float tempSec = Time.deltaTime / 10f;
        curr += rt ? -tempSec : tempSec;
        vignette.intensity.value = curr;
    }
}
