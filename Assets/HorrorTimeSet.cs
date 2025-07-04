using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorTimeSet : MonoBehaviour
{
    float curr = 0;
    private void Update()
    {
        curr += Time.deltaTime;
        if (curr> 5f)
        {
            GameManager.instance.time = 20f;
            Destroy(this);
        }
    }
}
