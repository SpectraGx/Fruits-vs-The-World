using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveController : MonoBehaviour
{
    protected Rigidbody2D rb2D;
    protected Animator animator;
    protected UnitShadow unitShadow;
    protected Vector3 initialGroundedPosition;
    protected Vector2 velocity;
    protected bool canMove;
    protected bool grounded = true;
    protected float gravityScale;
    protected float groundCheckTimer;
    protected float horizontalSpeed;
    protected float verticalSpeed;

    [HideInInspector] public UnitAttackController unitAttackController;
    [HideInInspector] public UnitKnockback unitKnockback;
    [HideInInspector] public UnitStats unitStats;

    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    protected virtual void Awake()
    {
        unitAttackController = GetComponent<UnitAttackController>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        unitShadow = GetComponentInChildren<UnitShadow>();
        unitKnockback = GetComponent<UnitKnockback>();
        unitStats = GetComponent<UnitStats>();
    }

    protected virtual void Start()
    {
        grounded = true;
        initialGroundedPosition = transform.position;
        rb2D.drag = 15f;
        rb2D.gravityScale = 0;
    }

    protected virtual void Update()
    {
        HandleTimers();
        CheckIfGrounded();
        HandleMovement();
    }

    private void HandleTimers()
    {
        if (groundCheckTimer > 0)
        {
            groundCheckTimer -= Time.deltaTime;
        }
    }

    private void CheckIfGrounded()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    protected virtual void HandleMovement()
    {
        if (unitStats.Stunned())
        {

            rb2D.velocity = Vector3.zero;
            return;

        }
        if (grounded)
        {
            if (unitShadow != null)
            {
                //unitShadow.TurnOnShadow();
            }
            rb2D.drag = 15f;
        }
        else
        {
            if (unitShadow != null)
            {
                //unitShadow.TurnOffShadow();
            }
            AdjustDragAndGravity();
        }

        if (canMove)
        {
            AdjustDirection();
        }
    }

    public void Move(Vector2 directionalInput, float horizontalSpeed, float verticalSpeed)
    {
        if (!canMove || unitStats.Stunned())
        {
            return;
        }

        float currentHorizontalSpeed = horizontalSpeed;
        float currentVerticalSpeed = verticalSpeed;
        velocity = new Vector2(currentHorizontalSpeed * directionalInput.x, currentVerticalSpeed * directionalInput.y);
        rb2D.velocity = velocity;
    }

    private void AdjustDragAndGravity()
    {
        if (rb2D.velocity.y > 2f)
        {
            rb2D.drag = 3f;
            gravityScale = 0;
        }
        else if (rb2D.velocity.y <= 2f && rb2D.velocity.y >= -2f)
        {
            rb2D.drag = 3f;
            gravityScale = 0;
        }
        else
        {
            rb2D.drag = 3f;
            gravityScale = 0f;
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
    }

    public bool IsRunning()
    {
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
}
