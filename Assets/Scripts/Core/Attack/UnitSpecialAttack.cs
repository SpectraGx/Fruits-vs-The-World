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
            specialCharge += Time.deltaTime * 10f; // Tasa de carga del ataque especial
        }
    }

    public void ExecuteSpecialAttack()
    {
        if (specialCharge >= maxSpecialCharge)
        {
            // Lógica para ejecutar el ataque especial
            specialCharge = 0;
        }
    }

    public float GetSpecialCharge()
    {
        return specialCharge;
    }
}
