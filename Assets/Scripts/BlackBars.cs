using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBars : MonoBehaviour
{
    public RectTransform top, bottom;

    float height = 70.0f;
    float rate = 1f;
    float time = 0;
    float current = 0;
    public bool active = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time > 0.0005f) {
            bool changed = false;

            if (active) {
                if (current < height) {
                    current += rate;
                    changed = true;
                }
            }
            else if (current > 0f) {
                current -= rate;
                changed = true;
            }

            if (changed) {
                top.sizeDelta = new Vector2(0, current);
                bottom.sizeDelta = new Vector2(0, current);
            }

            time = 0;
        }

        time += Time.fixedDeltaTime;
    }
}
