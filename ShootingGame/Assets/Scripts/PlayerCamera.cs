using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    [Range(0f, 2f)] [SerializeField] private float smoothing;

    Vector3 velocity;
    Vector3 offset = new Vector3(0f, 0f, -10f);

    void Update()
    {
        Vector3 playerPosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, playerPosition, ref velocity, smoothing);
    }
}
