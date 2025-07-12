using UnityEngine;

public class SlimeLandEffects : MonoBehaviour
{
    private LandCameraShake landCameraShake;

    private void Start()
    {
        landCameraShake = FindFirstObjectByType<LandCameraShake>();
    }

    public void PlayEffects
    (
        ParticleSystem jumpParticles, ParticleSystem speedParticles,
        ParticleSystem slimeParticles, Vector2 direction, float jumpSpeed
    )
    {
        jumpParticles.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        jumpParticles.Play();

        speedParticles.Stop();
        slimeParticles.Play();

        landCameraShake.ShakeCamera(direction, jumpSpeed);

        AudioController.Instance.PlayLandSound();
    }
}