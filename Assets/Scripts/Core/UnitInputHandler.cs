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
    private PlayerAttack playerAttack;

    private void Awake()
    {
        unitMoveController = GetComponent<UnitMoveController>();
        unitJump = GetComponent<UnitJump>();
        unitAttackController = GetComponent<UnitAttackController>();
        unitBlock = GetComponent<UnitBlock>();
        unitParry = GetComponent<UnitParry>();
        unitSpecialAttack = GetComponent<UnitSpecialAttack>();
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
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
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        /*
        if (context.performed)
        {
            playerMove.Jump();
            Debug.Log("Se presionó Space");
        }
        */
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //unitAttackController.ExecuteAttack(unitAttackController.normalAttack);
            playerAttack.ExecuteNormalAttack();
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //unitSpecialAttack.ExecuteSpecialAttack();
            playerAttack.ExecuteSpecialAttack();
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

    public void OnStopMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            unitMoveController.StopMoving();
        }
    }
}
