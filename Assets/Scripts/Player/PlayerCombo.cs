using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    [Header("Referencias")]
    private PlayerAnimationController playerAnimationController;

    [Header("Setting: Combo")]
    private int comboCount = 0;
    private int bestComboCount = 0;
    [SerializeField] private float comboResetTimer = 1.5f;
    private float currentComboTimer;
    private bool isComboActive = false;

    [Header("Inspector: UI")]
    [SerializeField] private EnergyBar energyBar;
    [SerializeField] private TextMeshProUGUI comboText;

    private void Awake()
    {
        playerAnimationController = GetComponent<PlayerAnimationController>();
        comboText.gameObject.SetActive(false);
        energyBar = FindObjectOfType<EnergyBar>();
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

    public void IncrementCombo()
    {
        comboCount++;
        energyBar.IncrementEnergy(1);
        currentComboTimer = comboResetTimer;
        isComboActive = true;

        if (comboCount > bestComboCount)
        {
            bestComboCount = comboCount;
        }

        if (comboCount >= 2)
        {
            comboText.gameObject.SetActive(true);
            comboText.text = comboCount + " COMBO";
        }

        Debug.Log($"El combo es de: {comboCount}");
    }

    public void ResetCombo()
    {
        comboCount = 0;
        isComboActive = false;

        comboText.gameObject.SetActive(false);

        Debug.Log("Combo Reiniciado");
    }

    public int GetComboCount()
    {
        return comboCount;
    }

    public int GetBestComboCount()
    {
        return bestComboCount;
    }
}
