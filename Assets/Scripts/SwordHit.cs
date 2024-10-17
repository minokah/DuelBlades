using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    public bool canHit = false;
    public AudioSource hit, waterHit;
    public GameObject character;

    private float cooldown = 0;
    private bool justHit = false;

    void Update()
    {
        cooldown += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cooldown > 2f) justHit = false;
        if (justHit || !canHit) return;

        CharacterInfo hitInfo = other.gameObject.GetComponent<CharacterInfo>();
        EnemyController enemyController = character.GetComponent<EnemyController>();

        if (hitInfo != null)
        {
            if ((enemyController == null && other.name.Equals("Enemy")) 
                || (enemyController != null && enemyController.GetState().Equals("Attack") && other.name.Equals("Player")))
            {
                CharacterInfo playerInfo = character.GetComponent<CharacterInfo>();

                // General damage
                if (hitInfo.health > 0)
                {
                    // Player strikes enemy
                    if (enemyController == null)
                    {
                        playerInfo.charge += playerInfo.chargeAmount;
                        if (playerInfo.charge > playerInfo.chargeMax) playerInfo.charge = playerInfo.chargeMax;
                    }

                    if (!Global.booleans["WaterMusic"]) hit.Play();
                    else waterHit.Play();
                    hitInfo.Damage(playerInfo.weaponDamage * playerInfo.weaponMultiplier);

                    justHit = true;
                }

                cooldown = 0;
            }
        }
    }
}
