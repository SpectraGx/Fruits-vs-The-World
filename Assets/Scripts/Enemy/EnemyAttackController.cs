using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : UnitAttackController
{
    private EnemyAnimationController enemyAnimationController;
    private EnemyMovement enemyMovement;
    [SerializeField] private GameObject hitboxAttack;

    protected override void Awake()
    {
        base.Awake();
        enemyAnimationController = GetComponent<EnemyAnimationController>();
        enemyMovement = GetComponent<EnemyMovement>();
        hitboxAttack.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
    }

    public void ApplyDamage(EnemyStats enemyStats)
    {
        if (enemyStats != null)
        {
            enemyStats.TakeDamage(attackToAnimate);
        }
    }

    public void ExecuteNormalAttack()
    {
        if (unitStats.Stunned()) return;
        /*
        if (enemyAnimationController.GetCurrentState() == "enemy_idle" && !enemyMovement.IsMoving() || enemyAnimationController.GetCurrentState() == "enemy_walk")
        {
            ExecuteAttack(normalAttack);
            enemyAnimationController.SetAttack1();
        }
        */

        if (enemyAnimationController.GetCurrentState() != "enemy_attack")
        {
            ExecuteAttack(normalAttack);
            enemyAnimationController.SetAttack1();
        }
        ActivateHitbox();

    }

    public void ActivateHitbox()
    {
        hitboxAttack.SetActive(true);
    }

    public void DeactivateHitbox()
    {
        if (enemyAnimationController.GetCurrentState() == "enemy_attack")
        {
            EndAttack();
            enemyAnimationController.ResetToIdle();
        }
        hitboxAttack.SetActive(false);
    }
}
