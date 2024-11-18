using System.Collections.Generic;
using UnityEngine;

public class UnitAttackController : MonoBehaviour
{
    private Animator animator;
    private UnitMoveController unitMoveController;
    private UnitKnockback unitKnockback;
    private UnitAnimationLayers unitAnimationLayers;
    public UnitStats unitStats; // Nueva referencia a UnitStats
    private List<Collider2D> hitsRecorded;

    public Attack lightAttack;
    public Attack heavyAttack;
    public Attack aerialAttack;
    public Attack specialAttack;

    private Attack attackToAnimate;
    private bool attacking;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        unitMoveController = GetComponent<UnitMoveController>();
        unitKnockback = GetComponent<UnitKnockback>();
        unitAnimationLayers = GetComponent<UnitAnimationLayers>();
        unitStats = GetComponent<UnitStats>(); // Asignar referencia a UnitStats
        hitsRecorded = new List<Collider2D>();
    }

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        animator.SetBool("Attacking", attacking);
        animator.SetBool("Stunned", unitStats.Stunned()); // Usar UnitStats para verificar el estado de aturdimiento
    }

    public void ExecuteAttack(Attack attack)
    {
        if (attacking || unitStats.Stunned()) return; // Usar UnitStats para verificar el estado de aturdimiento
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
                if (!hit.GetComponentInParent<UnitAttackController>().unitStats.Stunned() && !hitsRecorded.Contains(hit))
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

            if (!attackComponent.unitStats.Stunned())
            {
                attackComponent.unitStats.Stun(); // Usar UnitStats para aplicar el aturdimiento
            }
        }
    }

    public void TakeHit(Attack incomingAttack)
    {
        if (unitStats.Stunned()) return; // Usar UnitStats para verificar el estado de aturdimiento
        attacking = false;

        bool grounded = unitMoveController.IsGrounded();
        //unitKnockback.Knockback(transform.position, incomingAttack.GetKnockback(), incomingAttack.GetHitType(), grounded);
        unitAnimationLayers.SetHitLayer();

        if (unitStats.TakeDamage(incomingAttack))
        {
            unitStats.Stun(); // Usar UnitStats para aplicar el aturdimiento
        }
    }

    public void Stun()
    {
        animator.SetTrigger("Stunned");
    }

    public void EndStun()
    {
        unitAnimationLayers.SetMovementLayer();
    }

    public bool IsBlocking()
    {
        return GetComponent<UnitBlock>().IsBlocking();
    }

    public void ExecuteParry()
    {
        //GetComponent<UnitParry>().ExecuteParry();
    }

    public bool CurrentlyAttacking()
    {
        return attacking;
    }
}
