using UnityEngine;

public class SlimeMergeEffects : MonoBehaviour
{
    public void PlayEffects()
    {
        AudioController.Instance.PlayMergeSound();
    }
}