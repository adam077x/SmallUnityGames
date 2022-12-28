using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody2D rb;

    public float startLifeTime;

    float currentLifeTime;

    void Start()
    {
        currentLifeTime = startLifeTime;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleDeath();
    }

    void HandleDeath()
    {
        if(currentLifeTime <= 0) 
        {
            Destroy(gameObject);
            return;
        }

        currentLifeTime -= Time.deltaTime;

        float radius = (currentLifeTime / startLifeTime) * 0.2f + 0.1f;

        transform.localScale = new Vector2(radius, radius);
    }
}
