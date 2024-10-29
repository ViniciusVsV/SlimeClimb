using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool hasKey = false;
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float jumpSpeed;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private Animator animator;

    [Header("Division")]
    [SerializeField] GameObject playerCopyPrefab;
    [SerializeField] float sizeLimit;

    public bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleJumping();
        HandleDivision();  
    }

    // Verifica se o player tem a chave
    public bool HasKey()
    {
        return hasKey;
    }

    // Método para coletar a chave
    public void CollectKey()
    {
        hasKey = true;
        
    }

    void HandleJumping()
    {
        if (isJumping) return;

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

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));

        while (rb.velocity != Vector2.zero)
            yield return null;

        isJumping = false;
        animator.SetBool("isJumping", false);

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
        playerSprite.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, nextRotation));
    }

    void HandleDivision()
    {
        if (transform.localScale.x <= sizeLimit) return;

        Vector2 launchDirection = Vector2.zero;
        float rotation = 0f;
        float nextRotation = 0f;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            launchDirection = Vector2.up;
            rotation = 0f;
            nextRotation = 180f;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            launchDirection = Vector2.down;
            rotation = 180f;
            nextRotation = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            launchDirection = Vector2.left;
            rotation = 90f;
            nextRotation = -90f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            launchDirection = Vector2.right;
            rotation = -90f;
            nextRotation = 90f;
        }

        if (launchDirection == Vector2.zero) return;

        GameObject newCopy = Instantiate(playerCopyPrefab, transform.position, Quaternion.identity);
        PlayerCopyController playerCopyController = newCopy.GetComponent<PlayerCopyController>();

        transform.localScale /= 1.5f;
        newCopy.transform.localScale = transform.localScale;

        StartCoroutine(playerCopyController.JumpStart(launchDirection, rotation, nextRotation));
    }
}
