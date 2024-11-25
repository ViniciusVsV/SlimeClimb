using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CheckpointBox : MonoBehaviour{
    [SerializeField] private Vector3 savedPosition;
    [SerializeField] private Quaternion savedRotation;
    [SerializeField] private CinemachineVirtualCamera savedCamera;
    private CheckpointController checkpointController;
    private BoxCollider2D trigger;

    void Start(){
        trigger = GetComponent<BoxCollider2D>();

        checkpointController = FindFirstObjectByType<CheckpointController>();
    }

    private void OnTriggerEnter2D(Collider2D other){    
        if(other.CompareTag("Player") || other.CompareTag("PlayerCopy")){
            checkpointController.SaveTransform(savedPosition, savedRotation, savedCamera);

            trigger.enabled = false;
        }
    }
}