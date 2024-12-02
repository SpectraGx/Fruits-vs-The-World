using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : UnitMoveController
{
    [Header("Settings: Move & Radius")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float minDistanceToPlayer = 1f;
    private Transform playerTransform;
    private EnemyAnimationController enemyAnimation;
    private bool isKnockedBack = false;

    protected override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimation = GetComponent<EnemyAnimationController>();

        SetSpeed(1f, 1f);
    }

    public void OnEnable()
    {
        canMove = true;
        isKnockedBack = false;
    }

    protected override void Update()
    {
        base.Update();

        if (isKnockedBack)
        {
            return;
        }

        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer > minDistanceToPlayer)
            {
                Move(direction, horizontalSpeed, verticalSpeed);
            }
            else
            {
                Move(Vector3.zero, horizontalSpeed, verticalSpeed);
            }

            enemyAnimation.SetIsMoving(true);

        }
        else
        {
            StopMoving();
            canMove = false;
            enemyAnimation.SetIsMoving(false);
        }
    }

    public void ApplyKnockback(Vector2 force, float duration)
    {
        isKnockedBack = true;
        rb2D.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(EndKnockback(duration));
    }

    private IEnumerator EndKnockback(float duration)
    {
        yield return new WaitForSeconds(duration);
        isKnockedBack = false;
        rb2D.velocity = Vector2.zero;
        canMove = true;  // Asegurarse de que canMove se restaure correctamente
        OnEnable();
    }

}
