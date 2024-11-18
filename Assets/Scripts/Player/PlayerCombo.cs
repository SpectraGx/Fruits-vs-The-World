using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    private PlayerAnimationController playerAnimationController;
    private int comboStep = 0;
    private float comboTimer = 0.5f; // Tiempo para completar el combo
    private float currentComboTimer;
    private bool isComboActive = false;

    private void Awake()
    {
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerAnimationController.OnAnimationComplete += ResetCombo;
    }

    private void OnDestroy()
    {
        playerAnimationController.OnAnimationComplete -= ResetCombo;
    }

    private void Update()
    {
        if (isComboActive)
        {
            currentComboTimer -= Time.deltaTime;
            if (currentComboTimer <= 0)
            {
                ResetCombo();
            }
        }
    }

    public void ExecuteCombo()
    {
        currentComboTimer = comboTimer;
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
    }

    public void ResetCombo()
    {
        comboStep = 0;
        isComboActive = false;
    }

    public int GetComboStep()
    {
        return comboStep;
    }
}
