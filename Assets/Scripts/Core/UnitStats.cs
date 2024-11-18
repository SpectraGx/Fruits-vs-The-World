using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    private UnitAttackController unitAttackController;
    private float meterDelayTimer;
    private int currentStun;
    private int currentMeter;
    private int currentHealth;

    public byte meterDelay = 3;
    public byte statMeterGain = 2;
    public int maxMeter = 100;
    public int maxHealth = 100;
    public int maxStun = 20;
    public bool isPlayer;

    protected virtual void Awake()
    {
        unitAttackController = GetComponent<UnitAttackController>();
    }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        currentMeter = 0;
        currentStun = 0;
        meterDelayTimer = 0;
    }

    protected virtual void Update()
    {
        if (currentMeter < maxMeter)
        {
            if (meterDelayTimer > 0f)
            {
                meterDelayTimer -= Time.deltaTime;
            }
            else
            {
                RestoreUnit(0, statMeterGain);
                meterDelayTimer = meterDelay;
            }
        }
    }

    public virtual bool TakeDamage(AttackData incomingAttack)
    {
        int totalDamage = incomingAttack.damage;
        currentHealth -= totalDamage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.LogWarning("Zero health! KO!!");
        }

        currentMeter += 3;
        if (currentMeter >= maxMeter)
        {
            currentMeter = maxMeter;
        }

        currentStun += (int)incomingAttack.stunDuration;

        return currentStun >= maxStun;
    }

    public void Stun(float duration)
    {
        currentStun = maxStun;
        unitAttackController.Stun(duration);
    }

    public void EndStun()
    {
        currentStun = 0;
        unitAttackController.EndStun();
    }

    public virtual bool MeterBurn(int meterBurn)
    {
        if (currentMeter < meterBurn)
        {
            return false;
        }
        currentMeter -= meterBurn;
        return true;
    }

    public virtual void RestoreUnit(int healthRestore, int meterRestore)
    {
        currentHealth += healthRestore;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        currentMeter += meterRestore;
        if (currentMeter > maxMeter)
        {
            currentMeter = maxMeter;
        }
    }

    public virtual void RestAll()
    {
        currentHealth = maxHealth;
        currentMeter = maxMeter;
        currentStun = 0;
    }

    public void ResetStun()
    {
        currentStun = 0;
    }

    public bool Stunned()
    {
        return currentStun >= maxStun;
    }

    public bool StaminaEmpty()
    {
        return currentHealth <= 0;
    }

    public bool MeterMaxedOut()
    {
        return currentMeter >= maxMeter;
    }

    public int CurrentHealth()
    {
        return currentHealth;
    }

    public int CurrentMeter()
    {
        return currentMeter;
    }

    public int MaxHealth()
    {
        return maxHealth;
    }

    public int MaxMeter()
    {
        return maxMeter;
    }

    public void SetTest()
    {
        statMeterGain = 1;
        maxHealth = 300;
        maxStun = 100;
        currentHealth = maxHealth;
        currentMeter = 0;
    }
}
