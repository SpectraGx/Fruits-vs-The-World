using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCombo : MonoBehaviour
{
    private UnitAttackController unitAttackController;
    private int comboCount;
    private float comboResetTimer = 2f;

    private void Awake()
    {
        unitAttackController = GetComponent<UnitAttackController>();
    }

    private void Update()
    {
        if (comboCount > 0)
        {
            comboResetTimer -= Time.deltaTime;
            if (comboResetTimer <= 0)
            {
                comboCount = 0;
                comboResetTimer = 2f;
            }
        }
    }

    public void IncrementCombo()
    {
        comboCount++;
        comboResetTimer = 2f;
        if (comboCount >= 3) // Número de golpes en el combo para desencadenar el stun
        {
            ApplyStunToAllEnemiesInRange();
            comboCount = 0;
        }
    }

    private void ApplyStunToAllEnemiesInRange()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 2f); // Radio de alcance del combo
        foreach (Collider2D enemy in enemies)
        {
            UnitAttackController enemyController = enemy.GetComponent<UnitAttackController>();
            if (enemyController != null && !enemyController.IsStunned())
            {
                enemyController.TakeHit(unitAttackController.lightAttack); // Ejemplo de ataque ligero usado para stun
                enemyController.Stun();
            }
        }
    }
}
