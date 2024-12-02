using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : UnitMoveController
{
    [Header("Settings: Move & Radius")]
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float minDistanceToPlayer = 1f;
    private Transform playerTransform;
    private float distanceToPlayer;
    private EnemyAnimationController enemyAnimation;
    private EnemyAttackController enemyAttackController;
    private bool isKnockedBack = false;
    private bool repeat = false;

    protected override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimation = GetComponent<EnemyAnimationController>();
        enemyAttackController = GetComponent<EnemyAttackController>();

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

        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= detectionRadius)
            {
                Vector3 direction = (playerTransform.position - transform.position).normalized;

                if (distanceToPlayer > minDistanceToPlayer)
                {
                    canMove = true;

                    Move(direction, horizontalSpeed, verticalSpeed);
                    enemyAnimation.SetIsMoving(true);
                }
                else if (distanceToPlayer <= minDistanceToPlayer)
                {
                    Move(Vector3.zero, horizontalSpeed, verticalSpeed);
                    enemyAnimation.SetIsMoving(false);
                    Debug.Log("En la distancia min");

                    if (enemyAnimation.GetCurrentState() != "enemy_attack" || enemyAnimation.GetCurrentState() == "enemy_idle")
                    {
                        enemyAnimation.SetAttack1();
                        enemyAttackController.ExecuteNormalAttack();
                        Debug.Log("Sigue atacando");
                    }

                    if (repeat == true)
                    {
                        enemyAnimation.SetAttack1();
                        enemyAttackController.ExecuteNormalAttack();
                    }
                }
            }
            else if (distanceToPlayer > detectionRadius)
            {
                StopMoving();
                canMove = false;
                enemyAnimation.SetIsMoving(false);
            }
        }
    }

    public void RepeatAttack()
    {
        if (distanceToPlayer <= minDistanceToPlayer)
        {
            repeat = true;
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
        canMove = true;
        OnEnable();
    }

    public bool IsMoving()
    {
        return rb2D.velocity.magnitude > 0.1f;
    }
}
