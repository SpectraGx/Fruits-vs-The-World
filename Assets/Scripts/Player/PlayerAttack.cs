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
    [SerializeField] private EnergyBar energyBar;

    protected override void Awake()
    {
        base.Awake();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerCombo = GetComponent<PlayerCombo>();
        playerMove = GetComponent<PlayerMove>();
        attackHitbox = GetComponentInChildren<AttackHitbox>();
        hitboxAttack.SetActive(false);

        energyBar = FindObjectOfType<EnergyBar>();
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

            if (attackToAnimate is SpecialAttackData)
            {
                SpecialAttackData specialAttackData = (SpecialAttackData)attackToAnimate;
                energyBar.DecrementEnergy(specialAttackData.energyCost);
            }
            else
            {
                playerCombo.IncrementCombo();
                energyBar.IncrementEnergy(5);
            }
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

        if (specialAttack is SpecialAttackData specialAttackData && energyBar.GetCurrentEnergy() >= specialAttackData.energyCost)
        {
            if (playerAnimationController.GetCurrentState() == "player_idle" && !playerMove.IsMoving())
            {
                ExecuteAttack(specialAttack);
                playerAnimationController.SetIsSpecialAttack();
                ActivateHitbox();
            }
        }
        else {
            Debug.Log("No hay suficiente energia para un ataque especial");
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
            EndAttack();
        }
    }
}
