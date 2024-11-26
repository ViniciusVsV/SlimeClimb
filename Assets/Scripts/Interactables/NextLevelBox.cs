using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelBox : MonoBehaviour{
    [SerializeField] string nextLevelName;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.CompareTag("PlayerCopy"))
            SceneManager.LoadScene(nextLevelName);
    }
}