using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour{
    private void OnTriggerEnter2D(Collider2D other){
        /*if (other.CompareTag("Player") || other.CompareTag("PlayerCopy")){
            PlayerController playerController = FindFirstObjectByType<PlayerController>();
            playerController.hasKey = true;
            
            AudioController.instance.PlayCollectSound();
            
            gameObject.SetActive(false);   
        }*/
    }
}