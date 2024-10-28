using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerCopyController : MonoBehaviour{
    [Header("Movement")]
    [SerializeField] private float jumpSpeed;
    private Rigidbody2D rb;

    [Header("Objects")]
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D playerDetector;
    private GameObject player;
    private PlayerController playerController;

    public bool isJumping;
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        if(!rb)
            Debug.Log("PORRA");

        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void Update(){
        HandleJumping();
    }

    void HandleJumping(){
        if(isJumping)
            return;

        if(Input.GetKeyDown(KeyCode.W))
            StartCoroutine(Jump(Vector2.up, 0f, 180f));

        else if(Input.GetKeyDown(KeyCode.S))
            StartCoroutine(Jump(Vector2.down, 180f, 0f));

        else if(Input.GetKeyDown(KeyCode.A))
            StartCoroutine(Jump(Vector2.left, 90f, -90f));

        else if(Input.GetKeyDown(KeyCode.D))
            StartCoroutine(Jump(Vector2.right, -90f, 90f));
    }

    public IEnumerator JumpStart(Vector2 direction, float rotation, float nextRotation){
        yield return new WaitForEndOfFrame();

        animator.SetTrigger("jumpCopy");

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));

        isJumping = true;

        rb.velocity = direction * jumpSpeed;
        animator.SetBool("isJumping", true);

        while(rb.velocity != Vector2.zero)
            yield return null;

        playerDetector.enabled = true;

        isJumping = false;

        animator.SetBool("isJumping", false);
        
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
    }

    IEnumerator Jump(Vector2 direction, float rotation, float nextRotation){
        animator.SetTrigger("jump");

        yield return new WaitForEndOfFrame();

        while(!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump"))
            yield return null;

        isJumping = true;

        rb.velocity = direction * jumpSpeed;
        animator.SetBool("isJumping", true);

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));

        while(rb.velocity != Vector2.zero)
            yield return null;

        isJumping = false;

        animator.SetBool("isJumping", false);
        
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") || other.CompareTag("PlayerCopy")){
            Destroy(gameObject);

            playerController.numDivisions--;
            other.transform.localScale *= 2;
        }
    }
}