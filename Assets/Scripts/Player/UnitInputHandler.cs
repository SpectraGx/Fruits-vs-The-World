using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitInputHandler : MonoBehaviour
{
    private UnitMoveController unitMoveController;
    private UnitAttackController unitAttackController;
    private UnitSpecialAttack unitSpecialAttack;
    private Vector2 moveInput;

    private PlayerMove playerMove;
    private PlayerAttack playerAttack;

    private void Awake()
    {
        unitMoveController = GetComponent<UnitMoveController>();
        unitAttackController = GetComponent<UnitAttackController>();
        unitSpecialAttack = GetComponent<UnitSpecialAttack>();
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void FixedUpdate()
    {
        playerMove.Move(moveInput, 2f, 1f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerAttack.ExecuteNormalAttack();
            playerAttack.ActivateHitbox();
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerAttack.ExecuteSpecialAttack();
            Debug.Log("Se ha hecho un ataque especial");
        }
    }
}
