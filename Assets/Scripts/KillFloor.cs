using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
    public AudioSource deathSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            CharacterInfo info = other.GetComponent<CharacterInfo>();
            if (info != null)
            {
                deathSound.Play();
                info.Damage(info.healthMax);
            }
        }
    }
}
