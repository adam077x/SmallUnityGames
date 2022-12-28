using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject player;
    [SerializeField] float spawnCounter;

    public static bool spawning = true;

    public IEnumerator StartSpawning()
    {
        while(spawning) { 
            yield return new WaitForSeconds(spawnCounter);

            Vector2 randomPosition = Utils.GetRandomPosition(player.transform.position, 10);

            Instantiate(enemy, randomPosition, Quaternion.identity);
        }
    }

    Coroutine spawningCorutine;
    private bool paused = false;

    void Start()
    {
        spawningCorutine = StartCoroutine(StartSpawning());
    }

    void Update()
    {
        HandleDifficulty();

        if (!GameStateManager.alive && !paused)
        {
            StopCoroutine(spawningCorutine);
            paused = true;
        }

        if(paused)
        {
            spawningCorutine = StartCoroutine(StartSpawning());
            paused = false;
        }
    }

    float diffCounter = 0;
    void HandleDifficulty()
    {
        diffCounter += Time.deltaTime;

        if (diffCounter < spawnCounter) return;
        
        diffCounter = 0;

        if(spawnCounter > 0.75f) spawnCounter -= 0.1f;
    }
}
