using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitKnockback : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float originalGravityScale;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        originalGravityScale = rb2D.gravityScale;
    }

    public void ApplyKnockback(Vector2 direction, AttackData attackData)
    {
        Vector2 knockbackForce = new Vector2(direction.x * attackData.knockbackForce.x, attackData.knockbackForce.y);

        rb2D.gravityScale = attackData.gravityScaleKnockback;
        rb2D.AddForce(knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(StopKnockback(attackData.knockbackDuration));
    }

    private IEnumerator StopKnockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0f;

        rb2D.gravityScale = originalGravityScale;
    }
}
