using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvasIn : MonoBehaviour
{
    CanvasGroup canvas;
    public bool active = false;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.alpha < 1) canvas.alpha += 3 * Time.deltaTime;
        else canvas.alpha = 1;
    }
}
