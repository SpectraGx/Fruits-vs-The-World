using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyStats enemyStats;
    private EnemyAttackController enemyAttackController;
    private EnemyMovement enemyMovement;

    private void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
        enemyAttackController = GetComponent<EnemyAttackController>();
        enemyMovement = GetComponent<EnemyMovement>();
        if (enemyStats == null)
            Debug.Log("No se ha asignado EnemyStats");
        if (enemyAttackController == null)
            Debug.Log("No se ha asignado EnemyAttackController");
    }

}
