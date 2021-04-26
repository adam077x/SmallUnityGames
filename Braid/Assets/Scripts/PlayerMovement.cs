using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0.0f, 1000.0f)] [SerializeField] private float speed;
    [Range(1.0f, 1000.0f)] [SerializeField] private float jumpStrength;

    [Space]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [Range(0.0f, 1.0f)] [SerializeField] private float checkSize;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Health playerHealth;
    private Animator animator;

    [Space]
    [SerializeField] [Range(0.0f, 2.0f)] private float JumpPressedTime;
    [SerializeField] private float fJumpPressedTime;

    [SerializeField] [Range(0.0f, 2.0f)] private float GroundTouchedTime;
    private float fGroundTouchedTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<Health>();
        animator = GetComponent<Animator>();
        
        fJumpPressedTime = 0;
        fGroundTouchedTime = 0;
    }

    void Update()
    {
        animator.SetBool("Dead", !playerHealth.alive);

        if (playerHealth.alive == false)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        Jump();
    }

    void FixedUpdate()
    {
        if (playerHealth.alive == false) return;

        Move();
    }

    private float timer = 0.0f;
    private void Jump() 
    {
        fJumpPressedTime -= Time.deltaTime;
        fGroundTouchedTime -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            fJumpPressedTime = JumpPressedTime;
            timer = 0.5f;
        }
        if (Input.GetButton("Jump"))
        {
            rb.gravityScale = 1 * 0.75f;
        }
        if(Input.GetButtonUp("Jump"))
        {
            rb.gravityScale = 1;
        }

        if (fGroundTouchedTime > 0 && fJumpPressedTime > 0)
        {
            fJumpPressedTime = 0;
            fGroundTouchedTime = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
        else if (Physics2D.OverlapCircle(groundCheck.position, checkSize, groundMask))
        {
            fGroundTouchedTime = GroundTouchedTime;
        }

        timer -= Time.deltaTime;
    }
    private void Move() 
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);

        if (horizontal > 0)
        {
            sr.flipX = false;
        }
        else if (horizontal < 0)
        {
            sr.flipX = true;
        }

        if (horizontal != 0)
        {
            animator.SetBool("Running", true);
        }
        else 
        {
            animator.SetBool("Running", false);
        }
    }
}
