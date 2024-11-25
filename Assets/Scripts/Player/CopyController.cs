using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CopyController : SlimeController{
    [Header("Copy")]
    public int copyId;

    [Header("Trigger")]
    [SerializeField] private BoxCollider2D triggerCollider;
    [SerializeField] private float activateTriggerThreshold;

    [Header("Player")]
    private Transform playerTransform;
    private float distancePlayer;

    [Header("Booleans")]
    public bool isMerging;
    public bool hasMerged;

    protected override void Start(){
        base.Start();
        
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();

        copyId = GameObject.FindGameObjectsWithTag("PlayerCopy").Length;

        slimeColor = new Color(Random.value, Random.value, Random.value, (float) 205 / 255);
        spriteRenderer.color = slimeColor;

        var main = slimeParticles.main;
        main.startColor = slimeColor;

        main = speedParticles.main;
        main.startColor = slimeColor;

        HandleParticleValues();
    }

    protected override void Update(){
        base.Update();

        HandleTrigger();
        isMerging = false;
    }

    public IEnumerator JumpStart(Vector2 direction, float rotation, float nextRotation){
        yield return new WaitForEndOfFrame();

        animator.SetTrigger("jumpCopy");

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotation));

        isJumping = true;

        rb.velocity = direction * jumpSpeed;
        animator.SetBool("isJumping", true);

        speedParticles.Play();
        slimeParticles.Stop();

        while (rb.velocity != Vector2.zero)
            yield return null;

        if(distancePlayer < activateTriggerThreshold)
            MergePlayer();

        triggerCollider.enabled = true;

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

    void HandleTrigger(){
        if (triggerCollider.enabled == true)
            return;

        distancePlayer = Vector2.Distance(playerTransform.position, transform.position);
        if (distancePlayer >= activateTriggerThreshold)
            triggerCollider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other){
        if (isMerging)
            return;

        isMerging = true;

        if (other.CompareTag("Player"))
            MergePlayer();

        else if (other.CompareTag("CopyDetector"))
            MergeCopy(other);
    }

    void MergePlayer(){
        Debug.Log("Tentando fundir com o player. O meu id é: " + copyId);

        Vector3 sizeIncrease;

        if(hasMerged)
            sizeIncrease = new Vector3(0.5f, 0.5f);
        else
            sizeIncrease = new Vector3(0.25f, 0.25f);

        playerTransform.localScale += sizeIncrease;

        AudioController.instance.PlayMergeSound();

        Destroy(gameObject);
    }

    void MergeCopy(Collider2D other){
        Debug.Log("Tentando fundir com uma cópia. O meu id é: " + copyId);

        CopyController otherController = other.GetComponentInParent<CopyController>();
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

        HandleParticleValues();
    }
}