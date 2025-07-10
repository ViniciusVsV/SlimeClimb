using UnityEngine;

public class SlimeMergeEffects : MonoBehaviour
{
    private AudioController audioController;

    private void Start()
    {
        audioController = FindFirstObjectByType<AudioController>();
    }

    public void PlayEffects()
    {
        audioController.PlayMergeSound();
    }
}