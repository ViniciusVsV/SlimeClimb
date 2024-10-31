using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip collectSound;
    public AudioClip portalSound;
    public AudioClip doorSound;
    public AudioClip copySound;
    public AudioClip mergeSound;
    private AudioSource audioSource;

    void Awake()
    {
        // Implement the singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep this object between scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Ensure an AudioSource component is attached
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

    public void PlayDeathSound()
    {
        PlaySound(deathSound);
    }

    public void PlayCollectSound()
    {
        PlaySound(collectSound);
    }

    public void PlayPortalSound()
    {
        PlaySound(portalSound);
    }

    public void PlayDoorSound()
    {
        PlaySound(doorSound);
    }

    public void PlayCopySound()
    {
        PlaySound(copySound);
    }

    public void PlayMergeSound()
    {
        PlaySound(mergeSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
