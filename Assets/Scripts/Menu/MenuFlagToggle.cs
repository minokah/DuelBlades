using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFlagToggle : MonoBehaviour
{
    public string flag;

    Toggle toggle;
    protected virtual void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(Changed);
        toggle.isOn = Global.booleans[flag];
    }

    protected virtual void Changed(bool state)
    {
        Global.booleans[flag] = state;
    }
}
