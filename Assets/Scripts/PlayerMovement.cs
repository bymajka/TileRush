using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = System.Random;

public class PlayerMovement: MonoBehaviour
{
    private Vector2 moveInput;
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private float playerSpeed;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float jumpForceValue;
    [SerializeField] private float additionalJumpForceValue;
    [SerializeField] private float climbSpeed;
    [SerializeField] private Collider2D checkingGround;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletsSpawnDistance;
    private float standartGravityScale;
    private bool isAlive = true;

    private void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        checkingGround = GetComponent<CircleCollider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        standartGravityScale = playerRigidbody2D.gravityScale;
    }

    private void Update()
    {
        if (!isAlive) return;
        Run();
        Flip();
        Climbing();
        Die();
    }

    public void OnMove (InputValue value)
    {
        if (!isAlive) return;
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    
    public void OnJump(InputValue value)
    {
        if (!isAlive) return;
        if (checkingGround.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (value.isPressed)
            {
                playerRigidbody2D.AddForce(new Vector2(0 , jumpForceValue), ForceMode2D.Impulse);
            }
        }
    }

    public void OnFire(InputValue value)
    {
        if (!isAlive) return;
        if (value.isPressed)
        {
            Instantiate(bullet, bulletsSpawnDistance.transform.position, quaternion.identity);
        }
    }
    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, playerRigidbody2D.velocity.y);
        playerRigidbody2D.velocity = playerVelocity;
        bool playerHorizontalSpeed = Mathf.Abs(playerRigidbody2D.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("IsRunning", playerHorizontalSpeed);
    }
    private void Flip()
    {
        bool playerHorizontalSpeed = Mathf.Abs(playerRigidbody2D.velocity.x) > Mathf.Epsilon;
        
        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody2D.velocity.x), 1f);
        }
    }
    private void Climbing()
    {
        if (!checkingGround.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerRigidbody2D.gravityScale = standartGravityScale;
            playerAnimator.SetBool("IsClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(playerRigidbody2D.velocity.x, moveInput.y * climbSpeed);
        playerRigidbody2D.velocity = climbVelocity;
        playerRigidbody2D.gravityScale = 0f;
        
        bool playerVerticalSpeed = Mathf.Abs(playerRigidbody2D.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("IsClimbing", playerVerticalSpeed);
    }
    private void Die()
    {
        if (playerRigidbody2D.IsTouchingLayers(LayerMask.GetMask("Enemies", "Traps")))
        {
            isAlive = false;
            playerRigidbody2D.AddForce(new Vector2(0f, 15f), ForceMode2D.Impulse);
            playerRenderer.color = Color.red;
            playerAnimator.SetTrigger("Dying");
            Destroy(gameObject, 1f);
            FindObjectOfType<GameController>().ProcessPlayerDeath();
        }
    }
}
