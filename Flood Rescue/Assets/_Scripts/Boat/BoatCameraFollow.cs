using UnityEngine;
public class BoatCameraFollow : MonoBehaviour
{
    [Header("Camera Follow Settings")]
    public Transform boat;
    [Header("Mode Settings")]
    [SerializeField] private string mode = "TPP";
    [SerializeField] private Sprite[] sprites;

    private Vector3 desiredPosition = Vector3.zero;
    private Vector3 smoothedPosition = Vector3.zero;
    [SerializeField] private GameData gameData;
    private void LateUpdate()
    {
        if (mode.Equals("TPP"))
        {
            desiredPosition = boat.position - boat.forward * gameData.TPP.x + Vector3.up * gameData.TPP.y + boat.right * -gameData.TPP.z;
        }
        else
        {
            desiredPosition = boat.position - boat.forward * gameData.FPP.x + Vector3.up * gameData.FPP.y + boat.right * -gameData.FPP.z;
        }

        smoothedPosition = Vector3.Lerp(smoothedPosition, desiredPosition, Time.deltaTime * gameData.PositionLagSpeed);
        transform.position = smoothedPosition;

        Quaternion targetRotation = Quaternion.LookRotation(-boat.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * gameData.RotationLagSpeed);
    }
    public void OnSwitchButtonClicked()
    {
        mode = mode.Equals("TPP") ? "FPP" : "TPP";
    }
}