using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource myAudioSource;

    private GameObject playerGameObject;
    private AudioSource playerAudioSource;

    public AudioClip runningClip;
    public AudioClip bulletExplosion;
    public AudioClip playerHit;

    public bool bulletExploded = false;
    public bool pBulletExploded = false;

    void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerAudioSource = playerGameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (bulletExploded)
        {
            myAudioSource.clip = bulletExplosion;
            myAudioSource.Play();
            bulletExploded = false;
        }
        else if(pBulletExploded)
        {
            myAudioSource.clip = playerHit;
            myAudioSource.Play();
            pBulletExploded = false;
        }
    }
}
