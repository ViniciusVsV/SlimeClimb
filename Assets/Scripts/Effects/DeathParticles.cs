using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathParticles : MonoBehaviour
{
    public static DeathParticles Instance;

    [SerializeField] private ParticleSystem particlePrefab;

    void Start(){
        Instance = this;
    }

    public void PlayParticles(Vector3 position, Vector3 scale){
        ParticleSystem newParticle = Instantiate(particlePrefab, position, Quaternion.identity);

        newParticle.transform.localScale = scale;

        newParticle.Play();

        Destroy(newParticle.gameObject, 2.5f);
    }
}