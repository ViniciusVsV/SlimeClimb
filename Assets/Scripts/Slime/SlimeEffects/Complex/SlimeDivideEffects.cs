using UnityEngine;

public class SlimeDivideEffects : MonoBehaviour
{
    private AudioController audioController;

    private void Start()
    {
        audioController = FindFirstObjectByType<AudioController>();
    }

    public void PlayEffects()
    {
        audioController.PlayCopySound();
    }
}