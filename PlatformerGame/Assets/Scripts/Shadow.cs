using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private GameObject shadow;

    [SerializeField] private Vector2 offset;
    [SerializeField] private Color color;

    private void Start()
    {
        shadow = new GameObject(gameObject.name + "_Shadow");
        shadow.transform.localScale = transform.localScale;
        shadow.transform.rotation = transform.rotation;
        shadow.transform.parent = transform;
        shadow.AddComponent<SpriteRenderer>();
        shadow.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        shadow.GetComponent<SpriteRenderer>().color = color;
    }

    private void Update()
    {
        shadow.transform.position = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z + 1);
    }
}
