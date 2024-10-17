using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public GameObject promptObject, trophyImage, skullImage;
    public TMP_Text heading, subtext, scoreText;
    CanvasGroup promptCanvas;

    Game Game;

    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        promptCanvas = promptObject.GetComponent<CanvasGroup>();
        Game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            promptObject.SetActive(true);
            if (promptCanvas.alpha < 1) promptCanvas.alpha += Time.deltaTime;
        }
        else
        {
            promptObject.SetActive(false);
            if (promptCanvas.alpha > 0) promptCanvas.alpha -= Time.deltaTime;
        }
    }

    public void SetCondition(bool victory)
    {
        if (victory)
        {
            trophyImage.SetActive(true);
            heading.SetText("Victory!");
            subtext.SetText("Time: " + Game.UI.Timer.getTimeString());
        }
        else
        {
            skullImage.SetActive(true);
            heading.SetText("Defeat...");
            subtext.SetText("Try again?");
        }
    }
}
