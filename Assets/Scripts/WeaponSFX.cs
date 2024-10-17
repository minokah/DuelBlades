using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSFX : MonoBehaviour
{
    public AudioSource attack1, attack2, attack3;

    public void Play(int n)
    {
        switch (n)
        {
            case 0:
                attack1.Play();
                break;
            case 1:
                attack2.Play();
                break;
            case 2:
                attack3.Play();
                break;
        }
    }
}
