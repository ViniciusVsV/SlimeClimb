using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour{
    [SerializeField] private float duration;
    [SerializeField] private Image image;

    void OnEnable() {
        StartCoroutine(FadeRoutine());    
    }

    public IEnumerator FadeRoutine(){
        float time = 0;

        Color color = image.color;

        while(time < duration){
            color.a = Mathf.Lerp(0, 1, time / duration);

            image.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        image.color = color;

        yield return new WaitForSeconds(5f);

        time = 0;

        while(time < duration){
            color.a = Mathf.Lerp(1, 0, time / duration);

            image.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        image.color = color;
    }
}