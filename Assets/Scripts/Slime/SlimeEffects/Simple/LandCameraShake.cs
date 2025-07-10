using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class LandCameraShake : MonoBehaviour
{
    private CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeCamera(Vector2 direction, float jumpSpeed)
    {
        Vector3 impulseDirection = Vector3.zero;

        if (direction.x != 0)
            impulseDirection.x = -0.02f;
        else
            impulseDirection.y = -0.02f;

        cinemachineImpulseSource.GenerateImpulse(impulseDirection * jumpSpeed / 10);
    }
}