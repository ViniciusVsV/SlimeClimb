using System.Collections;
using UnityEngine;

public class JumpStart : BaseState
{
    [SerializeField] private AnimationClip jumpStartAnimation;
    private float animationDuration;

    private void Awake()
    {
        animationDuration = jumpStartAnimation.length;
    }

    public override void StateEnter()
    {
        animator.Play(jumpStartAnimation.name);

        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        yield return new WaitForSeconds(animationDuration);

        isComplete = true;

        controller.SetJumpState();
    }
}