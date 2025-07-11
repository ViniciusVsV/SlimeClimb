using UnityEngine;

public class SlimeStats : MonoBehaviour
{
    [SerializeField] private int slimeId;
    public CircleCollider2D mergeTrigger;

    public bool isDead;
    public bool hasMerged;

    public int GetId() { return slimeId; }
}