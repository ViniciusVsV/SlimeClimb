using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour{
    [SerializeField] private bool takesKey;

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player") || other.CompareTag("PlayerCopy")){
            PlayerController playerController = FindFirstObjectByType<PlayerController>();
         
            if(playerController.hasKey){
                AudioController.instance.PlayDoorSound();   

                if(takesKey)
                    playerController.hasKey = false;

                gameObject.SetActive(false);
            }
        }
    }
}