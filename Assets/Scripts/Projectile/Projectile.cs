using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Specs")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] int damage = 1;

    [Header("Hit SFX")]
    [SerializeField] AudioClip hitSFX;

    // Cached Components 
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Fire();
        Destroy(gameObject, lifeTime);
    }

    //TODO: Make projectile hit whatever 
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (!enemyHealth) return;
        enemyHealth.TakeDamage(damage);
        if(hitSFX) AudioSource.PlayClipAtPoint(hitSFX, transform.position);
        Destroy(gameObject);
    }

    private void Fire()
    {
        bool isShooterFacingRight = FindObjectOfType<Player>().IsFacingRight();
        spriteRenderer.flipX = !isShooterFacingRight;
        Vector2 force = isShooterFacingRight ? Vector2.right * projectileSpeed : Vector2.right * -projectileSpeed;

        rigidBody.AddForce(force);
    }
}
