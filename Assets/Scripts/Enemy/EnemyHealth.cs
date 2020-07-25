﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHits = 1;

    [Header("SFXs")]
    [SerializeField] AudioClip deathSFX;

    int hitsTaken = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        hitsTaken += damage;
        HandleDeath();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth= other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            hitsTaken++;
            playerHealth.TakeHit();
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        if (hitsTaken < maxHits) return;
        GetComponent<Animator>().SetTrigger("deathTrigger");

        GetComponent<AudioSource>().PlayOneShot(deathSFX, 1f);

        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        if(circleCollider) circleCollider.enabled = false;
        else GetComponent<BoxCollider2D>().enabled = false;

        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
        Destroy(gameObject, 0.5f);
    }
}

