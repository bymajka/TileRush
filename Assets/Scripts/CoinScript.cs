using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] private Collider2D coinCollider;
    [SerializeField] private AudioClip audioCollect;
    [SerializeField] private float coinScore;
    private bool wasCollected;
    public float CoinScore
    {
        get { return coinScore;}
        private set { coinScore = value; }
    }

    private void Start()
    {
        coinCollider = GetComponent<Collider2D>();
        CoinScore = coinScore;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;
            Debug.Log("Touch");
            AudioSource.PlayClipAtPoint(audioCollect, Camera.main.transform.position);
            FindObjectOfType<GameController>().AddScore();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
