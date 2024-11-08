using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    protected bool forAirborne;

    /// <summary>
    /// Is this hitbox only meant for Units in the air?
    /// </summary>
    /// <returns></returns>
    public bool ForAirborne()
    {
        return forAirborne;
    }
}
