using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : Enemy
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool left = false;
    [SerializeField] [Range(0.0f, 5.0f)] private float range;

    private float speed = 5.0f;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (!health.alive) return;

        rb.velocity = new Vector2(speed * (left ? -1 : 1), rb.velocity.y);

        RaycastHit2D ray = Physics2D.Raycast(transform.position, new Vector2(rb.velocity.x, 0), range, layerMask);

        spriteRenderer.flipX = left;

        if (ray.collider != null && ray.collider.CompareTag("Wall")) 
        {
            left = !left;
        }

        if (rb.velocity.x != 0)
        {
            GetComponent<Animator>().SetBool("Running", true);
        }
        else 
        {
            GetComponent<Animator>().SetBool("Running", false);
        }
    }
}
