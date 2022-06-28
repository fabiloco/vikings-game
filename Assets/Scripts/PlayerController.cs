using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private float moveInput;
    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;
    
    private void Start()
    {
        this.extraJumps = this.extraJumpsValue;
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGrounded)
        {
            this.extraJumps = this.extraJumpsValue;
            print("is grounded");
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * this.jumpForce;
            --this.extraJumps;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * this.jumpForce;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, this.whatIsGround);
        
        this.moveInput = Input.GetAxisRaw("Horizontal");
        this.rb.velocity = new Vector2(this.moveInput * this.speed, rb.velocity.y);

        if (!this.facingRight && moveInput > 0)
            this.Flip();
        else if (this.facingRight && moveInput < 0)
            this.Flip();
    }

    void Flip()
    {
        this.facingRight = !this.facingRight;
        Vector3 scaler = this.transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
