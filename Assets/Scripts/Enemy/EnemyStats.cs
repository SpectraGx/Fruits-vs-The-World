using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : UnitStats
{
    protected override void Start()
    {
        base.Start();
    }

    public override bool TakeDamage(AttackData incomingAttack)
    {
        bool isStunned = base.TakeDamage(incomingAttack);
        Debug.Log($"Daño recibido: {incomingAttack.damage}. Vida: {currentHealth}");

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("El enemigo ha muerto");
        }

        
        return isStunned;
    }
}
