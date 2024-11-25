using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SlimeController : MonoBehaviour{
    [Header("Character")]
    [Header("Jumping")]
    [SerializeField] protected float jumpSpeed;
    protected Rigidbody2D rb;

    [Header("Visuals")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Transform jumpParticlesPoint;
    [SerializeField] protected ParticleSystem slimeParticles;
    [SerializeField] protected ParticleSystem speedParticles;
    protected Color slimeColor;

    [Header("Objects")]
    [SerializeField] protected CinemachineImpulseSource impulseSource;
    [SerializeField] protected Animator animator;
    protected PauseMenuController pauseMenuController;

    [Header("Booleans")]
    public bool isJumping;
    public bool hasKey;
    
    protected virtual void Start(){
        rb = GetComponent<Rigidbody2D>();

        pauseMenuController = FindFirstObjectByType<PauseMenuController>();
    }        
    
    protected virtual void Update(){
        if(pauseMenuController.isPaused)
            return;

        HandleJumping();
    }

    void HandleJumping(){
        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(Jump(Vector2.up, 0f, 180f));

        else if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine(Jump(Vector2.down, 180f, 0f));

        else if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(Jump(Vector2.left, 90f, -90f));

        else if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine(Jump(Vector2.right, -90f, 90f));
    } 

    IEnumerator Jump(Vector2 direction, float rotation, float nextRotation){
        if(isJumping)
            yield break;

        animator.SetTrigger("jump");
        yield return new WaitForEndOfFrame();

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump"))
            yield return null;

        AudioController.instance.PlayJumpSound();

        isJumping = true;

        rb.velocity = direction * jumpSpeed;

        animator.SetBool("isJumping", true);

        JumpParticles.Instance.PlayParticles(jumpParticlesPoint.position, transform.localScale, transform.rotation, slimeColor);
        speedParticles.Play();
        slimeParticles.Stop();

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));

        while (rb.velocity != Vector2.zero)
            yield return null;
        
        isJumping = false;
        animator.SetBool("isJumping", false);

        AudioController.instance.PlayLandSound();

        Vector3 impulseDirection = Vector3.zero;
        if(nextRotation == -90f || nextRotation == 90f)
            impulseDirection.x = -0.02f;
        else
            impulseDirection.y = -0.02f;        
        impulseSource.GenerateImpulse(impulseDirection * jumpSpeed / 10);

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));

        JumpParticles.Instance.PlayParticles(jumpParticlesPoint.position, transform.localScale, transform.rotation, slimeColor);
        speedParticles.Stop();
        slimeParticles.Play();
    }

    protected void HandleParticleValues(){
        var main = slimeParticles.main;
        var shape =  slimeParticles.shape;

        var main2 = speedParticles.main;
        var shape2 = speedParticles.shape;

        if(transform.localScale.x == 1f){
            main.startSize = 0.1f;
            shape.radius = 0.53f;
            shape.position = new Vector3(0f, -0.17f, 0f);

            main2.startSize = 0.1f;
            shape2.radius = 0.45f;
        }

        else if(transform.localScale.x == 0.75f){
            main.startSize = 0.06f;
            shape.radius = 0.41f;
            shape.position = new Vector3(0f, -0.14f, 0f);

            main2.startSize = 0.07f;
            shape2.radius = 0.29f;
        }

        else{
            main.startSize = 0.03f;
            shape.radius = 0.29f;
            shape.position = new Vector3(0f, -0.11f, 0f);

            main2.startSize = 0.04f;
            shape2.radius = 0.2f;
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Obstacle")){
            AudioController.instance.PlayDeathSound();

            DeathParticles.Instance.PlayParticles(transform.position, transform.localScale);

            DeathCameraShake.Instance.GenerateImpulse();

            if(gameObject.CompareTag("Player")){
                Destroy(spriteRenderer.gameObject);
                Destroy(slimeParticles.gameObject);
                Destroy(speedParticles.gameObject);

                rb.velocity = Vector2.zero;
                
                enabled = false;
            }
            else
                Destroy(gameObject);
        }
    }
}