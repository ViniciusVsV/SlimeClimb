using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpBox : MonoBehaviour{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject copyPrefab;
    [SerializeField] private float targetSpeed;
    private BoxCollider2D trigger;
    private PlayerController playerController;
    private CopyController copyController;

    void Start(){
        trigger = GetComponent<BoxCollider2D>();

        playerController = playerPrefab.GetComponent<PlayerController>();
        copyController = copyPrefab.GetComponent<CopyController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("PlayerCopy")){
            playerController.SpeedUp(targetSpeed);
            copyController.SpeedUp(targetSpeed);
        
            SlimeController aux = other.GetComponent<SlimeController>();
            aux.SpeedUp(targetSpeed);

            trigger.enabled = false;
        }
    }
}
