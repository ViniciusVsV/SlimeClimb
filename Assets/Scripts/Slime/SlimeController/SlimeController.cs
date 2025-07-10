using System;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Actions")]
    [SerializeField] private Divide divideAction;
    [SerializeField] private Merge mergeAction;

    [Header("States")]
    [SerializeField] private Idle idleState;
    [SerializeField] private JumpStart jumpStartState;
    [SerializeField] private Jump jumpState;
    [SerializeField] private Land landState;
    [SerializeField] private Death deathState;
    private StateMachine stateMachine;

    [Header("Jump")]
    public Vector2 nextJumpDirection;
    public Vector2 currentJumpDirection;

    [Header("Divide")]
    public Vector2 divideDirection;

    [Header("Booleans")]
    public bool jumpBuffered;
    public bool isCopy;
    public bool isInactive;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine();

        idleState.Setup(rb, animator, this);
        jumpStartState.Setup(rb, animator, this);
        jumpState.Setup(rb, animator, this);
        landState.Setup(rb, animator, this);
        deathState.Setup(rb, animator, this);

        stateMachine.Set(idleState);
    }
    public void Jump(Vector2 direction)
    {
        nextJumpDirection = direction;

        //Remove movimentos na diagonal
        if (Mathf.Abs(nextJumpDirection.x) == Math.Abs(nextJumpDirection.y))
            nextJumpDirection = nextJumpDirection.x > 0f ? Vector2.right : Vector2.left;

        SetJumpStartState();
    }
    public void Divide(Vector2 direction)
    {
        if (isCopy || !divideAction)
            return;

        divideDirection = direction;

        //Remove divisões na diagonal
        if (Mathf.Abs(divideDirection.x) == Math.Abs(divideDirection.y))
            divideDirection = divideDirection.x > 0f ? Vector2.right : Vector2.left;

        divideAction.DivideSlime(divideDirection);
    }

    public void SetIdleState()
    {
        if (!stateMachine.currentState.isComplete)
            return;

        stateMachine.Set(idleState);
    }
    private void SetJumpStartState()
    {
        if (!stateMachine.currentState.isComplete)
        {
            jumpBuffered = true;
            return;
        }

        stateMachine.Set(jumpStartState);
    }
    public void SetJumpState()
    {
        if (!stateMachine.currentState.isComplete)
            return;

        jumpBuffered = false;

        currentJumpDirection = nextJumpDirection;

        stateMachine.Set(jumpState);
    }
    public void SetLandState()
    {
        if (!stateMachine.currentState.isComplete)
            return;

        stateMachine.Set(landState);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
            stateMachine.Set(deathState);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CopyDetector"))
            mergeAction.MergeSlimes(gameObject, other.gameObject);
    }
}