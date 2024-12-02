using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : UnitMoveController
{
    [Header("Settings: Move & Radius")]
    [SerializeField] private float detectionRadius = 5f;
    private Transform playerTransform;
    private EnemyAnimationController enemyAnimation;
    private bool isKnockback = false;

    protected override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimation = GetComponent<EnemyAnimationController>();

        SetSpeed(1f, 1f);
    }
    public void OnEnable() {
        canMove = true;
        isKnockback = false;
    }

    protected override void Update()
    {
        base.Update();

        if (isKnockback)
        {
            return;
        }

        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            Debug.Log("El jugador esta en el radio");
            canMove = true;
            Move(direction, horizontalSpeed, verticalSpeed);
        }
        else
        {
            StopMoving();
            canMove = false;
            Debug.Log("El jugador NO esta en el radio");
        }
    }

    protected override void HandleMovement()
    {
        base.HandleMovement();

        if (velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (!canMove)
        {
            enemyAnimation.ResetToIdle();
        }
    }

/*
    public void AppplyKnockback (Vector2 force, float duration){
        isKnockback = true;
        rb2D.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(EndKnockback(duration));
    }

    private IEnumerator EndKnockback(float duration){
        yield return new WaitForSeconds(duration);
        isKnockback = false;
        rb2D.velocity = Vector2.zero;
    }
    */
}
