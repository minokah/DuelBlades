using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chariot : Mechanic
{
    public override void DoMechanic()
    {
        state = "Recenter";
    }

    void Update()
    {
        switch (state)
        {
            case "Start":
                state = "Chariot"; // Chariot on the spot
                Game.EnemyController.currentState = "Casting";
                break;
            case "Chariot":
                Renderer aoe = aoes["Area"].GetComponent<Renderer>();
                Material mat = aoe.material;
                Renderer hitbox = aoes["Hitbox"].GetComponent<Renderer>();
                Material hitboxMat = hitbox.material;

                if (time < 3)
                {
                    Vector3 startPos = Game.EnemyController.transform.position;
                    aoe.transform.position = new Vector3(startPos.x, 5.37f, startPos.z);
                    hitbox.transform.position = new Vector3(startPos.x, 5.81f, startPos.z);
                    Game.UI.StatusArea.FadeInCast("Chariot");
                    if (!Global.booleans["Hard"]) FadeIn(mat, 0.4f);
                }
                else if (time < 4)
                {
                    Game.UI.StatusArea.FadeOutCast();

                    FadeOut(mat, 0);
                    FadeIn(hitboxMat, 1, 10f);
                    hitbox.gameObject.SetActive(true);
                }
                else if (time < 4.1f)
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
}
