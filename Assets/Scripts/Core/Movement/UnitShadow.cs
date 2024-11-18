using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShadow : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }


    public void TurnOnShadow()
    {
        sprite.enabled = true;
    }

    public void TurnOffShadow()
    {
        sprite.enabled = false;
    }
}
