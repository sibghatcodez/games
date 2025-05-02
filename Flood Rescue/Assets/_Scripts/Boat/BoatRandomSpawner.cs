using UnityEngine;

public class BoatRandomSpawner : MonoBehaviour
{
    [SerializeField] private BoatRandomSpawns BoatRandomSpawnPositions;
    [SerializeField] private Rigidbody boatRigidbody;
    private void Awake() => SpawnBoatAtRandomPosition();

    private void SpawnBoatAtRandomPosition()
    {
        Vector3[] positions = BoatRandomSpawnPositions.positions;
        Vector3[] rotations = BoatRandomSpawnPositions.rotations;

        int randomIndex = Random.Range(0, positions.Length);
        if (positions.Length > 0)
        {
            boatRigidbody.isKinematic = true;
            transform.localPosition = positions[randomIndex];
            transform.localRotation = Quaternion.Euler(rotations[randomIndex]);
        }
        else
        {
            transform.localPosition = new Vector3(105, 0, -6);
            transform.localRotation = Quaternion.identity;
        }
        Invoke("SetIsKinematicToFalse", 0.5f);
    }
    private void SetIsKinematicToFalse() => boatRigidbody.isKinematic = false;
}