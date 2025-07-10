using System;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField] private BaseState initialState;
    private Rigidbody2D rb;
    private Animator animator;
    private SlimeStats stats;

    [Header("Actions")]
    [SerializeField] private Divide divideAction;
    [SerializeField] private Merge mergeAction;
    [SerializeField] private Die dieAction;

    [Header("States")]
    [SerializeField] private Idle idleState;
    [SerializeField] private JumpStart jumpStartState;
    [SerializeField] private Jump jumpState;
    [SerializeField] private Land landState;
    [SerializeField] private DivisionRecoil divisionRecoilState;
    [SerializeField] private Inactive inactiveState;
    private StateMachine stateMachine;

    [Header("Input Directions")]
    public Vector2 nextJumpDirection;
    public Vector2 currentJumpDirection;
    public Vector2 divideDirection;

    [Header("Booleans")]
    public bool jumpBuffered;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stats = GetComponent<SlimeStats>();

        stateMachine = new StateMachine();

        idleState.Setup(rb, animator, this, stats);
        jumpStartState.Setup(rb, animator, this, stats);
        jumpState.Setup(rb, animator, this, stats);
        landState.Setup(rb, animator, this, stats);
        divisionRecoilState.Setup(rb, animator, this, stats);
        inactiveState.Setup(rb, animator, this, stats);

        stateMachine.Set(initialState);
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
        if (!divideAction || stateMachine.currentState is Inactive)
            return;

        divideDirection = direction;

        //Remove divisões na diagonal
        if (Mathf.Abs(divideDirection.x) == Math.Abs(divideDirection.y))
            divideDirection = divideDirection.x > 0f ? Vector2.right : Vector2.left;

        divideAction.DivideSlime(divideDirection);

        //Se estiver no estado idle, corrige a posição do slime para grudar na parede
        SetDivisionRecoilState();
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
    public void SetJumpState(bool forced)
    {
        if (!forced && !stateMachine.currentState.isComplete)
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
    public void SetDivisionRecoilState()
    {
        if (!stateMachine.currentState.isComplete)
            return;

        stateMachine.Set(divisionRecoilState, true);
    }
    public void SetInactiveState()
    {
        stateMachine.Set(inactiveState);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
            dieAction.KillSlime();
    }
}