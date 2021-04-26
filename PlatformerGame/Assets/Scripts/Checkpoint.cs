using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointId;
    private string playerTag = "Player";
    
    public void RespawnOnThisCheckpoint(Transform player)
    {
        player.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (checkpointId > other.GetComponent<PlayerHealth>().lastestCheckpoint)
            {
                other.GetComponent<PlayerHealth>().lastestCheckpoint = checkpointId;
            }
        }
    }
}
