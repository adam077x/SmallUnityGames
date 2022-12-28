using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!GameStateManager.alive) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position += Vector3.right * horizontal * speed * Time.deltaTime;
        transform.position += Vector3.up * vertical * speed * Time.deltaTime;

        if(vertical > 0 || horizontal > 0) 
        { 
            rb.velocity = rb.velocity * 0.2f;
        }
    }
}
