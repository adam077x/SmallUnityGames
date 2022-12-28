using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathParticle;

    GameObject player;

    Rigidbody2D rb;

    ScoreTimer scoreTimer;

    void Start()
    {
        player = GameObject.Find("Player");

        rb = GetComponent<Rigidbody2D>();

        scoreTimer = GameObject.Find("ScoreManager").GetComponent<ScoreTimer>();
    }

    void Update()
    {
        if (!GameStateManager.alive) return;

        HandleMovement();
    }


    void HandleMovement()
    {
        float angle = Utils.AngleBetweenPoints(transform.position, player.transform.position);

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;

        rb.velocity = -transform.right * 8;
    }

    void HandleDeath(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Bullet")) return;

        AudioManager.instance.PlayExplosion();
        Die();

        Destroy(collision.collider.gameObject); // Destroys bullet
    }

    public void Die()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        ScreenShake.StartShaking();
        Destroy(gameObject);

        scoreTimer.IncrementScore();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleDeath(collision);
    }
}
