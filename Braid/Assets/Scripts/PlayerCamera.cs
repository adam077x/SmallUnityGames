using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Range(0.0f, 10.0f)] [SerializeField] private float smoothing;
    [SerializeField] private Transform playerTransform;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(new Vector3(transform.position.x, transform.position.y, -10), new Vector3(playerTransform.position.x, playerTransform.position.y, -10), smoothing * Time.fixedDeltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
