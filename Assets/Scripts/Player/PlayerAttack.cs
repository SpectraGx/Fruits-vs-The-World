using UnityEngine;

public class PlayerAttack : UnitAttackController
{
    private PlayerAnimationController playerAnimationController;
    private PlayerCombo playerCombo;


    protected override void Awake()
    {
        base.Awake();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerCombo = GetComponent<PlayerCombo>();
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
    }

    public void OnAnimationComplete()
    {
        if (playerCombo.GetComboStep() == 0)
        {
            playerCombo.ResetCombo();
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
}
