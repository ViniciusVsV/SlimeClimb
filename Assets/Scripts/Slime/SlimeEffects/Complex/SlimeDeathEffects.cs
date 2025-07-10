using UnityEngine;

public class SlimeDeathEffects : MonoBehaviour
{
    private AudioController audioController;
    private DeathCameraShake deathCameraShake;

    private void Start()
    {
        audioController = FindFirstObjectByType<AudioController>();
        deathCameraShake = FindFirstObjectByType<DeathCameraShake>();
    }

    public void PlayEffects(ParticleSystem deathParticles)
    {
        deathParticles.Play();
        deathCameraShake.ShakeCamera();

        audioController.PlayDeathSound();
    }
}