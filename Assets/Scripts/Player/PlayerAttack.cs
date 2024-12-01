using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : UnitAttackController
{
    private PlayerAnimationController playerAnimationController;
    private PlayerCombo playerCombo;
    private AttackHitbox attackHitbox;
    private PlayerMove playerMove;
    [SerializeField] private GameObject hitboxAttack;

    protected override void Awake()
    {
        base.Awake();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerCombo = GetComponent<PlayerCombo>();
        playerMove = GetComponent<PlayerMove>();
        attackHitbox = GetComponentInChildren<AttackHitbox>();
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
            enemyStats.TakeDamage(normalAttack);
            playerCombo.IncrementCombo();
        }
    }

    public void ExecuteNormalAttack()
    {
        if (unitStats.Stunned()) return;
        if (playerAnimationController.GetCurrentState() == "player_idle" && !playerMove.IsMoving())
        {
            ExecuteAttack(normalAttack);
            playerAnimationController.SetAttack1();
            ActivateHitbox();
        }
    }

    public void ExecuteSpecialAttack()
    {
        if (unitStats.Stunned()) return;

        if (playerAnimationController.GetCurrentState() == "player_idle" && !playerMove.IsMoving())
        {
            ExecuteAttack(specialAttack);
            playerAnimationController.SetIsSpecialAttack();
            ActivateHitbox();
        }
    }

    public void ActivateHitbox()
    {
        hitboxAttack.SetActive(true);
    }

    public void DeactivateHitbox()
    {
        if (playerAnimationController.GetCurrentState() == "player_idle")
        {
            hitboxAttack.SetActive(false);
        }
    }
}
