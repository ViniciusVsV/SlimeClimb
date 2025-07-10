using UnityEngine;

public class Merge : MonoBehaviour
{
    public void MergeSlimes(GameObject slime1, GameObject slime2)
    {
        Debug.Log("Merge entre os slimes: " + slime1.name + " e " + slime2.name);
    }
}