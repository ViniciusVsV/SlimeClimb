using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Ending1Box: MonoBehaviour{
    [SerializeField] private GameObject cam;
    [SerializeField] private float textWaitTime;
    private Rigidbody2D camRb;

    void Start(){
        camRb = cam.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("PlayerCopy")){
            camRb.velocity = Vector2.up * other.GetComponent<SlimeController>().GetJumpSpeed();
            if (AudioController.instance != null)
                    {
                        AudioController.instance.PlayEndingMusic();
                    }
            StartCoroutine(Ending1());    
        }
    }   

    IEnumerator Ending1(){
        yield return new WaitForSeconds(textWaitTime);

        //aparecer o primeiro texto(Finalmente, liberdade!!)

        yield return new WaitForSeconds(textWaitTime);

        //aparecer o segundo texto(...)

        yield return new WaitForSeconds(textWaitTime);

        //aparecer o ultimo texto (Obrigado por jogar!  :))
    }
}