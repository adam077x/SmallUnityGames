using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public Image healthBarImage;
    public int maxHealthBarSize;
    public bool inAcid;
    public bool drowning;
    public int lastestCheckpoint;
    
    [SerializeField] private Image transitionImage;
    
    private void Start()
    {
        health = 1.0f;
        maxHealthBarSize = 100;
        inAcid = false;
        lastestCheckpoint = 0;
    }

    private void Update()
    {
        if (inAcid)
        {
            health -= Time.deltaTime * 2;
        }

        if (!inAcid && health < 1 && !drowning)
        {
            health += Time.deltaTime / 2;
        }

        if (health > 1)
        {
            health = 1;
        }

        if (health <= 0)
        {
            StartCoroutine(Die());
        }

        if (drowning)
        {
            health -= Time.deltaTime / 10;
        }

        healthBarImage.rectTransform.sizeDelta = new Vector2(health * maxHealthBarSize, healthBarImage.rectTransform.sizeDelta.y);
    }

    IEnumerator Die()
    {
        transitionImage.GetComponent<Animator>().SetBool("Die", true);
        yield return new WaitForSeconds(0.5f);
        Checkpoint[] checkpoints = GameObject.FindObjectsOfType<Checkpoint>();
        for (int i = 0; i < checkpoints.Length; i++)
        {
            print(checkpoints[i].checkpointId);
            if (checkpoints[i].checkpointId == lastestCheckpoint)
            {
                checkpoints[i].RespawnOnThisCheckpoint(transform);
            }
        }

        health = 1;
        transitionImage.GetComponent<Animator>().SetBool("Die", false);
    }
}
