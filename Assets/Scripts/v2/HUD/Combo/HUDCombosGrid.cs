using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDCombosGrid : MonoBehaviour
{
    private ComboCounter[] comboMeters;

    private void Awake()
    {
        comboMeters = GetComponentsInChildren<ComboCounter>();
    }
    private void Start()
    {
        
    }

    /// <summary>
    /// Get the HUD Combo Counter based on the player. First player should use "1."
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public ComboCounter GetComboCounter(byte player)
    {
        if (player == 0)
        {
            player = 1;
        }
        if (player > 4)
        {
            player = 4;
        }
        return comboMeters[player - 1];
    }
}
