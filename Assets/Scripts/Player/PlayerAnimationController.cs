using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private string currentState;

    private static readonly string Player_Idle = "player_idle";
    private static readonly string Player_Walk = "player_walk";
    private static readonly string Player_Attack1 = "player_attack1";
    private static readonly string Player_Attack2 = "player_attack2";
    private static readonly string Player_Attack3 = "player_attack3";
    private static readonly string Player_SpecialAttack = "player_specialAttack";

    public delegate void AnimationCompleteHandler();
    public event AnimationCompleteHandler OnAnimationComplete;

    private UnitAttackController unitAttackController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        unitAttackController = GetComponent<UnitAttackController>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
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
        ChangeAnimationState(Player_Attack1);
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

    public void ResetToIdle()
    {
        ChangeAnimationState(Player_Idle);
    }


    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // Verificar si la animacion es de ataque
        bool isAttackAnimation = currentState == Player_Attack1 || currentState == Player_Attack2 || currentState == Player_Attack3;
        /*
        if (isAttackAnimation && stateInfo.normalizedTime >= 1.0f)
        {
            OnAnimationComplete?.Invoke(); ResetToIdle();
        }
        */

        if (isAttackAnimation)
        {
            if (stateInfo.normalizedTime >= 0.5f && stateInfo.normalizedTime < 0.5f)
            {
                unitAttackController.OnAttackHitboxActive();
            }

            if (stateInfo.normalizedTime >= 1f)
            {
                OnAnimationComplete?.Invoke();
                ResetToIdle();
            }
        }
    }
}