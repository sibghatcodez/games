using Unity.VisualScripting;
using UnityEngine;

public class NPC_FacingBoat : MonoBehaviour
{
    private Vector3 boatPosition, targetPosition;
    private void LateUpdate()
    {
        CheckAndFaceBoat();
    }

    private void CheckAndFaceBoat()
    {
        float distanceSquared = (transform.position - GameManager.Instance.boat.position).sqrMagnitude;
        if (distanceSquared < 200 * 200) FaceBoat();
    }

    private void FaceBoat()
    {
        boatPosition = GameManager.Instance.boat.position;
        targetPosition = new Vector3(boatPosition.x, transform.position.y, boatPosition.z);
        transform.LookAt(targetPosition);
    }
}