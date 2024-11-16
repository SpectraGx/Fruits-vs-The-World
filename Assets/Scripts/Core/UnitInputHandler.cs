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
    private float jumpInput;

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

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        //unitMoveController.Move(moveInput, 5f, 5f); // Velocidades de ejemplo
        playerMove.Move(moveInput, 8f, 4f);
        Debug.Log("Se presiono una tecla de movimiento");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpInput = context.ReadValue<float>();
            //unitJump.Jump(moveInput, 5f, 10f); // Velocidades de ejemplo
            playerMove.Jump();
            Debug.Log("Se presiono Space");
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
