using UnityEngine;

public class PlayerAttack : UnitAttackController
{
    private PlayerAnimationController playerAnimationController;

    protected override void Awake()
    {
        base.Awake();
        playerAnimationController = GetComponent<PlayerAnimationController>();
    }
    protected override void Update()
    {
        base.Update();
    }

    public void ExecuteNormalAttack()
    {
        ExecuteAttack(normalAttack);
        playerAnimationController.SetIsAttacking();
    }

    public void ExecuteSpecialAttack()
    {
        ExecuteAttack(specialAttack);
    }
}
