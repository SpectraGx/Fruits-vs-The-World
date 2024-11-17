using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitJump : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Vector3 initialGroundedPosition;
    private bool grounded;
    private float jumpHeight;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0; 
    }

    public void Jump(Vector2 directionalInput, float horizontalSpeed)
    {
        if (grounded)
        {
            Vector2 velocity = new Vector2(Mathf.Clamp(directionalInput.x, -1, 1) * horizontalSpeed, jumpHeight);
            MakeJump(velocity);
        }
    }

    private void MakeJump(Vector2 velocity)
    {
        initialGroundedPosition = transform.position;
        grounded = false;
        rb2D.gravityScale = 0.5f; // Activar la gravedad durante el salto
        rb2D.velocity = velocity;
        StartCoroutine(HandleLanding());
    }

    private IEnumerator HandleLanding()
    {
        while (true)
        {
            if (rb2D.velocity.y <= 0 && transform.position.y <= initialGroundedPosition.y)
            {
                rb2D.gravityScale = 0; // Desactivar la gravedad al aterrizar
                rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
                transform.position = new Vector2(transform.position.x, initialGroundedPosition.y); // Asegurarse de que vuelva a la posición inicial
                grounded = true;
                break;
            }
            yield return null;
        }
    }

    public void SetJumpHeight(float height)
    {
        jumpHeight = height;
    }
}
