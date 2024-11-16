using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    private readonly List<Attack> nextAttacks;
    private readonly Vector2 knockback;
    private readonly Vector2 hitboxDimensions;
    private bool canAddAttack;
    private readonly byte hitType;
    private readonly int damage;
    private readonly int meterCost;
    private readonly int animationID;
    private readonly string attackName;
    private readonly byte attributes;

    public Attack(string attackName, int damage, int meterCost, int animationID,
        float hitboxWidth, float hitboxHeight, byte hitType, float knockbackDistance, float knockbackHeight, string attribute)
    {
        canAddAttack = true;
        nextAttacks = new List<Attack>(2);
        this.damage = damage;
        this.meterCost = meterCost;
        this.animationID = animationID;
        this.attackName = attackName;
        hitboxDimensions = new Vector2(hitboxWidth, hitboxHeight);
        this.hitType = hitType;
        knockback = new Vector2(knockbackDistance, knockbackHeight);
        this.attributes = AddAttributes(attribute);
    }

    public void AddAttack(Attack attack)
    {
        if (canAddAttack)
        {
            nextAttacks.Add(attack);
        }
    }

    public void StopAddingAttacks()
    {
        canAddAttack = false;
        nextAttacks.TrimExcess();
    }

    public bool HasOptions()
    {
        return nextAttacks.Count > 0;
    }

    public bool HasMeterCost()
    {
        return meterCost > 0;
    }

    public bool CanUseMove(int currentMeter)
    {
        return currentMeter >= meterCost;
    }

    public bool AttributeGrab()
    {
        return (attributes & 0x1) == 0x1;
    }

    public bool AttributeHeavyStun()
    {
        return ((attributes >> 1) & 0x1) == 0x1;
    }

    public bool AttributeKnockback()
    {
        return ((attributes >> 2) & 0x1) == 0x1;
    }

    public bool AttributeKnockbackFar()
    {
        return ((attributes >> 3) & 0x1) == 0x1;
    }

    public bool AttributePopUp()
    {
        return ((attributes >> 4) & 0x1) == 0x1;
    }

    public bool AttributeFlip()
    {
        return ((attributes >> 5) & 0x1) == 0x1;
    }

    public byte GetHitType()
    {
        return hitType;
    }

    public int Damage()
    {
        return damage;
    }

    public int MeterCost()
    {
        return meterCost;
    }

    public int GetAnimationID()
    {
        return animationID;
    }

    public string GetName()
    {
        return attackName;
    }

    public Vector2 GetHitboxDimensions()
    {
        return hitboxDimensions;
    }

    public Vector2 GetKnockback()
    {
        return knockback;
    }

    public Attack GetNextAttack(int index)
    {
        return nextAttacks[index];
    }

    public List<Attack> GetNextAttacks()
    {
        return nextAttacks;
    }

    private byte AddAttributes(string attribute)
    {
        if (!canAddAttack)
        {
            return 0;
        }
        if (attribute == null)
        {
            return 0;
        }
        byte currentAttributes = 0;
        if (attribute.Contains("none", System.StringComparison.OrdinalIgnoreCase))
        {
            return 0;
        }
        if (attribute.Contains("heavyStun", System.StringComparison.OrdinalIgnoreCase))
        {
            currentAttributes |= (0x1 << 1);
        }
        if (attribute.Contains("knockback", System.StringComparison.OrdinalIgnoreCase))
        {
            currentAttributes |= (0x1 << 2);
        }
        if (attribute.Contains("knockbackFar", System.StringComparison.OrdinalIgnoreCase))
        {
            currentAttributes |= (0x1 << 3);
        }
        if (attribute.Contains("popUp", System.StringComparison.OrdinalIgnoreCase))
        {
            currentAttributes |= (0x1 << 4);
        }
        if (attribute.Contains("flip", System.StringComparison.OrdinalIgnoreCase))
        {
            currentAttributes |= (0x1 << 5);
        }
        return currentAttributes;
    }
}
