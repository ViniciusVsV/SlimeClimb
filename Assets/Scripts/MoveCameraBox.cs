using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MoveCameraBox : MonoBehaviour{
    [SerializeField] CinemachineVirtualCamera cam1;
    [SerializeField] CinemachineVirtualCamera cam2;
    CinemachineVirtualCamera currentCamera;
    CinemachineVirtualCamera nextCamera;

    void Start(){
        currentCamera = cam1;
        nextCamera = cam2;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            CinemachineVirtualCamera aux = currentCamera;

            currentCamera.Priority = 1;

            currentCamera = nextCamera;

            nextCamera = aux;

            currentCamera.Priority = 10;            
        }
    }
}