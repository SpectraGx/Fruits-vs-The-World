using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private string currentState;

    private static readonly string Player_Idle = "player_idle";
    private static readonly string Player_Walk = "player_walk";
    private static readonly string Player_Attack = "player_attack1";


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newstate)
    {
        if (currentState == newstate) return;
        animator.Play(newstate);
        currentState = newstate;
    }

    public void SetIsMoving(bool isMoving)
    {
        if (isMoving)
        {
            ChangeAnimationState(Player_Walk);
        }
        else
        {
            ChangeAnimationState(Player_Idle);
        }
    }

    public void SetIsAttacking()
    {
        ChangeAnimationState(Player_Attack);
    }
}
