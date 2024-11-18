using UnityEngine;

public class PlayerAttack : UnitAttackController
{
    private PlayerAnimationController playerAnimationController;
    private int comboStep = 0;
    private float comboTimer = 0.5f;
    private float currentCombotIimer;
    private bool isComboActive = false;

    protected override void Awake()
    {
        base.Awake();
        playerAnimationController = GetComponent<PlayerAnimationController>();
    }
    protected override void Update()
    {
        base.Update();

        if (isComboActive)
        {
            currentCombotIimer -= Time.deltaTime;
            if (currentCombotIimer <= 0)
            {
                ResetCombo();
            }
        }
    }

    public void ExecuteComboAttack()
    {
        if (unitStats.Stunned()) return;

        currentCombotIimer = comboTimer;
        isComboActive = true;

        comboStep++;
        if (comboStep == 1)
        {
            playerAnimationController.SetAttack1();
        }
        else if (comboStep == 2)
        {
            playerAnimationController.SetAttack2();
        }
        else if (comboStep >= 3)
        {
            playerAnimationController.SetAttack3();
        }
        ExecuteAttack(normalAttack);
    }

    public void ExecuteNormalAttack()
    {
        ExecuteAttack(normalAttack);
        playerAnimationController.SetAttack1();
    }


    public void ResetCombo()
    {
        comboStep = 0;
        isComboActive = false;
        playerAnimationController.ResetToIdle();
    }

    public void ExecuteSpecialAttack()
    {
        ExecuteAttack(specialAttack);
        playerAnimationController.SetIsSpecialAttack();
    }
}
