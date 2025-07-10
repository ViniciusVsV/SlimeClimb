using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Land : BaseState
{
    [SerializeField] private AnimationClip landAnimation;
    private float animationDuration;

    [SerializeField] private Transform slimeTransform;

    [SerializeField] private ParticleSystem landParticles;
    [SerializeField] private ParticleSystem trailParticles;
    [SerializeField] private ParticleSystem slimeParticles;

    Quaternion newRotation;

    public UnityEvent<LandArguments> land;

    private void Awake()
    {
        animationDuration = landAnimation.length;
    }

    public override void StateEnter()
    {
        (_, newRotation) = GetNewRotations(controller.currentJumpDirection);

        //Rotaciona o slime
        slimeTransform.rotation = newRotation;

        //Dá play na animação
        animator.Play(landAnimation.name);

        //Invoca o evento de aterrisagem
        LandArguments landArguments = new LandArguments
        {
            landParticles = landParticles,
            trailParticles = trailParticles,
            slimeParticles = slimeParticles,
            direction = controller.currentJumpDirection,
            jumpSpeed = 10f
        };

        land.Invoke(landArguments);

        //Inicia a corrotina
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(animationDuration);

        isComplete = true;

        if (controller.jumpBuffered)
            controller.SetJumpState();
        else
            controller.SetIdleState();
    }
}

public struct LandArguments
{
    public ParticleSystem landParticles;
    public ParticleSystem trailParticles;
    public ParticleSystem slimeParticles;
    public Vector2 direction;
    public float jumpSpeed;
}