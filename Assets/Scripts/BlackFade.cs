using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFade : MonoBehaviour
{
    Image image;
    float time = 0;
    float rate = 0.01f;
    public bool toBlack = false;

    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0.01f)
        {
            if (!toBlack)
            {
                if (image.color.a > 0) image.color = new Color(0, 0, 0, image.color.a - rate);
            }
            else
            {
                if (image.color.a < 1) image.color = new Color(0, 0, 0, image.color.a + rate);
            }

            time = 0;
        }
        
        time += Time.deltaTime;
    }

    public void SetOpacity(float n)
    {
        image.color = new Color(0, 0, 0, n);
    }
}
