using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private BoxCollider2D hitboxCollider;

    private void Awake()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();
        hitboxCollider = GetComponent<BoxCollider2D>();

        if (playerAttack == null)
        {
            Debug.LogError("PlayerAttack no asignado en AttackHitbox");
        }
        else
        {
            Debug.Log("PlayerAttack asignado correctamente en AttackHitbox");
        }

        if (hitboxCollider == null)
        {
            Debug.LogError("BoxCollider2D no asignado en AttackHitbox");
        }
        else
        {
            Debug.Log("BoxCollider2D asignado correctamente en AttackHitbox");
        }

        // Desactivar hitbox al inicio
        hitboxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                playerAttack.ApplyDamage(enemyStats);
            }
        }
    }

    public void Active()
    {
        hitboxCollider.enabled = true;
    }

    public void Desactive()
    {
        hitboxCollider.enabled = false;
    }
}
