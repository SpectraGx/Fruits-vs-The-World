using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;
    private UnitShadow unitShadow;
    private UnitAnimationLayers unitAnimationLayers;
    private Vector3 initialGroundedPosition;
    private Vector2 velocity;
    private Vector2 velocityRef;
    private bool canMove;
    private bool grounded;
    private int gravityScale;
    private float groundCheckTimer;

    [SerializeField] private UnitJump unitJump;
    [SerializeField] private UnitAttackController unitAttackController; // Cambiar referencia a UnitAttackController
    [SerializeField] private UnitKnockback unitKnockback;

    protected virtual void Awake()
    {
        unitAnimationLayers = GetComponent<UnitAnimationLayers>();
        unitAttackController = GetComponent<UnitAttackController>(); // Cambiar referencia a UnitAttackController
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        unitShadow = GetComponentInChildren<UnitShadow>();
        unitJump = GetComponent<UnitJump>();
        unitKnockback = GetComponent<UnitKnockback>();
    }

    protected virtual void Start()
    {
        grounded = true;
        initialGroundedPosition = transform.position;
        rb2D.drag = 15f;
        gravityScale = 12;
    }

    protected virtual void Update()
    {
        HandleTimers();
        HandleAnimations();
        HandleMovement();
    }

    private void HandleTimers()
    {
        if (groundCheckTimer > 0)
        {
            groundCheckTimer -= Time.deltaTime;
        }
    }

    private void HandleAnimations()
    {
        if (animator != null)
        {
            animator.SetBool("Grounded", grounded);
            animator.SetFloat("VelocityX", rb2D.velocity.x);
            animator.SetFloat("VelocityY", rb2D.velocity.y);
            animator.SetFloat("VelocityAll", rb2D.velocity.magnitude);
        }
    }

    private void HandleMovement()
    {
        if (grounded)
        {
            if (unitShadow != null)
            {
                unitShadow.TurnOnShadow();
            }
            rb2D.drag = 15f;
        }
        else
        {
            if (unitShadow != null)
            {
                unitShadow.TurnOffShadow();
            }
            AdjustDragAndGravity();
        }

        if (unitAttackController != null && !unitAttackController.CurrentlyAttacking() && !unitAttackController.IsStunned() && canMove) // Cambiar a UnitAttackController
        {
            AdjustDirection();
        }
    }

    public void Move(Vector2 directionalInput, float horizontalSpeed, float verticalSpeed)
    {
        if (!canMove || unitAttackController.CurrentlyAttacking() || unitAttackController.IsStunned()) // Cambiar a UnitAttackController
        {
            return;
        }

        float currentHorizontalSpeed = IsRunning() ? horizontalSpeed * 1.8f : horizontalSpeed;
        float currentVerticalSpeed = IsRunning() ? verticalSpeed * 1.5f : verticalSpeed;
        velocity = new Vector2(currentHorizontalSpeed * directionalInput.x, currentVerticalSpeed * directionalInput.y);
        rb2D.velocity = velocity;
    }

    private void AdjustDragAndGravity()
    {
        if (rb2D.velocity.y > 2f)
        {
            rb2D.drag = 3f;
            gravityScale = 12;
        }
        else if (rb2D.velocity.y <= 2f && rb2D.velocity.y >= -2f)
        {
            rb2D.drag = 3f;
            gravityScale = 4;
        }
        else
        {
            rb2D.drag = 6f;
            gravityScale = 12;
        }
        rb2D.gravityScale = gravityScale;
    }

    private void AdjustDirection()
    {
        if (velocity.x > 0)
        {
            Vector3 scaleTemp = transform.localScale;
            scaleTemp.x = 1;
            transform.localScale = scaleTemp;
        }
        else if (velocity.x < 0)
        {
            Vector3 scaleTemp = transform.localScale;
            scaleTemp.x = -1;
            transform.localScale = scaleTemp;
        }
    }

    public void StopMoving()
    {
        if (grounded)
        {
            velocity = Vector2.zero;
            rb2D.velocity = velocity;
        }
    }

    public void CanMove(bool tOrF)
    {
        canMove = tOrF;
    }

    public void SetMoveSmoothing(bool tOrF)
    {
        // Logic for move smoothing can be handled here
    }

    public bool IsRunning()
    {
        // Implement logic to determine if the unit is running
        return false;
    }

    public bool IsGrounded()
    {
        return grounded;
    }
}
