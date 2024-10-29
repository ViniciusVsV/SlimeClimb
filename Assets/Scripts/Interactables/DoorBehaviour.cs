using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private PlayerController playerController;

    void Start(){
        playerController = FindFirstObjectByType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerCopy"))
        {
            if(playerController.hasKey)
                Destroy(gameObject);      
        }
    }
}