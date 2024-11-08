using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : UnitStats
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        SetTest();
    }
}
