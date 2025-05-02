using UnityEngine;

[CreateAssetMenu(fileName = "SitPositions", menuName = "NPC_SitPositions")]
public class SitPositions : ScriptableObject
{
    public Vector3[] positions;
    public Vector3[] rotations;
}