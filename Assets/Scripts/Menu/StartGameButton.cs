using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    Button start;

    // Start is called before the first frame update
    void Start()
    {
        start = GetComponent<Button>();
        start.onClick.AddListener(Begin);
    }

    private void Begin()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
}
