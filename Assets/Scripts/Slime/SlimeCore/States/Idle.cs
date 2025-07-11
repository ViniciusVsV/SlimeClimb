using UnityEngine;

public class Idle : BaseState
{
    [SerializeField] private AnimationClip idleAnimation;

    public override void StateEnter()
    {
        animator.Play(idleAnimation.name);

        isComplete = true;
    }
}