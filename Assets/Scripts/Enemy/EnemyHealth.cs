using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHits = 1;

    [Header("SFXs")]
    [SerializeField] AudioClip deathSFX;

    int hitsTaken = 0;
    bool isDead = false;

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        hitsTaken += damage;
        HandleDeath();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
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
        isDead = true;
        GetComponent<Animator>().SetTrigger("deathTrigger");

        GetComponent<AudioSource>().PlayOneShot(deathSFX, 1f);

        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        if(circleCollider) circleCollider.enabled = false;
        
        GetComponent<BoxCollider2D>().enabled = false;

        GetComponent<Rigidbody2D>().isKinematic = true;
        Destroy(gameObject, 0.5f);
    }
}

