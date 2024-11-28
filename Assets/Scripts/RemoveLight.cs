using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLight : MonoBehaviour{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("playerCopy"))
            Destroy(gameObject);
    }
}