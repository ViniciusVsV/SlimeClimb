using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float jumpSpeed;
    private Rigidbody2D rb;

    [Header("Objects")]
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private Animator animator;

    [Header("Division")]
    [SerializeField] GameObject playerCopyPrefab;
    [SerializeField] float sizeLimit;

    [Header("Booleans")]
    public bool isJumping;
    public bool hasKey;
    //Temporário para a primeira build
    public bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleRestart();

        //Temporário
        if (isDead || PauseMenuController.Instance.isPaused)
            return;

        HandleJumping();
        HandleDivision();
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
        AudioController.instance.PlayJumpSound();
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

    void HandleDivision()
    {
        if (transform.localScale.x <= sizeLimit)
            return;

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

        if (launchDirection == Vector2.zero)
            return;

        GameObject newCopy = Instantiate(playerCopyPrefab, transform.position, Quaternion.identity);

        AudioController.instance.PlayCopySound();

        PlayerCopyController playerCopyController = newCopy.GetComponent<PlayerCopyController>();

        transform.localScale -= new Vector3(0.25f, 0.25f);
        newCopy.transform.localScale = transform.localScale;

        StartCoroutine(playerCopyController.JumpStart(launchDirection, rotation, nextRotation));
    }

    //Temporário
    void HandleRestart()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene("Vinicius");
    }

    //Temporário
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            AudioController.instance.PlayDeathSound();
            playerSprite.SetActive(false);

            isDead = true;
        }
    }
}