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
        ParticleSystem jumpParticles,
        ParticleSystem trailParticles, ParticleSystem slimeParticles
    )
    {
        jumpParticles.Play();
        trailParticles.Play();
        slimeParticles.Stop();

        audioController.PlayJumpSound();
    }
}