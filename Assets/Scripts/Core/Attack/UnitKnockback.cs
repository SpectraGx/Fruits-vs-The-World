using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitKnockback : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float originalGravityScale;
    [SerializeField] private float gravityScaleKnockback;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        originalGravityScale = rb2D.gravityScale;
    }

    public void ApplyKnockback(Vector2 direction, Vector2 force, float duration)
    {
        Vector2 knockbackForce = new Vector2(direction.x * force.x, force.y);

        rb2D.gravityScale = gravityScaleKnockback;
        rb2D.AddForce(knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(StopKnockback(duration));
    }

    private IEnumerator StopKnockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0f;

        rb2D.gravityScale = originalGravityScale;
    }
}
