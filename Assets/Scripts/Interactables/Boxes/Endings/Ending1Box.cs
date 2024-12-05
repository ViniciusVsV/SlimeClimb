using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Ending1Box: MonoBehaviour{
    [SerializeField] private GameObject cam;
    [SerializeField] private float textWaitTime;
    [SerializeField] private GameObject text1;
    [SerializeField] private GameObject text2;
    [SerializeField] private GameObject endText;
    private Rigidbody2D camRb;

    void Start(){
        camRb = cam.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("PlayerCopy")){
            camRb.velocity = Vector2.up * other.GetComponent<SlimeController>().GetJumpSpeed();

            StartCoroutine(Ending1());    
        }
    }   

    IEnumerator Ending1(){
        yield return new WaitForSeconds(textWaitTime);

        //ativar primeiro texto e esperar até ele se desativar
        text1.SetActive(true);
        while(text1.activeSelf)
            yield return null;

        //esperar um pouco a mais de tempo
        yield return new WaitForSeconds(textWaitTime);

        //ativar o segundo texto e esperar até ele se desativar
        text2.SetActive(true);
        while(text2.activeSelf)
            yield return null;

        //esperar um pouco a mais de tempo
        yield return new WaitForSeconds(textWaitTime * 2f);
        endText.SetActive(true);
    }
}