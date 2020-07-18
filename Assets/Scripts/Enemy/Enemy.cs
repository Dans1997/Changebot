using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Transform player;
	[SerializeField] float range;
	[SerializeField] float moveSpeed;
	Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < range)
        {
            FollowPlayer();
    	} 
    	else 
    	{
            StopFollowingPlayer();
    	}
    }

    void FollowPlayer()
    {
    	if (transform.position.x < player.position.x)
    	{
    		rb.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
    	} 
    	else
    	{
    		rb.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
    	}
    }

    void StopFollowingPlayer()
    {
        rb.velocity = new Vector2(0,0);
    }
}
