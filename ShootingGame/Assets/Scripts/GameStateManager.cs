using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static bool alive = false;

    [SerializeField] TextMeshProUGUI playText;

    void Start()
    {
        
    }

    void Update()
    {
        CheckAlive();
    }

    void CheckAlive()
    {
        playText.gameObject.SetActive(!alive);

        if(!alive && Input.GetMouseButton(0))
        {
            alive = true;
            SceneManager.LoadScene(0);
        }
    }

    public static void Die()
    {
        alive = false;

        ScoreTimer.scoreTimer.Reset();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Die();
        }
    }
}
