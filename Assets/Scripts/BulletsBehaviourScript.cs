using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BulletsBehaviourScript : MonoBehaviour
{
     [SerializeField] private float bulletSpeed = 5f;
     //[SerializeField] private ParticleSystem boomEffect;
     Rigidbody2D bulletsRigidbody2D;
     private PlayerMovement player;
     private float xSpeed;
     
    void Start()
    {
        bulletsRigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        bulletsRigidbody2D.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        //Instantiate(boomEffect, gameObject.transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}
