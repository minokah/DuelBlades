using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestTime : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text rogueKnightBest;

    Game Game;
    void Start()
    {
        if (PlayerPrefs.HasKey("RogueKnight_BestTime")) {
            float time = PlayerPrefs.GetFloat("RogueKnight_BestTime");

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

            rogueKnightBest.SetText("Best: " + text);
        }
    }
}
