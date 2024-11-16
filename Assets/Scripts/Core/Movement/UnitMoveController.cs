using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveController : MonoBehaviour
{
    protected Rigidbody2D rb2D;
    protected Animator animator;
    protected UnitShadow unitShadow;
    protected UnitAnimationLayers unitAnimationLayers;
    protected Vector3 initialGroundedPosition;
    protected Vector2 velocity;
    protected Vector2 velocityRef;
    protected bool canMove;
    protected bool grounded;
    protected int gravityScale;
    protected float groundCheckTimer;
    protected float horizontalSpeed;
    protected float verticalSpeed;
    protected float jumpHeight;

    [HideInInspector] public UnitJump unitJump;
    [HideInInspector] public UnitAttackController unitAttackController;
    [HideInInspector] public UnitKnockback unitKnockback;

    protected virtual void Awake()
    {
        unitAnimationLayers = GetComponent<UnitAnimationLayers>();
        unitAttackController = GetComponent<UnitAttackController>();
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
        //HandleAnimations();
        HandleMovement();
    }

    private void HandleTimers()
    {
        if (groundCheckTimer > 0)
        {
            groundCheckTimer -= Time.deltaTime;
        }
    }

/*
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

    */

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

        if (unitAttackController != null && !unitAttackController.CurrentlyAttacking() && !unitAttackController.IsStunned() && canMove)
        {
            AdjustDirection();
        }
    }

    public void Move(Vector2 directionalInput, float horizontalSpeed, float verticalSpeed)
    {
        if (!canMove || unitAttackController.CurrentlyAttacking() || unitAttackController.IsStunned())
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

    public void SetSpeed(float horizontalSpeed, float verticalSpeed)
    {
        this.horizontalSpeed = horizontalSpeed;
        this.verticalSpeed = verticalSpeed;
    }

    public void SetJumpHeight(float jumpHeight)
    {
        this.jumpHeight = jumpHeight;
        //unitJump.SetJumpHeight(jumpHeight); // Asumiendo que UnitJump tiene un método SetJumpHeight
    }
}
