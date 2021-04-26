using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Data 
{
    public bool alive;
    public Vector2 position;

    public Data(Vector2 position, bool alive)
    {
        this.alive = alive;
        this.position = position;
    }

    public Data(Vector2 position)
    {
        this.alive = true;
        this.position = position;
    }
}

public class TimeTraveler : MonoBehaviour
{
    private List<Data> memory = new List<Data>();

    private float time = 0;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private EdgeCollider2D edgeCollider;
    private Health health;

    [SerializeField] private GameObject debugObj;
    [SerializeField] private int fps = 120;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time > 1 / fps && !rewinding) 
        {
            if (health != null)
            {
                memory.Add(new Data(transform.position, health.alive));
            }
            else 
            {
                memory.Add(new Data(transform.position));
            }
            time = 0;
        }

        if (Input.GetKey(KeyCode.R)) 
        {
            Rewind();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            index = memory.Count - 1;
            rewinding = true;
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            rewinding = false;
        }

        rb.simulated = !rewinding;
        boxCollider.enabled = !rewinding;
    }

    int index = 0;
    bool rewinding = false;
    void Rewind() 
    {
        rewinding = true;

        if (rb != null) 
        {
            rb.simulated = false;
        }

        if (index <= 0) 
        {
            index = memory.Count-1;
        }

        if (time > 1 / fps && index >= 0) 
        {
            transform.position = memory[index].position;
            if(health != null) health.alive = memory[index].alive;

            time = 0;
            memory.RemoveAt(index);
            Instantiate(debugObj, transform.position, Quaternion.identity);
            index--;
        }
    }
}
