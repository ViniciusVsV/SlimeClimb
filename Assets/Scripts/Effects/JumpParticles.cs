using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class JumpParticles : MonoBehaviour{
    public static JumpParticles Instance;

    [SerializeField] private ParticleSystem particlePrefab;

    void Start(){
        Instance = this;
    }

    public void PlayParticles(Vector3 position, Vector3 scale, Quaternion rotation, Color color){
        ParticleSystem newParticle = Instantiate(particlePrefab, position, rotation);

        newParticle.transform.localScale = scale;

        if(color != Color.green){        
            var main = newParticle.main;
            main.startColor = color;
        }

        newParticle.Play();

        Destroy(newParticle.gameObject, 0.5f);
    }
}