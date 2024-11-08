using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxAirborne : Hitbox
{
    private void Awake()
    {
        forAirborne = true;
    }
}
