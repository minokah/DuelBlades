using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnMenuButton : MonoBehaviour
{
    Button menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = GetComponent<Button>();
        menu.onClick.AddListener(Begin);
    }

    private void Begin()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
}
