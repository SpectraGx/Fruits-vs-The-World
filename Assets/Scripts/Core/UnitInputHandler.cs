using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitInputHandler : MonoBehaviour
{
    private UnitMoveController unitMoveController;
    private UnitJump unitJump;
    private UnitAttackController unitAttackController;
    private UnitBlock unitBlock;
    private UnitParry unitParry;
    private UnitSpecialAttack unitSpecialAttack;
    private Vector2 moveInput;

    private PlayerMove playerMove;

    private void Awake()
    {
        unitMoveController = GetComponent<UnitMoveController>();
        unitJump = GetComponent<UnitJump>();
        unitAttackController = GetComponent<UnitAttackController>();
        unitBlock = GetComponent<UnitBlock>();
        unitParry = GetComponent<UnitParry>();
        unitSpecialAttack = GetComponent<UnitSpecialAttack>();
        playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        playerMove.Move(moveInput, 2f, 1f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Se presionó una tecla de movimiento");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerMove.Jump();
            Debug.Log("Se presionó Space");
        }
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            unitAttackController.ExecuteAttack(unitAttackController.lightAttack);
        }
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            unitAttackController.ExecuteAttack(unitAttackController.heavyAttack);
        }
    }

    public void OnAerialAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            unitAttackController.ExecuteAttack(unitAttackController.aerialAttack);
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            unitSpecialAttack.ExecuteSpecialAttack();
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            unitBlock.SetBlocking(true);
        }
        else if (context.canceled)
        {
            unitBlock.SetBlocking(false);
        }
    }

    public void OnParry(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            unitParry.ExecuteParry();
        }
    }

    public void OnStopMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            unitMoveController.StopMoving();
        }
    }
}
