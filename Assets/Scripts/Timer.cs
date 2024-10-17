using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text currentTime;
    public TMP_Text bestTime;
    public float time = 0;
    public bool running = false;

    // Update is called once per frame
    void Start()
    {
        if (PlayerPrefs.HasKey("RogueKnight_BestTime"))
        {
            float val = PlayerPrefs.GetFloat("RogueKnight_BestTime");
            bestTime.SetText("Best: " + convertTimeString(val));
        }
    }

    void Update()
    {
        if (running) {
            currentTime.SetText("Time: " + convertTimeString(time));
            this.time += Time.deltaTime;
        }
    }

    public string getTimeString()
    {
        return convertTimeString(time);
    }

    public string convertTimeString(float time)
    {
        int mins = Mathf.FloorToInt(time / 60);
        time -= mins * 60f;

        string text = "";
        if (mins > 0) text += mins + ":";
        if (time < 10)
        {
            text += "0";
            if (time < 1) text += "0";
        }
        text += time.ToString("#.##");
        return text;
    }
}
