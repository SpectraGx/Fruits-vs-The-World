using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [Header("Inspector")]
    [SerializeField] private Slider energySlider;
    [Header("Settings")]
    private int maxEnergy = 100;
    private int currentEnergy = 0;

    private void Start()
    {
        currentEnergy = 0;
        UpdateEnergyBar();
    }

    public void IncrementEnergy(int amount)
    {
        currentEnergy += amount;
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        UpdateEnergyBar();
    }

    public void DecrementEnergy(int amount){
        currentEnergy -= amount;
        if (currentEnergy < 0){
            currentEnergy = 0;
        }
        UpdateEnergyBar();
    }

    public void ResetEnergy()
    {
        currentEnergy = 0;
        UpdateEnergyBar();
    }

    private void UpdateEnergyBar()
    {
        energySlider.value = (float)currentEnergy / maxEnergy;
    }

    public int GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public int GetMaxEnergy()
    {
        return maxEnergy;
    }
}
