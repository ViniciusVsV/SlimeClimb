using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    private PlayerController playerController;

    void Start(){
        playerController = FindFirstObjectByType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerCopy"))
        {
            playerController.hasKey = true;
                
            Destroy(gameObject);     
        }
    }
}