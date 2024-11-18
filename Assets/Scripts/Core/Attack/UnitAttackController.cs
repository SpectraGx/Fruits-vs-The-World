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
    private List<Collider2D> hitsRecorded;

    public AttackData normalAttack;
    public AttackData specialAttack;

    private AttackData attackToAnimate;
    private bool attacking;
    private int comboHits;
    private int maxComboHits = 3; // Num max de golpes antes de aplicar knockback

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        unitMoveController = GetComponent<UnitMoveController>();
        unitKnockback = GetComponent<UnitKnockback>();
        unitAnimationLayers = GetComponent<UnitAnimationLayers>();
        unitStats = GetComponent<UnitStats>();
        hitsRecorded = new List<Collider2D>();
    }

    protected virtual void Update()
    {
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        //animator.SetBool("Attacking", attacking);
        //animator.SetBool("Stunned", unitStats.Stunned()); 
    }

    public void ExecuteAttack(AttackData attack)
    {
        if (attacking || unitStats.Stunned()) return;
        attackToAnimate = attack;
        attacking = true;
        hitsRecorded.Clear();
        //animator.Play(attack.animationClip.name);
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
        comboHits = 0;
        unitAnimationLayers.SetMovementLayer();
    }

    private void CollectHits()
    {
        Vector2 size = new Vector2(1.5f, 1.0f);
        Vector2 offset = new Vector2(0.5f, 0);
        Vector2 position = transform.position + (Vector3)offset;

        Collider2D[] hits = Physics2D.OverlapBoxAll(position, size, 0f);
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
                continue;
            }

            attackComponent.TakeHit(attackToAnimate);

            if (!attackComponent.unitStats.Stunned() || attackToAnimate == normalAttack)
            {
                attackComponent.unitStats.Stun(attackToAnimate.stunDuration);
            }

            comboHits++;
            if (comboHits >= maxComboHits)
            {
                attackComponent.unitKnockback.Knockback(transform.position, attackToAnimate.knockback, 0);
                comboHits = 0;
            }
        }
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

    public bool IsBlocking()
    {
        return GetComponent<UnitBlock>().IsBlocking();
    }

    public bool CurrentlyAttacking()
    {
        return attacking;
    }

    private void OnDrawGizmos()
    {
        if (attacking)
        {
            Vector2 size = new Vector2(1.5f, 1.0f);
            Vector2 offset = new Vector2(0.5f, 0);
            Vector2 position = transform.position + (Vector3)offset;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(position, size);
        }
    }

}
