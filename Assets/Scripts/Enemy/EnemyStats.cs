using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStats : UnitStats
{
    private EnemyAnimationController enemyAnimationController;
    private UnitKnockback unitKnockback;
    private Rigidbody2D rb2D;
    private bool isDead = false;

    [Header("Setting: Knockback")]
    private int hitCount = 0;
    private Coroutine stunCoroutine;

    protected override void Start()
    {
        base.Start();
        enemyAnimationController = GetComponent<EnemyAnimationController>();
        unitKnockback = GetComponent<UnitKnockback>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public override bool TakeDamage(AttackData incomingAttack)
    {
        if (isDead) return false;

        bool isStunned = base.TakeDamage(incomingAttack);

        if (stunCoroutine != null)
        {
            StopCoroutine(stunCoroutine);
        }

        stunCoroutine = StartCoroutine(ResetStunAfterDelay(1f));

        hitCount++;
        if (hitCount >= incomingAttack.knockbackThreshold)
        {
            ApplyKnockback(incomingAttack);
            hitCount = 0;
        }

        if (currentHealth <= 0)
        {
            Dead();
        }

        return isStunned;
    }

    private void Dead()
    {
        isDead = true;
        DisableComponents();
        enemyAnimationController.Dead();
    }

    private void DisableComponents()
    {
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        rb2D.velocity = Vector2.zero;
    }

    private IEnumerator ResetStunAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentStun = 0;
        if (!isDead)
        {
            enemyAnimationController.ResetToIdle();
        }
        stunCoroutine = null;
    }

    private void ApplyKnockback(AttackData attackData)
    {
        if (unitKnockback != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            Vector2 direction = (transform.position - player.transform.position).normalized;

            Vector2 knockbackForce = new Vector2(direction.x * attackData.knockbackForce.x, attackData.knockbackForce.y);

            unitKnockback.ApplyKnockback(direction, attackData);

            EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                StartCoroutine(ApplyKnockbackCoroutine(enemyMovement, knockbackForce, attackData.knockbackDuration));
            }
        }
    }

    private IEnumerator ApplyKnockbackCoroutine(EnemyMovement enemyMovement, Vector2 force, float knockbackDuration)
    {
        enemyMovement.enabled = false;
        rb2D.AddForce(force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockbackDuration);
        rb2D.velocity = Vector2.zero;
        enemyMovement.enabled = true;
        enemyMovement.OnEnable();
    }

    public void OnDeathAnimationComplete()
    {
        Destroy(gameObject);
    }
}

