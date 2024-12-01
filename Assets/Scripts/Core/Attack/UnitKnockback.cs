using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitKnockback : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 direction, Vector2 force, float duration)
    {
        Vector2 knockbackForce = direction * force;
        rb2D.AddForce(knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(StopKnockback(duration));
    }

    private IEnumerator StopKnockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        rb2D.velocity = Vector2.zero;
        rb2D.angularVelocity = 0f;
    }
}
