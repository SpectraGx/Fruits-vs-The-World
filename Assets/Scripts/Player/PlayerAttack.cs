using UnityEngine;

public class PlayerAttack : UnitAttackController
{
    private PlayerAnimationController playerAnimationController;
    private PlayerCombo playerCombo;
    private AttackHitbox attackHitbox;


    protected override void Awake()
    {
        base.Awake();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerCombo = GetComponent<PlayerCombo>();
        attackHitbox = GetComponentInChildren<AttackHitbox>();  
    }
    protected override void Update()
    {
        base.Update();
    }

    public void ExecuteComboAttack()
    {
        if (unitStats.Stunned()) return;

        playerCombo.ExecuteCombo();
        ExecuteAttack(normalAttack);
        attackHitbox.Active();
    }

    public void OnAnimationComplete()
    {
        if (playerCombo.GetComboStep() == 0)
        {
            playerCombo.ResetCombo();
            attackHitbox.Desactive();
        }
    }

    public void ExecuteNormalAttack()
    {
        ExecuteAttack(normalAttack);
        playerAnimationController.SetAttack1();
    }

    public void ExecuteSpecialAttack()
    {
        ExecuteAttack(specialAttack);
        playerAnimationController.SetIsSpecialAttack();
        
    }

    public void ApplyDamage(EnemyStats enemyStats){
        if (enemyStats != null){
            enemyStats.TakeDamage(normalAttack);
        }
    }
}
