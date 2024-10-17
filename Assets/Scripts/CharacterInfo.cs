using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public float health = 1500;
    public float healthMax = 1500;
    public float weaponDamage = 50;
    public float weaponMultiplier = 1;

    public float charge = 100;
    public float chargeMax = 100;
    public float chargeAmount = 2.5f;
    public float drainRate = 5;

    Game Game;

    void Start()
    {
        Game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
    }

    void Update()
    {
        if (health <= 0)
        {
            health = 0;

            // Player?
            if (gameObject.name.Equals("Player"))
            {
                Game.MovementController.Die();
                Game.ChargeController.Die();
                enabled = false;
            }

            // or enemy
            else
            {
                Game.EnemyController.Die();
                enabled = false;
            }
        }
    }

    public void Damage(float amt)
    {
        if (health > 0)
        {
            Random.InitState(System.Guid.NewGuid().GetHashCode());
            health -= Mathf.FloorToInt(amt + Random.Range(0, 51));
        }
        else health = 0;
    }
}
