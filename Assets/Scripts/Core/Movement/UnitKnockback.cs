using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitKnockback : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Vector2 velocity;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Knockback(Vector3 attackerPosition, Vector2 knockback, byte hitType, bool grounded)
    {
        float direction = (transform.position.x >= attackerPosition.x) ? 1 : -1;
        if (Mathf.Sign(transform.localScale.x) != (-direction))
        {
            FlipSprite();
        }

        if (grounded)
        {
            ApplyGroundedKnockback(knockback, direction, hitType);
        }
        else
        {
            ApplyAirborneKnockback(knockback, direction);
        }
    }

    private void ApplyGroundedKnockback(Vector2 knockback, float direction, byte hitType)
    {
        if (hitType <= 2)
        {
            knockback.y = 0;
        }
        velocity = new Vector2(direction * knockback.x, Mathf.Abs(knockback.y));
        rb2D.velocity = velocity;
    }

    private void ApplyAirborneKnockback(Vector2 knockback, float direction)
    {
        velocity = new Vector2(direction * knockback.x, Mathf.Abs(knockback.y) * 0.9f);
        rb2D.velocity = velocity;
    }

    private void FlipSprite()
    {
        Vector3 scaleTemp = transform.localScale;
        scaleTemp.x *= -1;
        transform.localScale = scaleTemp;
    }
}
