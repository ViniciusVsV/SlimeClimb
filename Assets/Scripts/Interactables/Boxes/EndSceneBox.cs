using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EndSceneBox : MonoBehaviour{
    [SerializeField] private float cutsceneDuration;
    [SerializeField] private GameObject cam;
    private SlimeController[] slimes;

    void Start(){

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("PlayerCopy")){
            SlimeController slimeController = other.GetComponent<SlimeController>();

            StartCoroutine(EndCutscene(other.gameObject, slimeController));
        }
    }   

    IEnumerator EndCutscene(GameObject other, SlimeController slimeController){
        //bloquear os inputs do player
        slimes = FindObjectsOfType<SlimeController>();
        foreach(SlimeController slime in slimes)
            slime.inputsBlocked = true;

        //câmera começa a ir para cima junto com o player
        Rigidbody2D camRb = cam.GetComponent<Rigidbody2D>();
        camRb.velocity = Vector2.up * slimeController.GetJumpSpeed();

        yield return new WaitForEndOfFrame();
        //aumentar gradativamente a velocidade do player

        //aumentar gradativamente o tremor de tela

        //reduzir gradativamente o tamanho da câmera

        //quebrar a porta

        //aumentar rapidamente o tamanho da câmera

        //aparecer texto ("Voce está livre!")

        //esperar um pouco e aparecer texto("...")

        //esperar mais um pouco e aparecer texto("obrigado por jogar!)

        //camera para de seguir o player

        //aparece cena do menu final
    }
}