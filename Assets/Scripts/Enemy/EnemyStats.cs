using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : UnitStats
{
    private EnemyAnimationController enemyAnimationController;
    private UnitKnockback unitKnockback;

    [Header("Setting: Knockback")]
    private int hitCount = 0;

    protected override void Start()
    {
        base.Start();
        enemyAnimationController = GetComponent<EnemyAnimationController>();
        unitKnockback = GetComponent<UnitKnockback>();
    }

    public override bool TakeDamage(AttackData incomingAttack)
    {
        bool isStunned = base.TakeDamage(incomingAttack);
        Debug.Log($"Daño recibido: {incomingAttack.damage}. Vida: {currentHealth}");

        hitCount++;
        if (hitCount >= incomingAttack.knockbackThreshold)
        {
            ApplyKnockback(incomingAttack);
            hitCount = 0;
        }

        if (currentHealth <= 0)
        {
            enemyAnimationController.Dead();
            Debug.Log("El enemigo ha muerto");
        }

        return isStunned;
    }

    private void ApplyKnockback(AttackData attackData)
    {
        if (unitKnockback != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            Vector2 direction = (transform.position - player.transform.position).normalized;

            Vector2 parabolaForce = new Vector2(direction.x * attackData.knockbackForce.x, attackData.knockbackForce.y);

            unitKnockback.ApplyKnockback(direction, attackData);

            EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                StartCoroutine(ApplyKnockbackCoroutine(enemyMovement, attackData.knockbackDuration));
            }

            //GetComponent<EnemyMovement>().AppplyKnockback(parabolaForce,attackData.knockbackDuration);
        }
    }

    private IEnumerator ApplyKnockbackCoroutine(EnemyMovement enemyMovement, float knockbackDuration)
    {
        enemyMovement.enabled = false;
        yield return new WaitForSeconds(knockbackDuration);
        enemyMovement.enabled = true;
        enemyMovement.OnEnable();
    }

    public void OnDeathAnimationComplete()
    {
        Destroy(gameObject);
    }
}
