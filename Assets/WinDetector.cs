using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDetector : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            gameManager.Win();
        }
    }
}
