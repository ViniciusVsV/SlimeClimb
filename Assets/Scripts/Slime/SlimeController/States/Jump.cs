using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Jump : BaseState
{
    [SerializeField] private AnimationClip jumpAnimation;
    [SerializeField] private float baseJumpSpeed;
    public float currentJumpSpeed;
    private Vector2 jumpDirection;
    private Quaternion newJumpRotation, newLandRotation;

    [SerializeField] private Transform slimeTransform;

    [SerializeField] private ParticleSystem jumpParticles;
    [SerializeField] private ParticleSystem trailParticles;
    [SerializeField] private ParticleSystem slimeParticles;

    public UnityEvent<ParticleSystem, ParticleSystem, ParticleSystem> jump;

    private void Awake()
    {
        currentJumpSpeed = baseJumpSpeed;
    }

    public override void StateEnter()
    {
        jumpDirection = controller.currentJumpDirection;

        //Verifica se o pulo é inconsequente
        (newJumpRotation, newLandRotation) = GetNewRotations(jumpDirection);

        if (Quaternion.Angle(newLandRotation, slimeTransform.rotation) < 0.1f)
        {
            isComplete = true;

            controller.SetIdleState();

            return;
        }

        //Invoca o evento de pulo
        jump.Invoke(jumpParticles, trailParticles, slimeParticles);

        //Rotaciona o slime
        slimeTransform.rotation = newJumpRotation;

        //Dá play na animação
        animator.Play(jumpAnimation.name);

        //Aplica a velocidade no rigidBody
        rb.linearVelocity = jumpDirection * currentJumpSpeed;

        //Inicia corrotina
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        while (rb.linearVelocity != Vector2.zero)
            yield return null;

        isComplete = true;

        controller.SetLandState();
    }
}