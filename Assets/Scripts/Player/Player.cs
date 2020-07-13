using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 5f;

    [Header("Player Jump")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] int maxExtraJumps = 2;
    [SerializeField] float maxJumpTime = 1f;
    [SerializeField] Transform groundCheck = null;
    [SerializeField] float groundCheckRadius = 0f;
    [SerializeField] LayerMask whatIsGround = 0;

    bool isFacingRight = true;
    bool isGrounded = true;
    bool isJumping = false;
    bool isWalking = false;

    int extraJumps;
    float moveInput = 0f;
    float jumpTimeCounter;

    // Cached components
    Rigidbody2D rigidBody = null;
    Animator animator = null;
    SpriteRenderer spriteRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = maxExtraJumps;
        jumpTimeCounter = maxJumpTime;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Physics Related Update
    void FixedUpdate()
    {
        GroundCheck();
        MovePlayer();

        animator.SetBool("isWalking", isWalking);
    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();
    }

    private void GroundCheck() 
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void MovePlayer()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isWalking = !(Mathf.Abs(moveInput) <= Mathf.Epsilon);

        HandleSpriteFlip();
        rigidBody.velocity = new Vector2(moveInput * moveSpeed * Time.deltaTime, rigidBody.velocity.y);
    }

    private void HandleSpriteFlip()
    {
        if (isFacingRight && moveInput < 0) isFacingRight = false;
        else if (!isFacingRight && moveInput > 0) isFacingRight = true;

        spriteRenderer.flipX = !isFacingRight;
    }

    private void HandleJump()
    {
        if (isGrounded) extraJumps = maxExtraJumps;

        Vector2 jumpVector = jumpForce * Vector2.up;

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            rigidBody.velocity = jumpVector;
            extraJumps--;
        } 
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
        {
            isJumping = false;
            jumpTimeCounter = maxJumpTime;
            rigidBody.velocity = jumpVector;
        }

        // Holding Down Space
        if(Input.GetKey(KeyCode.Space))
        {
            if(jumpTimeCounter > 0 && isJumping)
            {
                rigidBody.velocity = jumpVector;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) isJumping = false;
    }
}
