using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : UnitStats
{
    private EnemyAnimationController enemyAnimationController;
    protected override void Start()
    {
        base.Start();
        enemyAnimationController = GetComponent<EnemyAnimationController> ();
    }

    public override bool TakeDamage(AttackData incomingAttack)
    {
        bool isStunned = base.TakeDamage(incomingAttack);
        Debug.Log($"Daño recibido: {incomingAttack.damage}. Vida: {currentHealth}");

        if (currentHealth <= 0)
        {
            enemyAnimationController.Dead();
            Debug.Log("El enemigo ha muerto");
        }

        
        return isStunned;
    }

    public void OnDeathAnimationComplete(){
        Destroy(gameObject);
    }
}
