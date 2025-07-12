using UnityEngine;

public class SlimeDeathEffects : MonoBehaviour
{
    private DeathCameraShake deathCameraShake;
    private DeathParticles deathParticles;

    private void Start()
    {
        deathCameraShake = FindFirstObjectByType<DeathCameraShake>();
        deathParticles = FindFirstObjectByType<DeathParticles>();
    }

    public void PlayEffects(Vector2 position, Vector3 scale)
    {
        deathParticles.PlayParticles(position, scale);

        deathCameraShake.ShakeCamera();

        AudioController.Instance.PlayDeathSound();
    }
}