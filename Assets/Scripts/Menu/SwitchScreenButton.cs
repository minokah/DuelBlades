using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchScreenButton : MonoBehaviour
{
    Button button;
    public GameObject sourceCam, goalCam, sourceUI, goalUI;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Go);
    }

    void Go()
    {
        sourceCam.SetActive(false);
        sourceUI.SetActive(false);
        goalCam.SetActive(true);
        goalUI.SetActive(true);
        sourceUI.GetComponent<CanvasGroup>().alpha = 0;
        goalUI.GetComponent<CanvasGroup>().alpha = 0;
    }
}
