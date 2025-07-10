using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class DeathCameraShake : MonoBehaviour
{
    private CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeCamera()
    {
        cinemachineImpulseSource.GenerateImpulse();
    }
}