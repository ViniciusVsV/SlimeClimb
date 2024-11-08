using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathCameraShake : MonoBehaviour{
    public static DeathCameraShake Instance;
    
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;

    void Start(){
        Instance = this;
    }

    public void GenerateImpulse(){
        cinemachineImpulseSource.GenerateImpulse();
    }
}