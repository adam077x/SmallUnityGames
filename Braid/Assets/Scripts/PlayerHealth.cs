using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Health health;

    [SerializeField] private Animator blackoutImageAnimator;

    void Start()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        blackoutImageAnimator.SetBool("Alive", health.alive);

        if (blackoutImageAnimator.IsInTransition(0) && !health.alive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
