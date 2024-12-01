using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [Header("Manejo de estados")]
    private Animator animator;
    private string currentState;

    [Header("Estados del enemigo")]
    private static readonly string Enemy_Idle = "enemy_idle";
    private static readonly string Enemy_Walk = "enemy_walk";
    private static readonly string Enemy_Attack = "enemy_attack";
    private static readonly string Enemy_Dead = "enemy_dead";


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        Debug.Log($"Cambiando de {currentState} is {newState}");

        animator.Play(newState);
        currentState = newState;
    }

    public void SetIsMoving(bool isMoving)
    {
        if (isMoving)
        {
            ChangeAnimationState(Enemy_Walk);
        }
        else
        {
            ChangeAnimationState (Enemy_Idle);
        }
    }

    public void SetAttack1()
    {
        ChangeAnimationState(Enemy_Attack);
    }

    public void ResetToIdle()
    {
        ChangeAnimationState (Enemy_Idle);
    }

    public void Dead(){
        ChangeAnimationState(Enemy_Dead);
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        bool isAttackAnimation = currentState == Enemy_Attack;

        /*
        if (isAttackAnimation && stateInfo.normalizedTime >= 1f)
        {
            ResetToIdle();
        }
        */
    }

    public string GetCurrentState(){
        return currentState;
    }
}
