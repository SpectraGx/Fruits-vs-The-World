using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitStats
{
    [SerializeField] private HealthBar healthBar;

    protected override void Start()
    {
        base.Start();
        healthBar = FindObjectOfType<HealthBar>();
    }

    public override bool TakeDamage(AttackData incomingAttack)
    {
        bool isStunned = base.TakeDamage(incomingAttack);
        healthBar.DecrementHealth(incomingAttack.damage);

        if (currentHealth <= 0){
            Debug.Log("GAME OVER");
        }

        return isStunned;
    }

    public void Heal(int amount)
    {
        RestoreUnit(amount, 0);
        healthBar.IncrementHealth(amount);
    }
}
