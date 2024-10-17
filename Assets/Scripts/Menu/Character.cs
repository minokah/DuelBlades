using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Animator animator;
    public string state = "Idle";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case "Idle":
                animator.SetBool("Idle", true);
                animator.SetBool("Busy", false);
                animator.SetInteger("SwordSwing", 0);
                break;
            case "Swing":
                animator.SetBool("Idle", false);
                animator.SetBool("Busy", true);
                animator.SetInteger("SwordSwing", 1);
                state = "Idle";
                break;
        }
    }
}
