using Unity.VisualScripting;
using UnityEngine;

public class SlimeJumpEffects : MonoBehaviour
{
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

        AudioController.Instance.PlayJumpSound();
    }
}