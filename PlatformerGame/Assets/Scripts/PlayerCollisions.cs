using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private string acidTag = "Acid";
    private string waterTag = "Water";
    
    private PlayerHealth health;
    private PlayerMovement movement;

    [SerializeField] private LayerMask waterMask;
    [SerializeField] private float waterCheckRadius;
    
    private void Start()
    {
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, waterCheckRadius, waterMask))
        {
            health.drowning = true;
        }
        else
        {
            health.drowning = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(acidTag))
        {
            health.inAcid = true;
        }
        else if (other.CompareTag(waterTag))
        {
            movement.inWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(acidTag))
        {
            health.inAcid = false;
        }
        else if (other.CompareTag(waterTag))
        {
            movement.inWater = false;
        }
    }
}
