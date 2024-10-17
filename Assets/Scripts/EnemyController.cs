using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour 
{
    Game Game;

    public string currentState = "Idle";

    Animator animator;
    NavMeshAgent nav;

    public bool active = true;

    public Dictionary<string, Mechanic> mechanics = new Dictionary<string, Mechanic>();
    public Mechanic mechanic;

    public AudioSource footstepAudio;
    public WeaponSFX weaponSFX;
    public AudioSource deadAudio;
    public SwordHit swordHit;

    private float attackDelay = 0;
    private float navTimer = 5;
    private float mechanicTimer = 0;

    void Awake()
    {
        Game = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();

        foreach (GameObject m in GameObject.FindGameObjectsWithTag("Mechanic"))
        {
            mechanics.Add(m.name, m.GetComponent<Mechanic>());
        }

    }

    void Start()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ResetActionAnims();

        if (!currentState.Equals("Moving")) footstepAudio.Stop();

        switch (currentState)
        {
            case "Idle":
                animator.SetBool("Idle", true);
                break;
            case "Dead":
                animator.SetBool("Dead", true);
                break;
            case "Casting":
                animator.SetBool("Casting", true);
                break;
            case "Moving":
                if (!footstepAudio.isPlaying) footstepAudio.Play();
                animator.SetBool("Idle", false);
                animator.SetFloat("MovementVertical", 1);
                break;
            case "Attack":
                animator.SetInteger("SwordSwing", 0);
                animator.SetBool("Busy", false);
                attackDelay += Time.deltaTime;
                mechanicTimer += Time.deltaTime;

                if (attackDelay > 2f)
                {
                    currentState = "Moving";
                    navTimer = 5;
                }
                break;
        }

        if (!active || currentState.Equals("Dead")) return;

        // Mechanic is done!
        if (mechanic != null)
        {
            if (mechanic.state.Equals("Done"))
            {
                attackDelay = 0;
                mechanic.state = "None";
                mechanic = null;
            }
        }
        // Chase
        else if (!currentState.Equals("Attack"))
        {
            if (mechanicTimer > 20)
            {
                mechanicTimer = 0;
                Random.InitState(System.Guid.NewGuid().GetHashCode());
                mechanic = mechanics.ElementAt(Random.Range(0, mechanics.Count)).Value;
                mechanic.state = "Start";
                return;
            }

            currentState = "Moving";

            // Run for 2 seconds before recalculating route to player
            if (navTimer >= 2)
            {
                nav.SetDestination(Game.MovementController.gameObject.transform.position);
                navTimer = 0;
            }

            // Pathfind to player
            if (attackDelay > 2f & !nav.pathPending && nav.remainingDistance < nav.stoppingDistance)
            {
                nav.ResetPath();
                footstepAudio.Stop();
                currentState = "Attack";
                animator.SetInteger("SwordSwing", Random.Range(1, 4));
                weaponSFX.Play(Random.Range(0, 3));
                animator.SetBool("Busy", true);
                attackDelay = 0;
            }

            attackDelay += Time.deltaTime;
            navTimer += Time.deltaTime;
            mechanicTimer += Time.deltaTime;
        }
    }

    private void ResetActionAnims()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Dead", false);
        animator.SetBool("Casting", false);
        animator.SetFloat("MovementVertical", 0);
    }

    private void HitActive()
    {
        swordHit.canHit = true;
    }

    private void HitInactive()
    {
        swordHit.canHit = false;
    }

    public void SetState(string state)
    {
        currentState = state;
    }

    public string GetState()
    {
        return currentState;
    }

    public void Die()
    {
        currentState = "Dead";
        deadAudio.Play();
        if (mechanic != null)
        {
            mechanic.DisableAOEs();
            mechanic.state = "None";
        }
        nav.isStopped = true;
        Game.UI.StatusArea.FadeOutCast();
    }

    public void DoMechanic(string mech)
    {
        if (!mechanics.ContainsKey(mech))
        {
            Debug.Log("Mechanic " + mech + " does not have the Mechanic tag!");
        }
        else
        {
            mechanic = mechanics[mech];
            mechanic.state = "Start";
        }
    }

    public NavMeshAgent GetNavAgent()
    {
        return nav;
    }
}
