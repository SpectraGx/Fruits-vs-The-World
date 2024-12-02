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

        if (currentState == "enemy_attack" && newState != "enemy_dead") return;

        Debug.Log($"Cambiando de {currentState} a {newState}");

        animator.Play(newState);
        currentState = newState;
    }


    public void SetIsMoving(bool isMoving)
    {
        if (isMoving)
        {
            ChangeAnimationState(Enemy_Walk);
            Debug.Log("Walk");
        }
        else
        {
            ChangeAnimationState(Enemy_Idle);
        }
    }

    public void SetAttack1()
    {
        ChangeAnimationState(Enemy_Attack);
    }

    public void ResetToIdle()
    {
        ChangeAnimationState(Enemy_Idle);
    }

    public void Walk()
    {
        ChangeAnimationState(Enemy_Walk);
    }

    public void Dead()
    {
        ChangeAnimationState(Enemy_Dead);
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        bool isAttackAnimation = currentState == Enemy_Attack;
        bool isMoving = GetComponent<Rigidbody2D>().velocity.magnitude > 0.1f;

        if (isMoving && currentState != Enemy_Walk)
        {
            ChangeAnimationState(Enemy_Walk);
        }
        else if (!isMoving && currentState == Enemy_Idle)
        {
            ChangeAnimationState(Enemy_Idle);
        }

        /*
        if (isAttackAnimation && stateInfo.normalizedTime >= 1f)
        {
            ResetToIdle();
        }
        */
    }

    public string GetCurrentState()
    {
        return currentState;
    }
}
