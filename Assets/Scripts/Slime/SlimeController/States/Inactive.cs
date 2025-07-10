using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Inactive : BaseState
{
    [SerializeField] private Transform slimeTransform;
    [SerializeField] private SpriteRenderer slimeSprite;
    [SerializeField] private ParticleSystem trailParticles;
    [SerializeField] private ParticleSystem slimeParticles;

    public override void StateEnter()
    {
        stats.mergeTrigger.enabled = false;
        slimeSprite.enabled = false;

        slimeParticles.Stop();
        trailParticles.Stop();

        stats.isDead = true;

        StartCoroutine(EnterRoutine());
    }

    private IEnumerator EnterRoutine()
    {
        yield return new WaitForSeconds(1);

        slimeTransform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0f, 0f, 30f));
    }

    public override void StateExit()
    {
        slimeSprite.enabled = true;
        slimeParticles.Play();

        stats.isDead = false;

        StartCoroutine(ExitRoutine());
    }

    private IEnumerator ExitRoutine()
    {
        while (rb.linearVelocity != Vector2.zero)
            yield return null;

        stats.mergeTrigger.enabled = true;
    }
}