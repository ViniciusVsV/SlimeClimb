using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MoveCameraBox : MonoBehaviour{
    [SerializeField] CinemachineVirtualCamera cam1;
    [SerializeField] CinemachineVirtualCamera cam2;
    [SerializeField] private float transitionDuration;
    private CinemachineVirtualCamera currentCamera;
    private CinemachineVirtualCamera nextCamera;
    private CinemachineBrain cinemachineBrain;

    void Start(){
        currentCamera = cam1;
        nextCamera = cam2;

        cinemachineBrain = GameObject.FindWithTag("MainCamera").GetComponent<CinemachineBrain>();

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

        cinemachineBrain.m_DefaultBlend.m_Time = transitionDuration;

        aux = currentCamera;

        currentCamera = nextCamera;
        nextCamera = aux;

        currentCamera.Priority = 10;
        nextCamera.Priority = 1; 
    }
}