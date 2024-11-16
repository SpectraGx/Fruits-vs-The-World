using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitParry : MonoBehaviour
{
    private bool canParry;
    private bool parrying;

    public void SetCanParry(bool value)
    {
        canParry = value;
    }

    public void ExecuteParry()
    {
        if (canParry)
        {
            parrying = true;
            // Lógica adicional para el parry
        }
    }

    public void EndParry()
    {
        parrying = false;
    }

    public bool IsParrying()
    {
        return parrying;
    }
}
