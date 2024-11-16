using System.Collections.Generic;
using UnityEngine;

public class UnitAttackController : MonoBehaviour
{
    private Animator animator;
    private UnitMoveController unitMoveController;
    private UnitKnockback unitKnockback;
    private UnitAnimationLayers unitAnimationLayers;
    private List<Collider2D> hitsRecorded;

    public Attack lightAttack;
    public Attack heavyAttack;
    public Attack aerialAttack;
    public Attack specialAttack;

    private Attack attackToAnimate;
    private bool attacking;
    private bool stunned;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        unitMoveController = GetComponent<UnitMoveController>();
        unitKnockback = GetComponent<UnitKnockback>();
        unitAnimationLayers = GetComponent<UnitAnimationLayers>();
        hitsRecorded = new List<Collider2D>();
    }

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        animator.SetBool("Attacking", attacking);
        animator.SetBool("Stunned", stunned);
    }

    public void ExecuteAttack(Attack attack)
    {
        if (attacking || stunned) return;
        attackToAnimate = attack;
        attacking = true;
        hitsRecorded.Clear();
        animator.SetTrigger(attack.GetAnimationID().ToString());
    }

    public void OnAttackHitboxActive()
    {
        CollectHits();
        ConsiderHits();
    }

    public void EndAttack()
    {
        attacking = false;
        attackToAnimate = null;
        unitAnimationLayers.SetMovementLayer();
    }

    private void CollectHits()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, attackToAnimate.GetHitboxDimensions(), 0f);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponentInParent<UnitAttackController>() != null && hit.GetComponentInParent<UnitAttackController>() != this)
            {
                if (!hit.GetComponentInParent<UnitAttackController>().IsStunned() && !hitsRecorded.Contains(hit))
                {
                    hitsRecorded.Add(hit);
                }
            }
        }
    }

    private void ConsiderHits()
    {
        foreach (Collider2D hit in hitsRecorded)
        {
            UnitAttackController attackComponent = hit.GetComponentInParent<UnitAttackController>();

            if (attackComponent.IsBlocking())
            {
                attackComponent.ExecuteParry();
                return;
            }

            attackComponent.TakeHit(attackToAnimate);

            if (!attackComponent.IsStunned())
            {
                attackComponent.Stun();
            }
        }
    }

    public void TakeHit(Attack incomingAttack)
    {
        if (stunned) return;
        attacking = false;
        stunned = true;
        bool grounded = unitMoveController.IsGrounded();
        unitKnockback.Knockback(transform.position, incomingAttack.GetKnockback(), incomingAttack.GetHitType(), grounded);
        unitAnimationLayers.SetHitLayer();
    }

    public void Stun()
    {
        stunned = true;
        animator.SetTrigger("Stunned");
    }

    public void EndStun()
    {
        stunned = false;
        unitAnimationLayers.SetMovementLayer();
    }

    public bool IsStunned()
    {
        return stunned;
    }

    public bool IsBlocking()
    {
        return GetComponent<UnitBlock>().IsBlocking();
    }

    public void ExecuteParry()
    {
        GetComponent<UnitParry>().ExecuteParry();
    }

    public bool CurrentlyAttacking()
    {
        return attacking;
    }
}
