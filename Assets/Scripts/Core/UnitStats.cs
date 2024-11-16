using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    private UnitAttackController unitAttackController;    // Se usa para el aturdimiento
    private float meterDelayTimer;                        // Temporizador antes de que el medidor pueda recargarse
    private int currentStun;                              // Si el aturdimiento llega al máximo, la unidad queda aturdida
    private int currentMeter;                             // Medidor actual para movimientos especiales
    private int currentHealth;                            // Si la salud llega a 0, la unidad está KO

    public byte meterDelay = 3;                           // Retraso del medidor antes de la recarga real
    public byte statMeterGain = 2;                        // Ganancia del medidor, stat * 0.5f
    public int maxMeter = 100;                            // Medidor para realizar movimientos especiales
    public int maxHealth = 100;                           // Salud máxima
    public int maxStun = 20;                              // Aturdimiento máximo
    public bool isPlayer;                                 // Indica si esta unidad es el jugador

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

    public virtual bool TakeDamage(Attack incomingAttack)
    {
        int totalDamage = incomingAttack.Damage();
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

        float immediateStunMultiplier = 1.0f;
        if (!GetComponent<UnitMoveController>().IsGrounded())
        {
            if (incomingAttack.AttributeKnockback() || incomingAttack.AttributeKnockbackFar() ||
                incomingAttack.AttributePopUp())
            {
                immediateStunMultiplier += 0.3f;
            }
        }

        if (incomingAttack.AttributeHeavyStun())
        {
            immediateStunMultiplier += 0.3f;
        }

        currentStun += (int)(totalDamage * immediateStunMultiplier);

        return currentStun >= maxStun;
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
