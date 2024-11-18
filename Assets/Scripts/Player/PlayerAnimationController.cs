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
    private static readonly string Player_Attack2 = "player_attack2";
    private static readonly string Player_Attack3 = "player_attack3";
    private static readonly string Player_SpecialAttack = "player_specialAttack";



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

    public void SetAttack1()
    {
        ChangeAnimationState(Player_Attack);
    }

    public void SetAttack2()
    {
        ChangeAnimationState(Player_Attack2);
    }

    public void SetAttack3()
    {
        ChangeAnimationState(Player_Attack3);
    }

    public void SetIsSpecialAttack()
    {
        ChangeAnimationState(Player_SpecialAttack);
    }

    public void ResetToIdle(){
        ChangeAnimationState(Player_Idle);
    }
}
