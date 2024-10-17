using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HalfRoomCleaves : Mechanic
{
    private bool westPicked = false;
    private bool eastPicked = false;
    public string westString = "West Cleave";
    public string eastString = "East Cleave";

    public override void DoMechanic()
    {
        state = "Recenter";
    }

    void Update()
    {
        switch (state)
        {
            case "Start":
                westPicked = false;
                eastPicked = false;
                Recenter();
                break;
            case "InCenter":
                if (Random.Range(0, 2) == 0) state = "WestCleave";
                else state = "EastCleave";
                break;
            case "WestCleave":
                westPicked = true;
                Renderer west = aoes["WestCleave"].GetComponent<Renderer>();
                Material westMat = west.material;
                Renderer westHitbox = aoes["WestCleaveHitbox"].GetComponent<Renderer>();
                Material westHitboxMat = westHitbox.material;

                // Telegraph
                if (time < 2)
                {
                    westHitbox.gameObject.transform.position = new Vector3(-1.4632f, 6.5f, -2.8708f);
                    Game.UI.StatusArea.FadeInCast(westString);
                    if (!Global.booleans["Hard"]) FadeIn(westMat, 0.4f);
                }
                // Attack
                else if (time < 4)
                {
                    Game.UI.StatusArea.FadeOutCast();
                    FadeOut(westMat, 0);
                    FadeIn(westHitboxMat, 1);
                    westHitbox.gameObject.SetActive(true);

                    Transform pos = westHitbox.gameObject.transform;
                    pos.position = new Vector3(pos.position.x - 20 * Time.deltaTime, pos.position.y, pos.position.z);
                }
                else if (time < 4.5f)
                {
                    FadeOut(westHitboxMat, 0);
                }
                else
                {
                    westHitbox.gameObject.SetActive(false);
                    time = 0;

                    if (westPicked && eastPicked) state = "Done";
                    else state = "EastCleave";
                }

                time += Time.deltaTime;
                break;
            case "EastCleave":
                eastPicked = true;
                Renderer east = aoes["EastCleave"].GetComponent<Renderer>();
                Material eastMat = east.material;
                Renderer eastHitbox = aoes["EastCleaveHitbox"].GetComponent<Renderer>();
                Material eastHitboxMat = eastHitbox.material;

                // Telegraph
                if (time < 2)
                {
                    eastHitbox.gameObject.transform.position = new Vector3(0.23f, 6.5f, -2.8708f);
                    Game.UI.StatusArea.FadeInCast(eastString);
                    if (!Global.booleans["Hard"]) FadeIn(eastMat, 0.4f);
                }
                // Attack
                else if (time < 4)
                {
                    Game.UI.StatusArea.FadeOutCast();
                    FadeOut(eastMat, 0);
                    FadeIn(eastHitboxMat, 1);
                    eastHitbox.gameObject.SetActive(true);

                    Transform pos = eastHitbox.gameObject.transform;
                    pos.position = new Vector3(pos.position.x + 20 * Time.deltaTime, pos.position.y, pos.position.z);
                }
                else if (time < 4.5f)
                {
                    FadeOut(eastHitboxMat, 0);
                }
                else
                {
                    eastHitbox.gameObject.SetActive(false);
                    time = 0;

                    if (westPicked && eastPicked) state = "Done";
                    else state = "WestCleave";
                }

                time += Time.deltaTime;
                break;
        }
    }
}
