using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBlock : MonoBehaviour
{
    private bool blocking;

    public void SetBlocking(bool value)
    {
        blocking = value;
    }

    public bool IsBlocking()
    {
        return blocking;
    }
}
