using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameButton : MonoBehaviour
{
    Button exit;

    // Start is called before the first frame update
    void Start()
    {
        exit = GetComponent<Button>();
        exit.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
