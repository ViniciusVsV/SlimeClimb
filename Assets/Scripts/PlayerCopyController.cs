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

    [Header("Trigger")]
    [SerializeField] private BoxCollider2D triggerCollider;
    [SerializeField] private float triggerThreshold;

    [Header("Objects")]
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private Animator animator;
    private Transform playerPosition;

    public bool isJumping;
    public bool isMerging;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
            Debug.Log("PORRA");

        playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();

        copyId = GameObject.FindGameObjectsWithTag("PlayerCopy").Length;
    }

    void Update()
    {
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

        triggerCollider.enabled = true;

        isJumping = false;

        animator.SetBool("isJumping", false);

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
    }

    void HandleTrigger()
    {
        if (triggerCollider.enabled == true)
            return;

        Vector2 playerDistance = playerPosition.position - transform.position;
        if (playerDistance.magnitude >= triggerThreshold)
            triggerCollider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isMerging)
            return;

        Debug.Log(other);

        isMerging = true;

        if (other.CompareTag("Player"))
        {
            other.transform.localScale += transform.localScale * 0.5f;
            AudioController.instance.PlayMergeSound();
            Destroy(gameObject);
        }

        else if (other.CompareTag("CopyDetector"))
        {
            int otherId = other.GetComponentInParent<PlayerCopyController>().copyId;

            if (copyId < otherId)
            {
                transform.localScale += other.transform.parent.localScale;
                AudioController.instance.PlayMergeSound();
                Destroy(other.transform.parent.gameObject);
            }
        }

        else if (other.CompareTag("Obstacle"))
            AudioController.instance.PlayDeathSound();
        Destroy(gameObject);
    }
}