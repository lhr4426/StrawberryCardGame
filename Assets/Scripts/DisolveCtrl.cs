using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DisolveCtrl : MonoBehaviour
{
    public Material mat;
    public Sprite chageSprite;
    [SerializeField]bool fadeOut;
    // Start is called before the first frame update
    void Awake()
    {
        mat = GetComponent<SpriteRenderer>().materials[0];
    }
    void Start()
    {
        StartCoroutine(DisolveTimeer(2));
    }
    IEnumerator DisolveTimeer(float time)
    {
        float curr = fadeOut? 0 : 1;
        mat.SetFloat("_Dissolve", fadeOut ? 0f : 1f);
        do
        {
            float currValue = mat.GetFloat("_Dissolve");
            curr += fadeOut?  (Time.deltaTime / time): (-Time.deltaTime / time) ;

            mat.SetFloat("_Dissolve", curr);
            yield return null;

        } while (curr < 1&& curr > 0);
        if (fadeOut)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = chageSprite;
            fadeOut = false;
            mat.SetColor("_BaseColor", Color.red);
            StartCoroutine(DisolveTimeer(2));
        }
        else
        {
            Destroy(this);
        }
    }
}
