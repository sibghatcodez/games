using UnityEngine;

public class OnItemCollected : MonoBehaviour
{
    [SerializeField] private InventoryData inventoryData;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Boat"))
        {
            string itemName = name;

            if (ItemPrices.Prices.ContainsKey(itemName))
            {
                AddItemInInventory(itemName);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log($"Item {itemName} not recognized.");
            }
        }
    }
    private void AddItemInInventory(string item)
    {
        switch (item)
        {
            case "BEACH BALL":
                ItemCollectParticle.Instance.ShowParticle(Particles.BEACH_BALL);
                inventoryData.beachBall++;
                break;
            case "UMBRELLA":
                ItemCollectParticle.Instance.ShowParticle(Particles.UMBRELLA);
                inventoryData.umbrella++;
                break;
            case "PLASTIC BOTTLE":
                ItemCollectParticle.Instance.ShowParticle(Particles.PLASTIC_BOTTLE);
                inventoryData.plasticBottle++;
                break;
            case "TIN":
                ItemCollectParticle.Instance.ShowParticle(Particles.TIN);
                inventoryData.tin++;
                break;
            default:
                Debug.LogWarning($"Item '{item}' is not recognized.");
                break;
        }
    }
}