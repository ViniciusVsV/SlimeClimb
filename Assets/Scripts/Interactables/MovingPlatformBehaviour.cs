using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private float waitDuration;
    private Transform targetPoint;

    public bool isWaiting;

    void Start(){
        targetPoint = point1;
    }

    void Update(){
        if(isWaiting)
            return;

        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        if(transform.position == point1.position){
            targetPoint = point2;

            StartCoroutine(Wait());
        }
        else if(transform.position == point2.position){
            targetPoint = point1;

            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait(){
        isWaiting = true;

        yield return new WaitForSeconds(waitDuration);

        isWaiting = false;
    }

    //Tentar criar um trigger apenas para detectar quando o player sai ou entra

    private void OnCollisionEnter2D(Collision2D other) {
        other.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D other) {
        other.transform.SetParent(null);
    }
}