using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class test : MonoBehaviour
{

    [SerializeField] private CinemachineImpulseSource a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            a.GenerateImpulse();
    }
}
