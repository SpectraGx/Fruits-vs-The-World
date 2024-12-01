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
        SetSpeed(1, 0.5f); 
    }

    protected override void Update()
    {
        base.Update();
        bool isMoving = rb2D.velocity.magnitude > 0.1f;
        playerAnimationController.SetIsMoving(isMoving);
    }

    public bool IsMoving(){
        return rb2D.velocity.magnitude > 0.1f;
    }
}
