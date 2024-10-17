using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public float rate = 1;
    public float xPos = -52.64f;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(xPos, 2, 0);
        xPos += rate * Time.deltaTime;

        if (xPos > 37.39f) xPos = -52.64f;
    }
}
