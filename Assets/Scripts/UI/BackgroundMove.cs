using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private Vector2 speedMove;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D playerRb2D;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        playerRb2D = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        offset = (playerRb2D.velocity.x * 0.1f) * speedMove * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
