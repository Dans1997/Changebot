using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    public static Transform player;

    [Header("Enemy Variables")]
    [SerializeField] float minRange = .3f;
    [SerializeField] float maxRange = 5f;
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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceToPlayerX = Mathf.Abs(transform.position.x - player.position.x);
        bool previousIsChasing = isChasing;

        if (distanceToPlayerX > minRange && distanceToPlayer < maxRange)
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
        if (!isChasing) return;
        isChasing = false;
        rb.velocity = new Vector2(0,0);
    }

    // See Enemy Range in Editor Mode
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, maxRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}
