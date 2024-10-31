using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerCopyController : MonoBehaviour
{
    public int copyId;

    [Header("Movement")]
    [SerializeField] private float jumpSpeed;
    private Rigidbody2D rb;
    private float distancePlayer;

    [Header("Trigger")]
    [SerializeField] private BoxCollider2D triggerCollider;
    [SerializeField] private float triggerThreshold;

    [Header("Objects")]
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private Animator animator;
    private Transform playerPosition;
    private GameObject player;

    public bool isJumping;
    public bool isMerging;
    public bool hasMerged;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
            Debug.Log("PORRA");

        player = GameObject.FindWithTag("Player");
        playerPosition = player.GetComponent<Transform>();

        copyId = GameObject.FindGameObjectsWithTag("PlayerCopy").Length;
    }

    void Update()
    {
        if(PauseMenuController.Instance.isPaused)
            return;

        HandleJumping();
        HandleTrigger();

        isMerging = false;
    }

    void HandleJumping()
    {
        if (isJumping)
            return;

        if (Input.GetKeyDown(KeyCode.W))
            StartCoroutine(Jump(Vector2.up, 0f, 180f));

        else if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine(Jump(Vector2.down, 180f, 0f));

        else if (Input.GetKeyDown(KeyCode.A))
            StartCoroutine(Jump(Vector2.left, 90f, -90f));

        else if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine(Jump(Vector2.right, -90f, 90f));
    }
    IEnumerator Jump(Vector2 direction, float rotation, float nextRotation)
    {
        animator.SetTrigger("jump");

        yield return new WaitForEndOfFrame();

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump"))
            yield return null;

        isJumping = true;

        rb.velocity = direction * jumpSpeed;
        animator.SetBool("isJumping", true);

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));

        while (rb.velocity != Vector2.zero)
            yield return null;

        isJumping = false;
        AudioController.instance.PlayLandSound();
        animator.SetBool("isJumping", false);

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
    }

    public IEnumerator JumpStart(Vector2 direction, float rotation, float nextRotation)
    {
        yield return new WaitForEndOfFrame();

        animator.SetTrigger("jumpCopy");

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));

        isJumping = true;

        rb.velocity = direction * jumpSpeed;
        animator.SetBool("isJumping", true);

        while (rb.velocity != Vector2.zero)
            yield return null;

        if(distancePlayer < triggerThreshold)
            MergePlayer();

        triggerCollider.enabled = true;

        isJumping = false;
        AudioController.instance.PlayLandSound();

        animator.SetBool("isJumping", false);

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
    }

    void HandleTrigger()
    {
        if (triggerCollider.enabled == true)
            return;

        distancePlayer = Vector2.Distance(playerPosition.position, transform.position);
        if (distancePlayer >= triggerThreshold)
            triggerCollider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isMerging)
            return;

        isMerging = true;

        if (other.CompareTag("Player"))
            MergePlayer();

        else if (other.CompareTag("CopyDetector"))
            MergeCopy(other);

        else if (other.CompareTag("Obstacle"))
        {
            AudioController.instance.PlayDeathSound();

            Destroy(gameObject);
        }
    }

    /*void OnTriggerStay2D(Collider2D other)
    {
        if (isMerging)
            return;

        isMerging = true;

        if (other.CompareTag("Player"))
            MergePlayer();

        else if (other.CompareTag("CopyDetector"))
            MergeCopy(other);

        else if (other.CompareTag("Obstacle"))
        {
            AudioController.instance.PlayDeathSound();

            Destroy(gameObject);
        }
    }*/

    void MergePlayer()
    {
        Debug.Log("Tentando fundir com o player. O meu id é: " + copyId);

        Vector3 sizeIncrease;

        if(hasMerged)
            sizeIncrease = new Vector3(0.5f, 0.5f);
        else
            sizeIncrease = new Vector3(0.25f, 0.25f);

        playerPosition.localScale += sizeIncrease;

        AudioController.instance.PlayMergeSound();

        Destroy(gameObject);
    }

    void MergeCopy(Collider2D other)
    {
        Debug.Log("Tentando fundir com uma cópia. O meu id é: " + copyId);

        PlayerCopyController otherController = other.GetComponentInParent<PlayerCopyController>();
        int otherId = otherController.copyId;

        if (copyId < otherId)
        {
            transform.localScale += new Vector3(0.125f, 0.125f);
            
            AudioController.instance.PlayMergeSound();

            Destroy(other.transform.parent.gameObject);

            hasMerged = true;
        }

        else if (copyId > otherId)
        {
            other.transform.parent.localScale += new Vector3(0.125f, 0.125f);

            AudioController.instance.PlayMergeSound();

            Destroy(gameObject);

            otherController.hasMerged = true;   
        }
    }
}