using UnityEngine;

[CreateAssetMenu(fileName = "Boat Random Positions", menuName = "BoatRandomSpawn")]
public class BoatRandomSpawns : ScriptableObject
{
    public Vector3[] positions;
    public Vector3[] rotations;
}