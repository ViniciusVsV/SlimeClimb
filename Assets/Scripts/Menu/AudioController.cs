using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public AudioClip landSound;
    public AudioClip textSound;
    public AudioClip checkpointSound;
    public AudioClip backgroundMusic;
    public AudioClip endingMusic;
    public AudioClip tunnelMusic;
    private AudioSource audioSource;

    [Header("UI Clips")]
    public AudioClip buttonPress;

    [Header("Audio Sources")]

    [SerializeField]
    AudioSource musicASource;

    [Header("Audio Mixers")]
    [SerializeField]
    AudioMixer mixer;

    public Slider musicSlider;
    public Slider sfxSlider;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Play na Musica
        musicASource.clip = backgroundMusic;
        musicASource.loop = true;
        musicASource.Play();

        musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
        sfxSlider = GameObject.FindGameObjectWithTag("SFXSlider").GetComponent<Slider>();

        // Load das preferencias
        if (PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("sfxVolume"))
            LoadVolume();

        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        mixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }
    public void PlayLandSound()
    {
        PlaySound(landSound);
    }

    public void PlayTextSound(){
        PlaySound(textSound);
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

    public void PlayCheckpointSound(){
        PlaySound(checkpointSound);
    }

    public void PlayButtonClip()
    {
        PlaySound(buttonPress);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void StopMusic()
    {
        if (musicASource.isPlaying)
        {
            musicASource.Stop();
        }
    }

    public void PlayTunnelMusic()
    {
        if (musicASource.isPlaying)
        {
            musicASource.Stop();
        }

        musicASource.clip = tunnelMusic;
        musicASource.Play();
    }

    public void PlayEndingMusic()
    {
        if (musicASource.isPlaying)
        {
            musicASource.Stop();
        }

        musicASource.clip = endingMusic;
        musicASource.Play();
    }
    // Este método será chamado quando o script for ativado
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;  // Inscreve no evento
    }

    // Este método será chamado quando o script for desativado
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desinscreve do evento
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoad");
        musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
        sfxSlider = GameObject.FindGameObjectWithTag("SFXSlider").GetComponent<Slider>();
    }
}
