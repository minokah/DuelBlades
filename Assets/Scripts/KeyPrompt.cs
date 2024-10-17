using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPrompt : MonoBehaviour
{
    public TMP_Text text;

    public void SetText(string s) {
        text.text = s;
    }

    public void SetVisible(bool b) {
        text.gameObject.SetActive(b);
    }
 }
