using Coffee.UIExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorParticle : MonoBehaviour
{
    UIParticle part;
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<UIParticle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            transform.position = Input.mousePosition;
            part.Play();
        }
    }
}
