using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardModeToggle : MenuFlagToggle
{
    public GameObject normalImage, hardImage;

    protected override void Start()
    {
        base.Start();
        SetImages(Global.booleans["Hard"]);
    }

    protected override void Changed(bool state)
    {
        base.Changed(state);
        SetImages(state);
    }

    private void SetImages(bool state)
    {
        if (state)
        {
            normalImage.SetActive(false);
            hardImage.SetActive(true);
        }
        else
        {
            normalImage.SetActive(true);
            hardImage.SetActive(false);
        }
    }
}
