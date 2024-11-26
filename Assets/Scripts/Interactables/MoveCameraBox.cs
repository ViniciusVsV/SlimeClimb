using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MoveCameraBox : MonoBehaviour{
    [SerializeField] CinemachineVirtualCamera cam1;
    [SerializeField] CinemachineVirtualCamera cam2;
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera nextCamera;

    void Start(){
        currentCamera = cam1;
        nextCamera = cam2;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("PlayerCopy"))
            ChangeCameras();                    
    }

    void ChangeCameras(){
        CinemachineVirtualCamera aux = currentCamera;

        //Trata o caso em que a câmera ativa é trocada ao resetar no checkpoint
        if(currentCamera.Priority != 10){
            currentCamera = nextCamera;
            nextCamera = aux;
        }

        aux = currentCamera;

        currentCamera = nextCamera;
        nextCamera = aux;

        currentCamera.Priority = 10;
        nextCamera.Priority = 1; 
    }
}