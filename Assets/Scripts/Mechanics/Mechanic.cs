using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mechanic : MonoBehaviour
{
    public string name;
    public string state = "None";
    public Dictionary<string, GameObject> aoes = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> waypoints = new Dictionary<string, GameObject>();
    protected float time = 0;

    protected Game Game;

    void Awake()
    {
        Game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();

        foreach (Transform parent in transform)
        {
            if (parent.name.Equals("AOEs"))
            {
                foreach (Transform aoe in parent)
                {
                    aoes.Add(aoe.name, aoe.gameObject);
                }
            }
            else if (parent.name.Equals("Waypoints"))
            {
                foreach (Transform waypoint in parent)
                {
                    waypoints.Add(waypoint.name, waypoint.gameObject);
                }
            }
        }
    }

    public virtual void DoMechanic() {}

    public void DisableAOEs()
    {
        foreach (GameObject aoe in aoes.Values)
        {
            aoe.SetActive(false);
        }
    }

    public void SetAlpha(Material mat, float amt)
    {
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, amt);
    }

    public void FadeIn(Material mat, float goal, float rate = 1)
    {
        if (mat.color.a < goal) SetAlpha(mat, mat.color.a + rate * Time.deltaTime);
        else SetAlpha(mat, goal);
    }

    public void FadeOut(Material mat, float goal, float rate = 1)
    {
        if (mat.color.a > goal) SetAlpha(mat, mat.color.a - rate * Time.deltaTime);
        else SetAlpha(mat, goal);
    }

    public void Recenter()
    {
        NavMeshAgent nav = Game.EnemyController.GetNavAgent();
        nav.SetDestination(waypoints["Center"].transform.position);
        Game.EnemyController.currentState = "Moving";

        if (!nav.pathPending && nav.remainingDistance < nav.stoppingDistance)
        {
            Game.EnemyController.currentState = "Casting";
            state = "InCenter";
        }
    }
}
