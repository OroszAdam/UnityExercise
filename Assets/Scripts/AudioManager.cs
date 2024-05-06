using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{    
    public static AudioManager Instance;

    public AudioClip collisionSound;
    private AudioSource audioSource;

    void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayCollision()
    {
        audioSource.PlayOneShot(collisionSound);
    }
}
