using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : UnitStats
{
    private int comboHits;
    private int highestCombo;
    private float comboTimer;
    private float multiplierKick;             //Extra damage from Kicks, will not influence stun damage
    private float multiplierPunch;            //Extra damage from Punches, will not influence stun damage
    private float multiplierStun;             //Extra stun damage from normal attacks, punch or kick
    private float multiplierSpecial;          //Extra damage from Special, will not influence stun damage
    private float multiplierSpecialDiscount;  //Discount of Meter from using Specials

    public ComboCounter comboCounter;
    public HUDMeters meters;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Get Player One.");
        GetMeter(1);
        GetComboCounter(1);
        SetTestWolf();
        SetTestMultipliers();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (comboTimer > 0f)
        {
            comboTimer -= Time.deltaTime;
        }
        else
        {
            ResetComboTooLong();
        }
    }
    /// <summary>
    /// Add one hit to this Player's Combo.
    /// </summary>
    public void AddToCombo()
    {
        comboHits++;
        if (comboHits > highestCombo)
        {
            highestCombo = comboHits;
        }
        comboTimer = GetTimer();
        comboCounter.SetText(comboHits, comboTimer);
    }
    /// <summary>
    /// Reset the Combo is taking too long.
    /// </summary>
    public void ResetComboTooLong()
    {
        comboHits = 0;
    }
    /// <summary>
    /// Reset the Combo due the player getting hit.
    /// </summary>
    public void ResetComboHit()
    {
        comboHits = 0;
        comboTimer = 0;
        comboCounter.PlayerHit();
    }
    /// <summary>
    /// Light up the Meter Bar if there's not enough Meter to use.
    /// </summary>
    public void NotEnoughMeter()
    {
        if (meters != null)
        {
            meters.LightUpMeter();
        }
    }
    public override bool TakeDamage(Attack incomingAttack, UnitStats attackingUnit)
    {
        bool temp = base.TakeDamage(incomingAttack, attackingUnit);
        if (meters != null)
        {
            meters.SetHealthBarCurrent(currentHealth);
        }
        return temp;
    }
    public override bool MeterBurn(int meterBurn)
    {
        bool temp = base.MeterBurn(meterBurn);
        if (meters != null)
        {
            meters.SetMeterBarCurrent(currentMeter, true);
        }
        return temp;
    }
    public override void RestoreUnit(int healthRestore, int meterRestore)
    {
        base.RestoreUnit(healthRestore, meterRestore);
        if (meters != null)
        {
            meters.SetHealthBarCurrent(currentHealth);
            meters.SetMeterBarCurrent(currentMeter, false);
        }
    }
    public override void RestAll()
    {
        base.RestAll();
        if (meters != null)
        {
            meters.SetHealthBarCurrent(currentHealth);
            meters.SetMeterBarCurrent(currentMeter, false);
        }
    }
    public float MultiplierKick()
    {
        return multiplierKick;
    }
    public float MultiplierPunch()
    {
        return multiplierPunch;
    }
    public float MultiplierStun()
    {
        return multiplierStun;
    }
    public float MultiplierSpecial()
    {
        return multiplierSpecial;
    }
    public float MultiplierSpecialDiscount()
    {
        return multiplierSpecialDiscount;
    }

    /// <summary>
    /// Get HUD for some player.
    /// </summary>
    /// <param name="whichPlayer"></param>
    private void GetMeter(byte whichPlayer)
    {
        meters = FindObjectOfType<HUDMetersGrid>().GetMeter(whichPlayer);
    }

    /// <summary>
    /// Get Combo Counter for some player.
    /// </summary>
    /// <param name="whichPlayer"></param>
    private void GetComboCounter(byte whichPlayer)
    {
        comboCounter = FindObjectOfType<HUDCombosGrid>().GetComboCounter(whichPlayer);
    }
    /// <summary>
    /// Get Timer for current Combo hit. The higher the combo, the less time to increase the combo hits.
    /// </summary>
    /// <returns></returns>
    private float GetTimer()
    {
        if (comboHits < 10)
        {
            return 5f;
        }
        else if ((comboHits >= 10) && (comboHits < 20))
        {
            return 4.5f;
        }
        else if ((comboHits >= 20) && (comboHits < 30))
        {
            return 4f;
        }
        else if ((comboHits >= 30) && (comboHits < 40))
        {
            return 3.5f;
        }
        else if ((comboHits >= 40) && (comboHits < 50))
        {
            return 3f;
        }
        else
        {
            return 2.5f;
        }
    }

    public void SetTestWolf()
    {
        meters.SetMetersWolf();
        statMeterGain = 2;
        maxHealth = 1000;
        maxMeter = 650;
        maxStun = 1000;
        currentHealth = maxHealth;
        currentMeter = maxMeter;
        meters.SetHealthBarCurrent(currentHealth);
        meters.SetMeterBarCurrent(currentMeter, false);
        GetComponentInChildren<ParticlePooler>().SetParticlesSpecialMovesWolf();
    }
    public void SetTestMultipliers()
    {
        multiplierKick = 1f;
        multiplierPunch = 1f;
        multiplierStun = 1f;
        multiplierSpecial = 1f;
        multiplierSpecialDiscount = 1f;
    }
}
