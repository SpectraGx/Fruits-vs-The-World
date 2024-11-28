using System.Collections;
using System.Collections.Generic;
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
    }

    private void OnDestroy()
    {
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
