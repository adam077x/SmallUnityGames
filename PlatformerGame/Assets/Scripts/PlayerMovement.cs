using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask liquidMask;
    
    [SerializeField] private float jumpPressedRememberTime;
    private float jumpPressedRemember;

    [SerializeField] private float groundedRememberTime;
    private float groundedRemember;
    
    private float jumpedRemember;
    
    public bool inWater;
    
    private byte onGround;

    [SerializeField] [Range(0.0f, 1.0f)] private float cutJumpVelocity;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        jumpPressedRemember = 0;
        groundedRemember = 0;
        jumpedRemember = 0;
        
        inWater = false;
    }

    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask))
        {
            onGround = 1;
        }
        else if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, liquidMask))
        {
            onGround = 2;
        }
        else
        {
            onGround = 0;
        }

        groundedRemember -= Time.deltaTime;
        if (onGround == 1)
        {
            groundedRemember = groundedRememberTime;
        }

        jumpPressedRemember -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpPressedRemember = jumpPressedRememberTime;
            
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutJumpVelocity);
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && onGround == 2)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2);
        }

        jumpedRemember -= Time.deltaTime;
        if ((jumpPressedRemember > 0) && (groundedRemember > 0) && (jumpedRemember < 0))
        {
            jumpPressedRemember = 0;
            groundedRemember = 0;
            jumpedRemember = 0.5f;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKey(KeyCode.S) && inWater)
        {
            rb.velocity = new Vector2(rb.velocity.x, -2.5f);
        }
    }
    
    private void OnGUI()
    {
        if(!VariableInspector.renderVariableGui) return;
        
        GUI.Label(new Rect(5, 5, 250, 20), "Grounded Remember Time: " + groundedRemember);
        GUI.Label(new Rect(5, 20, 250, 20), "Jump Remember Time: " + jumpPressedRemember);
        GUI.Label(new Rect(5, 35, 250, 20), "Jumped Remember Time: " + jumpedRemember);
    }
}
