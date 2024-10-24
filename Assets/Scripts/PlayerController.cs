using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour{
    public Vector2 initialVelocity;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;

    public bool isJumping;
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();

        rb.velocity = initialVelocity;
    }

    void Update(){
        HandleJumping();
    }

    void HandleJumping(){
        if(!isJumping){
            if(Input.GetKeyDown(KeyCode.W))
                StartCoroutine(Jump(Vector2.up, 0f, 180f));

            else if(Input.GetKeyDown(KeyCode.S))
                StartCoroutine(Jump(Vector2.down, 180f, 0f));

            else if(Input.GetKeyDown(KeyCode.A))
                StartCoroutine(Jump(Vector2.left, 90f, -90f));

            else if(Input.GetKeyDown(KeyCode.D))
                StartCoroutine(Jump(Vector2.right, -90f, 90f));
        }
    }

    IEnumerator Jump(Vector2 direction, float rotation, float nextRotation){
        animator.SetTrigger("jump");

        yield return new WaitForEndOfFrame();

        while(!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump"))
            yield return null;

        rb.velocity = direction * jumpSpeed;
        animator.SetBool("isJumping", true);

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));

        while(rb.velocity != Vector2.zero)
            yield return null;

        animator.SetBool("isJumping", false);
        
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
    }
}