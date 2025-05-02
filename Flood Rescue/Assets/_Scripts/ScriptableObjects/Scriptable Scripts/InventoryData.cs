using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "InventoryData")]
public class InventoryData : ScriptableObject
{
    [SerializeField] public float beachBall, umbrella, plasticBottle, tin;
}