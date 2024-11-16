using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script for controlling the player's movement, inheriting from UnitMoveController.
/// </summary>
public class PlayerMove : UnitMoveController
{
    private UnitInputHandler unitInputHandler;
    private PlayerAnimationController playerAnimationController;

    protected override void Awake()
    {
        base.Awake();
        unitInputHandler = GetComponent<UnitInputHandler>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
    }

    protected override void Start()
    {
        base.Start();
        CanMove(true);
        SetSpeed(8, 4); // Configure the player's speed
        SetJumpHeight(10); // Configure the player's jump height
    }

    protected override void Update()
    {
        base.Update();
        bool isMoving = rb2D.velocity.magnitude>0.1f;
        // Additional player-specific update logic can go here
    }
    
    public void Jump()
    {
        unitJump.Jump(Vector2.zero, 8f); // Usar la velocidad horizontal configurada
    }
}
