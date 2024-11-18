using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationLayers : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMovementLayer()
    {
        animator.SetLayerWeight(0, 1);
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(2, 0);
        animator.SetLayerWeight(3, 0);
    }

    public void SetHitLayer()
    {
        animator.SetLayerWeight(0, 0);
        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(2, 0);
        animator.SetLayerWeight(3, 0);
    }

    public void SetAttackLayer()
    {
        animator.SetLayerWeight(0, 0);
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(2, 1);
        animator.SetLayerWeight(3, 0);
    }

    public void SetStunnedLayer()
    {
        animator.SetLayerWeight(0, 0);
        animator.SetLayerWeight(1, 0);
        animator.SetLayerWeight(2, 0);
        animator.SetLayerWeight(3, 1);
    }
}
