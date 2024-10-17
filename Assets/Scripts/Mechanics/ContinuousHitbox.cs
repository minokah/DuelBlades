using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousHitbox : Hitbox
{
    public float continuousDamage, continuousRate;
    private float continuousTime = 0;

    protected override void Update()
    {
        base.Update();
        continuousTime += Time.deltaTime;

        if (touching && continuousTime > continuousRate)
        {
            if (player.health > 0)
            {
                mechanicHit.Play();
                player.Damage(continuousDamage);
            }

            continuousTime = 0;
        }
    }
}
