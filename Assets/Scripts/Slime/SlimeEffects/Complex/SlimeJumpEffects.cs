using Unity.VisualScripting;
using UnityEngine;

public class SlimeJumpEffects : MonoBehaviour
{
    private AudioController audioController;
    
    private void Start()
    {
        audioController = FindFirstObjectByType<AudioController>();
    }

    public void PlayEffects
    (
        ParticleSystem jumpParticles, Quaternion rotation,
        ParticleSystem trailParticles, ParticleSystem slimeParticles
    )
    {
        jumpParticles.transform.localRotation = rotation;
        jumpParticles.Play();

        trailParticles.Play();
        slimeParticles.Stop();

        audioController.PlayJumpSound();
    }
}