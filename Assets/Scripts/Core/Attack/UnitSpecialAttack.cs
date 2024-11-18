using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpecialAttack : MonoBehaviour
{
    private float specialCharge;
    private float maxSpecialCharge = 100f;

    private void Update()
    {
        if (specialCharge < maxSpecialCharge)
        {
            specialCharge += Time.deltaTime * 10f; 
        }
    }

    public void ExecuteSpecialAttack()
    {
        if (specialCharge >= maxSpecialCharge)
        {
            specialCharge = 0;
        }
    }

    public float GetSpecialCharge()
    {
        return specialCharge;
    }
}
