using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Linq;
using Unity.VisualScripting;
using DG.Tweening;


public class VignetteCtrl : MonoBehaviour
{
    Vignette vignette;
    public float curr = 0;
    readonly float min = 0.1f;
    readonly float max = 0.5f;
    float res = 1f;
    bool vigTrigger;
    public List<Transform> tracers = new List<Transform>();
    bool rt;// reverseTrigger
    public bool isHorrorScene = false;

    private void Awake()
    {
        if (!isHorrorScene) Destroy(this);
        if (GetComponent<Volume>().profile.TryGet<Vignette>(out Vignette vgt)) vignette = vgt;
    }
    void Start()
    {
        curr = vignette.intensity.value;
        res = (Screen.width+Screen.height)/2f;
    }

    private void Update()
    {
        MouseTraceVignette();
    }

    public void RegistTR(Transform tr)
    {
        tracers.Add(tr);
    }
    /// <summary>
    /// worldPos기준으로 마우스를 추격하는 함수
    /// </summary>
    /// <param name="pos"></param>
    public void MouseTraceVignette()
    {

        Vector2 pos = Vector2.one;
        float cDist = float.MaxValue;
        for (int i = 0; i < tracers.Count; i++)
        {
            if (tracers[i].IsDestroyed()) return;
            Vector2 tempScreenPos = Camera.main.WorldToScreenPoint(tracers[i].position);
            float x = tempScreenPos.x - Input.mousePosition.x;
            float y = tempScreenPos.y - Input.mousePosition.y;

            float tempdist = (x * x) + (y * y);
            if (cDist > tempdist)
            {
                cDist = tempdist;
                pos = tracers[i].transform.position;
            }
        }

        curr = 1f-(Mathf.Sqrt(cDist) / res);

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
