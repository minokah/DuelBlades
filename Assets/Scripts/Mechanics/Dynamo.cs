using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dynamo : Mechanic
{
    private bool hit = false;
    public float damage = 150f;
    public AudioSource mechanicHit;
    Vector3 goal;

    public override void DoMechanic()
    {
        state = "Recenter";
    }

    void Update()
    {
        switch (state)
        {
            case "Start":
                int choice = Random.Range(0, 5);

                // NW, NE, SW, SE, Center
                if (choice == 0) goal = waypoints["NW"].transform.position;
                else if (choice == 1) goal = waypoints["NE"].transform.position;
                else if (choice == 2) goal = waypoints["SW"].transform.position;
                else if (choice == 3) goal = waypoints["SE"].transform.position;
                else if (choice == 4) goal = waypoints["Center"].transform.position;

                state = "Run";
                break;
            case "Run":
                NavMeshAgent nav = Game.EnemyController.GetNavAgent();
                nav.SetDestination(goal);
                nav.speed = 20;
                nav.stoppingDistance = 1;

                Game.EnemyController.currentState = "Moving";

                if (!nav.pathPending && nav.remainingDistance < nav.stoppingDistance)
                {
                    Game.EnemyController.currentState = "Casting";
                    state = "Dynamo";
                    nav.speed = 5;
                    nav.stoppingDistance = 0.2f;
                }
                break;
            case "Dynamo":
                Renderer aoe = aoes["Area"].GetComponent<Renderer>();
                Material mat = aoe.material;
                Renderer hitbox = aoes["Hitbox"].GetComponent<Renderer>();
                Material hitboxMat = hitbox.material;

                if (time < 3)
                {
                    Vector3 startPos = Game.EnemyController.transform.position;
                    aoe.transform.position = new Vector3(startPos.x, 5.39f, startPos.z);
                    hitbox.transform.position = new Vector3(startPos.x, 5.84f, startPos.z);
                    Game.UI.StatusArea.FadeInCast(name);
                    if (!Global.booleans["Hard"]) FadeIn(mat, 0.4f);
                }
                else if (time < 4)
                {
                    Game.UI.StatusArea.FadeOutCast();

                    FadeOut(mat, 0);
                    FadeIn(hitboxMat, 1, 10f);

                    Vector3 enemyPos = Game.EnemyController.transform.position;
                    Vector3 playerPos = Game.MovementController.transform.position;

                    float mag = (enemyPos - playerPos).magnitude;
                    if (!hit && mag >= 4.5f)
                    {
                        Game.MovementController.gameObject.GetComponent<CharacterInfo>().Damage(damage);
                        hit = true;
                        mechanicHit.Play();
                    }

                }
                else if (time < 4.5f)
                {
                    hit = false;
                    FadeOut(hitboxMat, 0, 5f);
                }
                else
                {
                    time = 0;
                    state = "Done";
                }

                time += Time.deltaTime;
                break;
        }
    }
}
