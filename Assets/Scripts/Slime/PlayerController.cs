using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : SlimeController{
    [Header("Player")]
    [Header("Division")]
    [SerializeField] GameObject playerCopyPrefab;
    [SerializeField] float sizeLimit;
    public bool canDivide;

    protected override void Start(){
        base.Start();

        slimeColor = Color.green;
    }

    protected override void Update(){
        base.Update();
        
        HandleDivision();
    }

    void HandleDivision(){
        if (transform.localScale.x <= sizeLimit || !canDivide)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            Divide(Vector2.up, 0f, 180f);

        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Divide(Vector2.down, 180f, 0f);

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            Divide(Vector2.left, 90f, -90f);

        else if (Input.GetKeyDown(KeyCode.RightArrow))
            Divide(Vector2.right, -90f, 90f);
    }

    void Divide(Vector2 launchDirection, float rotation, float nextRotation){
        GameObject newCopy = Instantiate(playerCopyPrefab, transform.position, Quaternion.identity);

        AudioController.instance.PlayCopySound();

        CopyController copyController = newCopy.GetComponent<CopyController>();

        if(hasLight)
            copyController.transform.GetChild(0).gameObject.SetActive(true);

        transform.localScale -= new Vector3(0.25f, 0.25f);
        newCopy.transform.localScale = transform.localScale;
    
        HandleParticleValues();

        StartCoroutine(copyController.JumpStart(launchDirection, rotation, nextRotation));

        if(isJumping)
            return;
        
        Vector2 moveDirection = Vector2.zero;
        float rotationZ = transform.eulerAngles.z;

        if(rotationZ == 90f){
            moveDirection = Vector2.right;
        }
        else if(rotationZ == 270f){
            moveDirection = Vector2.left;
        }
        else if(rotationZ == 0f){
            moveDirection = Vector2.down;
        }
        else if(rotationZ == 180f){
            moveDirection = Vector2.up;
        }
        
        rb.velocity = moveDirection * jumpSpeed;
    } 

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("CopyDetector"))
            HandleParticleValues();
    }
}