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
        rb.linearVelocity = Vector2.zero;

        stats.mergeTrigger.enabled = false;
        slimeSprite.enabled = false;

        slimeParticles.Stop();
        trailParticles.Stop();

        stats.isDead = true;
        stats.hasMerged = false;

        slimeTransform.SetPositionAndRotation(new Vector3(0f, 2f * stats.GetId(), 0f), Quaternion.Euler(0f, 0f, 30f));
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
        yield return new WaitForSeconds(0.2f);

        stats.mergeTrigger.enabled = true;
    }
}