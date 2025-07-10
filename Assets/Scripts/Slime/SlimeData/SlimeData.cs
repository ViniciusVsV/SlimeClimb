using UnityEngine;

[CreateAssetMenu(fileName = "SlimeData", menuName = "Scriptable Objects/SlimeData")]
public class SlimeData : ScriptableObject
{
    [Header("Size")]
    [SerializeField] private float baseSize;
    [SerializeField] private float divisionSizeReduction;

    public Vector3 GetBaseSize()
    {
        return new Vector3(baseSize, baseSize, 1f);
    }
    public Vector3 GetDivisionSizeReduction()
    {
        return new Vector3(divisionSizeReduction, divisionSizeReduction, 0f);
    }
}