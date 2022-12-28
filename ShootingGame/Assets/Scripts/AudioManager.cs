using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource playerSrc;
    [SerializeField] AudioSource enemySrc;

    [SerializeField] AudioClip[] explosionClips;
    [SerializeField] AudioClip[] shootingClips;

    public static AudioManager instance;

    void Start()
    {
        instance = this;
    }

    public void PlayExplosion()
    {
        enemySrc.clip = explosionClips[Random.Range(0, explosionClips.Length)];
        enemySrc.Play();
    }

    public void PlayShooting()
    {
        playerSrc.clip = shootingClips[Random.Range(0, shootingClips.Length)];
        playerSrc.volume = 0.1f;
        playerSrc.Play();
    }
}
