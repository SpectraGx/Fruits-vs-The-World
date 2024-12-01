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
    [SerializeField] private int KnockbackThreshold = 3;
    [SerializeField] Vector2 knockbackforce = new Vector2(4f, 4f);
    [SerializeField] private float knockbackDuration = 0.3f;

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
        if (hitCount >= KnockbackThreshold)
        {
            ApplyKnockback();
            hitCount = 0;
        }

        if (currentHealth <= 0)
        {
            enemyAnimationController.Dead();
            Debug.Log("El enemigo ha muerto");
        }

        return isStunned;
    }

    public void OnDeathAnimationComplete()
    {
        Destroy(gameObject);
    }

    private void ApplyKnockback()
    {
        if (unitKnockback != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            Vector2 direction = (transform.position - player.transform.position).normalized;
            unitKnockback.ApplyKnockback(direction, knockbackforce, knockbackDuration);
        }
    }
}
