using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitJump : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Vector3 initialGroundedPosition;
    private bool grounded;
    private int gravityScale;
    private Vector2 velocity;
    private float jumpHeight;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Jump(Vector2 directionalInput, float horizontalSpeed)
    {
        if (grounded)
        {
            velocity = new Vector2(Mathf.Clamp(directionalInput.x, -1, 1) * horizontalSpeed, jumpHeight);
            MakeJump(velocity, true);
        }
    }

    private void MakeJump(Vector2 velocity, bool wasGrounded)
    {
        if (wasGrounded)
        {
            initialGroundedPosition = transform.position;
        }
        rb2D.velocity = Vector2.zero;
        grounded = false;
        rb2D.gravityScale = gravityScale;
        rb2D.velocity = velocity;
    }

    public void SetJumpHeight(float height)
    {
        jumpHeight = height;
    }
}
