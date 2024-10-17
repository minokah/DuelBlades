using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float damage;
    public AudioSource mechanicHit;
    private float time = 1; // incredible bandaid fix for jumping
    protected bool touching = false;
    protected CharacterInfo player;

    protected virtual void Update()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (time > 1f && other.name.Equals("Player"))
        {
            touching = true;
            player = other.gameObject.GetComponent<CharacterInfo>();
            if (player.health > 0)
            {
                player.Damage(damage);
                mechanicHit.Play();
            }

            time = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("Player"))
        {
            touching = false;
        }
    }
}
