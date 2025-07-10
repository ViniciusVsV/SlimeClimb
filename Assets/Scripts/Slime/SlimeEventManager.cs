using UnityEngine;

public class SlimeEventManager : MonoBehaviour
{
    private SlimeDeathEffects slimeDeathEffects;
    private SlimeDivideEffects slimeDivideEffects;
    private SlimeJumpEffects slimeJumpEffects;
    private SlimeLandEffects slimeLandEffects;

    private void Start()
    {
        slimeDeathEffects = FindFirstObjectByType<SlimeDeathEffects>();
        slimeDivideEffects = FindFirstObjectByType<SlimeDivideEffects>();
        slimeJumpEffects = FindFirstObjectByType<SlimeJumpEffects>();
        slimeLandEffects = FindFirstObjectByType<SlimeLandEffects>();
    }

    public void Jump(ParticleSystem jumpParticles, ParticleSystem trailParticles, ParticleSystem slimeParticles)
    {
        slimeJumpEffects.PlayEffects(jumpParticles, trailParticles, slimeParticles);

        CounterController.Instance.IncreaseJumps();
    }

    public void Land(LandArguments landArguments)
    {
        slimeLandEffects.PlayEffects
        (
            landArguments.landParticles,
            landArguments.trailParticles,
            landArguments.slimeParticles,
            landArguments.direction,
            landArguments.jumpSpeed
        );
    }

    public void Divide()
    {
        slimeDivideEffects.PlayEffects();

        CounterController.Instance.IncreaseDivisions();
    }

    public void Die(ParticleSystem deathParticles)
    {
        slimeDeathEffects.PlayEffects(deathParticles);

        CounterController.Instance.IncreaseDeaths();
    }
}