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

    public void PlayEffects(ParticleSystem deathParticles, Vector2 position)
    {
        deathParticles.transform.position = position;
        deathParticles.Play();

        deathCameraShake.ShakeCamera();

        audioController.PlayDeathSound();
    }
}