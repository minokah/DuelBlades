using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameListener : MonoBehaviour
{
    bool scoreChecked = false;
    Game Game;

    void Start()
    {
        Game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }

    void Update()
    {
        // Victory!
        if (Game.EnemyController.GetComponent<CharacterInfo>().health <= 0)
        {
            Game.UI.EndScreen.active = true;
            Game.UI.EndScreen.SetCondition(true);
            Game.UI.PauseMenu.canPause = false;
            Game.UI.PauseMenu.gameObject.SetActive(false);
            Game.EnemyController.active = false;
            Game.MovementController.active = false;
            Game.MovementController.canMove = false;
            Game.MovementController.GetComponent<Animator>().SetBool("Idle", true);
            Game.MovementController.GetComponent<Animator>().SetFloat("MovementVertical", 0);
            Game.UI.Timer.running = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.None;

            // Record new highscore if possible
            if (!scoreChecked)
            {
                if (PlayerPrefs.HasKey("RogueKnight_BestTime"))
                {
                    float val = PlayerPrefs.GetFloat("RogueKnight_BestTime");
                    if (Game.UI.Timer.time < val)
                    {
                        PlayerPrefs.SetFloat("RogueKnight_BestTime", Game.UI.Timer.time);
                        Game.UI.EndScreen.scoreText.SetText("New Record!");
                    }
                }
                else
                {
                    Game.UI.EndScreen.scoreText.SetText("New Record!");
                    PlayerPrefs.SetFloat("RogueKnight_BestTime", Game.UI.Timer.time);
                }

                scoreChecked = true;
            }
        }

        // Defeat :(
            if (Game.MovementController.GetComponent<CharacterInfo>().health <= 0)
        {
            Game.UI.EndScreen.active = true;
            Game.UI.EndScreen.SetCondition(false);
            Game.UI.PauseMenu.canPause = false;
            Game.UI.PauseMenu.gameObject.SetActive(false);
            Game.EnemyController.currentState = "Idle";
            Game.EnemyController.active = false;
            Game.UI.Timer.running = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
