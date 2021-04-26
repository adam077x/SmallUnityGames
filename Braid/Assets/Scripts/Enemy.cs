using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollider;
    protected SpriteRenderer spriteRenderer;
    protected EdgeCollider2D edgeCollider;
    protected Health health;

    protected virtual void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        health = GetComponent<Health>();

        health.alive = true;
    }

    public void Die() 
    {
        health.alive = false;
        rb.velocity = Vector2.zero;

        // TODO: Run somekind of animation

        boxCollider.enabled = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && health.alive) 
        {
            Die();
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, 8);
        }
    }
}
