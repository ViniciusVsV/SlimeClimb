using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightnBox : MonoBehaviour{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject copyPrefab;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float duration;
    [SerializeField] private bool activate;
    private GameObject playerLight;
    private GameObject copyLight;

    void Start(){
        playerLight = playerPrefab.transform.GetChild(0).gameObject;
        copyLight = copyPrefab.transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") || other.CompareTag("PlayerCopy")){
            if(activate){
                GameObject aux = other.transform.GetChild(0).gameObject;
                aux.SetActive(true);

                playerLight.SetActive(true);
                copyLight.SetActive(true);

                StartCoroutine(ReduceLight());
            }
            else{
                GameObject aux = other.transform.GetChild(0).gameObject;
                aux.SetActive(false);

                playerLight.SetActive(false);
                copyLight.SetActive(false);
            }
        }
    }

    IEnumerator ReduceLight(){
        float intensity = globalLight.intensity;
        if(intensity == 0f)
            yield break;

        float time = 0f;

        while(time < duration){
            time += Time.deltaTime;

            globalLight.intensity = Mathf.Lerp(intensity, 0f, time / duration);
    
            yield return null;
        }

        globalLight.intensity = 0f;
    }
}