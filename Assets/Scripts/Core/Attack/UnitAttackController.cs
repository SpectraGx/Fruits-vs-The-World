using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackController : MonoBehaviour
{
    private Animator animator;
    private UnitMoveController unitMoveController;
    private UnitKnockback unitKnockback;
    private UnitAnimationLayers unitAnimationLayers;
    public UnitStats unitStats;

    public AttackData normalAttack;
    public AttackData specialAttack;

    private AttackData attackToAnimate;
    private bool attacking;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        unitMoveController = GetComponent<UnitMoveController>();
        unitKnockback = GetComponent<UnitKnockback>();
        unitAnimationLayers = GetComponent<UnitAnimationLayers>();
        unitStats = GetComponent<UnitStats>();
    }

    protected virtual void Update()
    {
    }


    public void ExecuteAttack(AttackData attack)
    {
        if (attacking || unitStats.Stunned()) return;
        attackToAnimate = attack;
        attacking = true;
    }

    public void EndAttack()
    {
        attacking = false;
        attackToAnimate = null;
        unitAnimationLayers.SetMovementLayer();
    }

    public void TakeHit(AttackData incomingAttack)
    {
        if (unitStats.Stunned()) return;
        attacking = false;

        unitKnockback.Knockback(transform.position, incomingAttack.knockback, 0);
        unitAnimationLayers.SetHitLayer();

        if (unitStats.TakeDamage(incomingAttack))
        {
            unitStats.Stun(incomingAttack.stunDuration);
        }
    }

    public void Stun(float duration)
    {
        animator.SetTrigger("Stunned");
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator StunCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        EndStun();
    }

    public void EndStun()
    {
        unitAnimationLayers.SetMovementLayer();
    }

    public bool CurrentlyAttacking()
    {
        return attacking;
    }
}
