using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected Animator animator;
    protected Rigidbody2D rb;
    protected SlimeController controller;
    protected SlimeStats stats;

    public void Initialise()
    {
        isComplete = false;
    }

    public void Setup(Rigidbody2D rb, Animator animator, SlimeController controller, SlimeStats stats)
    {
        this.rb = rb;
        this.animator = animator;
        this.controller = controller;
        this.stats = stats;
    }

    public virtual void StateEnter() { }
    public virtual void StateUpdate() { }
    public virtual void StateFixedUpdate() { }
    public virtual void StateExit() { }

    protected (Quaternion, Quaternion) GetNewRotations(Vector2 direction)
    {
        Vector3 jumpRotation;
        Vector3 landRotation;

        if (direction == Vector2.up)
        {
            jumpRotation = new Vector3(0f, 0f, 0f);
            landRotation = new Vector3(0f, 0f, 180f);
        }

        else if (direction == Vector2.down)
        {
            jumpRotation = new Vector3(0f, 0f, 180f);
            landRotation = new Vector3(0f, 0f, 0f);
        }

        else if (direction == Vector2.left)
        {
            jumpRotation = new Vector3(0f, 0f, 90f);
            landRotation = new Vector3(0f, 0f, -90f);
        }

        else
        {
            jumpRotation = new Vector3(0f, 0f, -90f);
            landRotation = new Vector3(0f, 0f, 90f);
        }

        return (Quaternion.Euler(jumpRotation), Quaternion.Euler(landRotation));
    }
}