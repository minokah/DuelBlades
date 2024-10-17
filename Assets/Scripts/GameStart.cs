using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    Game Game;

    public bool done = false;
    public AudioSource song, hardSong, waterSong;
    public GameObject startCam, playerCam;
    float timer = 0;

    void Awake()
    {
        Game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }

    void Start()
    {
        Random.InitState(System.Guid.NewGuid().GetHashCode());
        Game.EnemyController.active = false;
        Game.MovementController.active = false;
        Game.UI.BlackFade.SetOpacity(1);
        Game.UI.StatusArea.visible = false;
        Game.UI.StatusArea.SetOpacity(0);
        startCam.SetActive(true);
        playerCam.SetActive(false);

        // Hard? Make enemy damage multiplier higher
        if (Global.booleans["Hard"])
        {
            Game.EnemyController.GetComponent<CharacterInfo>().weaponMultiplier = 2f;
        }
    }

    void Update()
    {
        if (timer > 3)
        {
            startCam.SetActive(false);
            playerCam.SetActive(true);
        }

        if (timer > 4)
        {
            int songChoice = 0;
            if (Global.booleans["Hard"]) songChoice = 1;
            if (Global.booleans["WaterMusic"]) songChoice = 2;

            switch (songChoice)
            {
                case 0:
                    if (!song.isPlaying) song.Play();
                    break;
                case 1:
                    if (!hardSong.isPlaying) hardSong.Play();
                    break;
                case 2:
                    if (!waterSong.isPlaying) waterSong.Play();
                    break;
            }
            
            Game.UI.StatusArea.visible = true;
            Game.UI.Timer.running = true;
            Game.EnemyController.active = true;
            Game.MovementController.active = true;
            enabled = false;
        }

        timer += Time.deltaTime;
    }
}
