using UnityEngine;

public class SlimeLandEffects : MonoBehaviour
{
    private AudioController audioController;
    private LandCameraShake landCameraShake;

    private void Start()
    {
        audioController = FindFirstObjectByType<AudioController>();
        landCameraShake = FindFirstObjectByType<LandCameraShake>();
    }

    public void PlayEffects
    (
        ParticleSystem jumpParticles, ParticleSystem speedParticles,
        ParticleSystem slimeParticles, Vector2 direction, float jumpSpeed
    )
    {
        jumpParticles.Play();
        speedParticles.Stop();
        slimeParticles.Play();

        landCameraShake.ShakeCamera(direction, jumpSpeed);

        audioController.PlayLandSound();
    }
}