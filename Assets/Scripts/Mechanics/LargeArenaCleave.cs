using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LargeArenaCleave : Mechanic
{
    Renderer aoe, hitbox;
    Material aoeMat, hitboxMat;
    GameObject goal;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case "Start":
                int choice = Random.Range(0, 4);

                // NSEW
                if (choice == 0) SetObjects("NorthSafeArea", "NorthSafeHitbox", waypoints["NorthSafe"]);
                else if (choice == 1) SetObjects("SouthSafeArea", "SouthSafeHitbox", waypoints["SouthSafe"]);
                else if (choice == 2) SetObjects("EastSafeArea", "EastSafeHitbox", waypoints["EastSafe"]);
                else if (choice == 3) SetObjects("WestSafeArea", "WestSafeHitbox", waypoints["WestSafe"]);

                state = "Run";
                break;
            case "Run":
                NavMeshAgent nav = Game.EnemyController.GetNavAgent();
                nav.SetDestination(goal.transform.position);
                nav.speed = 20;
                nav.stoppingDistance = 1;

                Game.EnemyController.currentState = "Moving";

                if (!nav.pathPending && nav.remainingDistance < nav.stoppingDistance)
                {
                    Game.EnemyController.currentState = "Casting";
                    state = "Cleave";
                    nav.speed = 5;
                    nav.stoppingDistance = 0.2f;
                }
                break;
            case "Cleave":
                if (time < 5)
                {
                    Vector3 startPos = Game.EnemyController.transform.position;
                    Game.UI.StatusArea.FadeInCast(name);
                    if (!Global.booleans["Hard"]) FadeIn(aoeMat, 0.4f);
                }
                else if (time < 9)
                {
                    Game.UI.StatusArea.FadeOutCast();

                    FadeOut(aoeMat, 0);
                    FadeIn(hitboxMat, 1, 10f);
                    hitbox.gameObject.SetActive(true);
                }
                else if (time < 10)
                {
                    FadeOut(hitboxMat, 0, 5f);
                }
                else
                {
                    hitbox.gameObject.SetActive(false);
                    time = 0;
                    state = "Done";
                }

                time += Time.deltaTime;

                break;
        }
    }

    private void SetObjects(string aoe, string hitbox, GameObject goal)
    {
        this.aoe = aoes[aoe].gameObject.GetComponent<Renderer>();
        this.hitbox = aoes[hitbox].gameObject.GetComponent<Renderer>();
        this.aoeMat = this.aoe.material;
        this.hitboxMat = this.hitbox.material;
        this.goal = goal;
    }
}
