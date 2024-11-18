using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        SetSpeed(1, 0.5f); // Configure the player's speed
        SetJumpHeight(10); // Configure the player's jump height
    }

    protected override void Update()
    {
        base.Update();
        bool isMoving = rb2D.velocity.magnitude>0.1f;
        playerAnimationController.SetIsMoving(isMoving);
        // Additional player-specific update logic can go here
    }
    
    public void Jump()
    {
        /*
        unitJump.Jump(Vector2.zero, 8f); // Usar la velocidad horizontal configurada
        Debug.Log("Active el metodo Jump de PlayerMove");
        */
    }    
}
