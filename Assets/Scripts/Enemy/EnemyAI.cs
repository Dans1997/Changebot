﻿using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    public static Transform player;

    [Header("Enemy Variables")]
    [SerializeField] float range;
	[SerializeField] float moveSpeed;

    [Header("SFXs")]
    [SerializeField] AudioClip engageSFX;
    [SerializeField] AudioClip disEngageSFX;
    //[SerializeField] AudioClip damageSFX;

    // State
    bool isChasing = false;

    // Cached Components
	Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        bool previousIsChasing = isChasing;

        if (distanceToPlayer < range)
        {
            FollowPlayer();
        } 
    	else 
    	{
            StopFollowingPlayer();
        }

        // Handle Animation
        if(animator) animator.SetBool("isChasing", isChasing);

        // Handle SFXs
        if (isChasing && !previousIsChasing) if(engageSFX) audioSource.PlayOneShot(engageSFX);
        else if (!isChasing && previousIsChasing) if (disEngageSFX) audioSource.PlayOneShot(disEngageSFX);
    }

    private void FollowPlayer()
    {
        isChasing = true;

        if (transform.position.x < player.position.x) rb.velocity = new Vector2(moveSpeed, 0);
        else rb.velocity = new Vector2(-moveSpeed, 0);

        spriteRenderer.flipX = !(transform.position.x < player.position.x);
    }

    private void StopFollowingPlayer()
    {
        isChasing = false;
        rb.velocity = new Vector2(0,0);
    }

    // See Enemy Range in Editor Mode
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
