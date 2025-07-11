using UnityEngine;

public class SlimeDeathEffects : MonoBehaviour
{
    private AudioController audioController;
    private DeathCameraShake deathCameraShake;
    private DeathParticles deathParticles;

    private void Start()
    {
        audioController = FindFirstObjectByType<AudioController>();
        deathCameraShake = FindFirstObjectByType<DeathCameraShake>();
        deathParticles = FindFirstObjectByType<DeathParticles>();
    }

    public void PlayEffects(Vector2 position, Vector3 scale)
    {
        deathParticles.PlayParticles(position, scale);

        deathCameraShake.ShakeCamera();

        audioController.PlayDeathSound();
    }
}