using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] int maxExtraJumps = 2;
    [SerializeField] float maxJumpTime = 1f;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0f;
    [SerializeField] LayerMask whatIsGround;

    bool isGrounded = true;
    bool isJumping = false;

    int extraJumps;
    float moveInput = 0f;
    float jumpTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = maxExtraJumps;
        jumpTimeCounter = maxJumpTime;
    }

    // Physics Related Update
    void FixedUpdate()
    {
        GroundCheck();
        MovePlayer();
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
        float speedX = moveInput * moveSpeed * Time.deltaTime;
        GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void HandleJump()
    {
        if (isGrounded) extraJumps = maxExtraJumps;

        Vector2 jumpVector = jumpForce * Vector2.up;

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            GetComponent<Rigidbody2D>().velocity = jumpVector;
            extraJumps--;
        } 
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
        {
            isJumping = false;
            jumpTimeCounter = maxJumpTime;
            GetComponent<Rigidbody2D>().velocity = jumpVector;
        }

        // Holding Down Space
        if(Input.GetKey(KeyCode.Space))
        {
            if(jumpTimeCounter > 0 && isJumping)
            {
                GetComponent<Rigidbody2D>().velocity = jumpVector;
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
