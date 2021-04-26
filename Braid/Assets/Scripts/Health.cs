using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool alive;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private EdgeCollider2D edgeCollider;
    private TimeTraveler timeTraveler;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        timeTraveler = GetComponent<TimeTraveler>();
    }

    void Update()
    {
        boxCollider.enabled = alive;
        edgeCollider.enabled = alive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            alive = false;
            boxCollider.enabled = false;
            edgeCollider.enabled = false;
        }
    }
}
