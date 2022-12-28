using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameStateManager.alive) return;

        if (!collision.collider.CompareTag("Enemy")) return;

        //SceneManager.LoadScene(0); // TODO: Create death screen later

        ScreenShake.StartShaking();
        GameStateManager.Die();
    }
}
