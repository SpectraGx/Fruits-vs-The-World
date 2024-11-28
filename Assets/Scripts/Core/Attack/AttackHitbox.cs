using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private PlayerAttack playerAttack;

    private void Awake()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();

        if (playerAttack == null)
        {
            Debug.LogError("PlayerAttack no asignado en AttackHitbox");
        }
        else
        {
            Debug.Log("PlayerAttack asignado correctamente en AttackHitbox");
        }
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

}
