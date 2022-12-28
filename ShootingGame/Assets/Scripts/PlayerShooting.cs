using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject bulletSpawner;

    Rigidbody2D rb;
    [SerializeField] Animator animator;

    public float fireRate;
    float fireRateTime = 0;

    Vector3 playerKnockback = new Vector3(0, 0, 0);
    Vector3 playerVelocity = new Vector3(0, 0, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GunTowardsMouse();

        if (!GameStateManager.alive) return;
        
        Shooting();

        ApplyKnockback();
    }

    void FixedUpdate()
    {
        rb.AddForce(-rb.velocity * 1.5f);
    }

    void Shooting()
    {
        bool leftMouse = Input.GetMouseButton(0);

        if (leftMouse) Shoot();
    }

    void Shoot()
    {
        fireRateTime -= Time.deltaTime;

        //if(fireRateTime <= 0) animator.SetBool("Shooting", false);

        if (fireRateTime > 0) return; 

        Quaternion rotation = bulletSpawner.transform.rotation;
        rotation.z += Random.Range(-0.2f, 0.2f);

        Quaternion oldRotation = bulletSpawner.transform.rotation;
        bulletSpawner.transform.rotation = rotation;

        GameObject bulletInstance = Instantiate(bullet, bulletSpawner.transform.position, rotation);
        bulletInstance.GetComponent<Rigidbody2D>().AddForce(bulletSpawner.transform.up * 10, ForceMode2D.Impulse);

        bulletSpawner.transform.rotation = oldRotation;

        fireRateTime = fireRate;

        playerKnockback = transform.right * 2;

        //transform.position += playerKnockback / 5;
        //playerVelocity = playerKnockback / 50;
        rb.AddForce(playerKnockback, ForceMode2D.Impulse);

        AudioManager.instance.PlayShooting();

        //animator.SetBool("Shooting", true);
    }

    void ApplyKnockback()
    {
        /*transform.position += playerVelocity;

        if(playerVelocity.x < 0 || playerVelocity.y < 0)
        {
            playerVelocity.x = 0;
            playerVelocity.y = 0;
        }
        else
        {
            playerVelocity -= Vector3.one / 5;
        }*/
    }

    void GunTowardsMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Utils.AngleBetweenPoints(transform.position, direction);

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = rotation;
    }
}
