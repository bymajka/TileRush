using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D enemyRigidbody2D;
    [SerializeField] private Collider2D enemyCollider2D;
    [SerializeField] private float enemyMoveSpeed;
    [SerializeField] private Collider2D chekingObstacle;

    private void Awake()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        enemyCollider2D = GetComponent<Collider2D>();
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        enemyRigidbody2D.velocity = new Vector2(enemyMoveSpeed, 0);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        enemyMoveSpeed = -enemyMoveSpeed;
        Flip();
    }
    private void Flip()
    {
        enemyRigidbody2D.transform.localScale = new Vector2(transform.localScale.x * -1f, 1f);
    }
}
