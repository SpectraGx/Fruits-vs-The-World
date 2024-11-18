using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyStats enemyStats;
    private EnemyAttackController enemyAttackController;

    private void Awake() {
        enemyStats = GetComponent<EnemyStats>();
        enemyAttackController = GetComponent<EnemyAttackController> ();
        if (enemyStats == null) 
            Debug.Log("No se ha asignado EnemyStats");
        if (enemyAttackController == null)
            Debug.Log("No se ha asignado EnemyAttackController");
    }
}
