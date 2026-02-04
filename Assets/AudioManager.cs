using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    
    public AudioClip scoreIncreaseSound;
    public AudioClip deathSound;
    public AudioClip backgroundMusic;

    private void Start()
    {
        // This will play a background song when starting the game
        // musicSource.clip = backgroundMusic;
        // musicSource.Play();
    }

    public void PlaySFX(AudioClip sound)
    {
        SFXSource.PlayOneShot(sound);
    }
}
