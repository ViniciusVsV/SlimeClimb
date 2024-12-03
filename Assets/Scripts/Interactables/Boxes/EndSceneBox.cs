using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class EndSceneBox : MonoBehaviour{
    [SerializeField] private GameObject cam;
    [SerializeField] private float maxShakeIntensity;
    [SerializeField] private float minCameraSize;
    [SerializeField] private float maxSpeed;

    private SlimeController[] slimes;
    private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noise;
    private Rigidbody2D playerRb;

    private float initialCameraSize;
    private float initialSpeed;

    public bool stop;
    public bool hasActivated;

    void Start(){
        vcam = cam.GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        stop = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("PlayerCopy")){
            SlimeController slimeController = other.GetComponent<SlimeController>();
            playerRb = other.GetComponent<Rigidbody2D>();

            if(stop)
                stop = false;
            else
                stop = true;

            if(!hasActivated)
                StartCoroutine(EndCutscene(other.gameObject, slimeController));
        }
    }   

    IEnumerator EndCutscene(GameObject other, SlimeController slimeController){
        hasActivated = true;

        //Pegar valores iniciais
        initialCameraSize = vcam.m_Lens.OrthographicSize;
        initialSpeed = playerRb.velocity.magnitude;

        //bloquear os inputs do player
        slimes = FindObjectsOfType<SlimeController>();
        foreach(SlimeController slime in slimes)
            slime.inputsBlocked = true;

        //câmera começa a ir para cima junto com o player
        Rigidbody2D camRb = cam.GetComponent<Rigidbody2D>();
        camRb.velocity = Vector2.up * initialSpeed;

        float progress = 0f;
        while(!stop){
            progress = Mathf.Clamp01(progress + Time.deltaTime * 0.2f);

            //aumentar gradativamente a velocidade do jogador
            float newSpeed = Mathf.Lerp(initialSpeed, maxSpeed, progress);
            playerRb.velocity = Vector2.up * newSpeed;
            camRb.velocity = Vector2.up * newSpeed;

            if(progress >= 0.1f){
                //reduzir gradativamente o tamanho da câmera
                float newSize = Mathf.Lerp(initialCameraSize, minCameraSize, (progress - 0.2f) / 0.8f);
                vcam.m_Lens.OrthographicSize = newSize;
            }
            
            if(progress >= 0.3f){
                //aumentar gradativamente o tremor de tela
                float newIntensity = Mathf.Lerp(0, maxShakeIntensity, (progress - 0.3f) / 0.7f);
                noise.m_AmplitudeGain = newIntensity;
            }

            yield return null;
        }

        //zerar parâmetros
        camRb.velocity = Vector2.zero;

        //Aplicar maior tremor da câmera
        noise.m_AmplitudeGain = maxShakeIntensity * 2f;
        noise.m_FrequencyGain = 1f;

        //aumentar rapidamente o tamanho da câmera e dimiuir o tremor da tela
        progress = 0f;
        while(progress < 1f){
            progress = Mathf.Clamp01(progress + Time.deltaTime * 1.2f);

            float newSize = Mathf.Lerp(minCameraSize, initialCameraSize, progress);
            vcam.m_Lens.OrthographicSize = newSize;

            float newIntensity = Mathf.Lerp(maxShakeIntensity * 2f, 0f, progress);
            noise.m_AmplitudeGain = newIntensity;

            yield return null;
        }

        //setar nova velocidade do player
        slimeController.SetJumpSpeed(maxSpeed);

        //reativar movimento do personagem
        slimeController.inputsBlocked = false;

        //ativar parallax do background
        ParallaxBackground parallaxBackground = FindFirstObjectByType<ParallaxBackground>();
        parallaxBackground.activated = true;
    }
}