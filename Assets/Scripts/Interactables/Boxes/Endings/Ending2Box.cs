using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending2Box : MonoBehaviour{
    [SerializeField] private float textWaitTime;
    [SerializeField] private GameObject text1;
    [SerializeField] private GameObject text2;
    [SerializeField] private GameObject endText;
    
    private int activations;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("PlayerCopy")){
            activations++;

            if(activations > 1){
                Destroy(other.gameObject, 1f);

                AudioController.instance.StopMusic();

                StartCoroutine(Ending2());  
            }
        }
    }

    IEnumerator Ending2(){
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
