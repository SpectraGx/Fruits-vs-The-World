using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Manejo de estados")]
    private Animator animator;
    private string currentState;

    [Header("Estados del jugador")]
    private static readonly string Player_Idle = "player_idle";
    private static readonly string Player_Walk = "player_walk";
    private static readonly string Player_Attack1 = "player_attack1";
    private static readonly string Player_SpecialAttack = "player_specialAttack";

    private UnitAttackController unitAttackController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        unitAttackController = GetComponent<UnitAttackController>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        //Debug.Log($"Cambiando de {currentState} is {newState}");

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

    public void ResetToIdle()
    {
        ChangeAnimationState(Player_Idle);
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        bool isAttackAnimation = currentState == Player_Attack1;

        /*
        if (isAttackAnimation && stateInfo.normalizedTime >= 1f)
        {
            ResetToIdle();
        }
        */
    }

    public void SetIsSpecialAttack()
    {
        ChangeAnimationState(Player_SpecialAttack);
    }

    public string GetCurrentState(){
        return currentState;
    }
}
