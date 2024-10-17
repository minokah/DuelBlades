using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool open = false;
    public GameObject pauseMenu;
    public bool canPause = true;

    // Update is called once per frame
    void Update()
    {
        if (canPause && Input.GetKeyDown(KeyCode.Escape))
        {
            open = !open;

            if (open)
            {
                Cursor.lockState = CursorLockMode.None;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
