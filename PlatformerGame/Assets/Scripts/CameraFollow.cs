using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float speed = 0.05f;
    private Vector3 offset = new Vector3(0, 0, -10);

    private void FixedUpdate()
    {
        if (target == null) return;
        
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position + offset, speed);
        transform.position = smoothedPosition;
    }
}
