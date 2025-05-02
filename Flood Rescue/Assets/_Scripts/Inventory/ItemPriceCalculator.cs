using TMPro;
using UnityEngine;

public class ItemPriceCalculator : MonoBehaviour
{
    #region Singleton
    public static ItemPriceCalculator Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

    [SerializeField] private InventoryData inventoryData;
    [SerializeField] private TextMeshProUGUI sellPriceTag;
    [SerializeField] private TextMeshProUGUI _globalScore;
    [SerializeField] private TextMeshProUGUI quantityTag;
    private float item_quantity = 0;
    private int item_price = 0;
    private GameObject selected_item;

    public void CalculatePrice(GameObject obj)
    {
        if (obj.name.Equals("BEACH BALL"))
        {
            item_quantity = inventoryData.beachBall;
        }
        else if (obj.name.Equals("UMBRELLA"))
        {
            item_quantity = inventoryData.umbrella;
        }
        else if (obj.name.Equals("PLASTIC BOTTLE"))
        {
            item_quantity = inventoryData.plasticBottle;
        }
        else if (obj.name.Equals("TIN"))
        {
            item_quantity = inventoryData.tin;
        }
        UpdateSellPrice(obj);


        selected_item = obj;
        quantityTag.text = item_quantity.ToString();
    }

    private void UpdateSellPrice(GameObject obj)
    {
        if (ItemPrices.Prices.TryGetValue(obj.name, out int item_price))
        {
            sellPriceTag.text = (item_price * item_quantity).ToString();
        }
    }


    public void OnSellButtonClicked()
    {
        if (selected_item != null)
        {
            int sellPrice = (int.Parse(_globalScore.text.Split(" ")[1]) + int.Parse(sellPriceTag.text));
            _globalScore.text = "Score: " + sellPrice.ToString();
            sellPriceTag.text = "0";
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score", 0) + sellPrice);
            PlayerPrefs.Save();

            OnItemSelected.Instance.DeselectItem();

            if (selected_item.name.Equals("BEACH BALL"))
            {
                inventoryData.beachBall -= item_quantity;
                if (inventoryData.beachBall <= 0) Inventory.Instance.RemoveItem(selected_item);
            }
            else if (selected_item.name.Equals("UMBRELLA"))
            {
                inventoryData.umbrella -= item_quantity;
                if (inventoryData.umbrella <= 0) Inventory.Instance.RemoveItem(selected_item);
            }
            else if (selected_item.name.Equals("PLASTIC BOTTLE"))
            {
                inventoryData.plasticBottle -= item_quantity;
                if (inventoryData.plasticBottle <= 0) Inventory.Instance.RemoveItem(selected_item);
            }
            else if (selected_item.name.Equals("TIN"))
            {
                inventoryData.tin -= item_quantity;
                if (inventoryData.tin <= 0) Inventory.Instance.RemoveItem(selected_item);
            }


            if (ItemPrices.Prices.TryGetValue(selected_item.name, out int item_price))
            {
                sellPriceTag.text = (item_price * item_quantity).ToString();
            }

            item_quantity = 0;
            item_price = 0;

            sellPriceTag.text = "0";
            quantityTag.text = "0";
            UpdateQtyLabel();
            selected_item = null;
        }
    }

    public void DecreaseQuantity()
    {
        if (selected_item != null && item_quantity > 1)
        {
            item_quantity--;
            quantityTag.text = item_quantity.ToString();

            if (ItemPrices.Prices.TryGetValue(selected_item.name, out int item_price))
            {
                sellPriceTag.text = (item_price * item_quantity).ToString();
            }
        }
    }
    public void IncreaseQuantity()
    {
        float maxQty = GetItemQty();
        if (selected_item != null && item_quantity < maxQty)
        {
            item_quantity++;
            quantityTag.text = item_quantity.ToString();

            if (ItemPrices.Prices.TryGetValue(selected_item.name, out int item_price))
            {
                sellPriceTag.text = (item_price * item_quantity).ToString();
            }
        }
    }
    private float GetItemQty()
    {
        if (selected_item)
        {
            if (selected_item.name.Equals("BEACH BALL")) return inventoryData.beachBall;
            if (selected_item.name.Equals("UMBRELLA")) return inventoryData.umbrella;
            if (selected_item.name.Equals("PLASTIC BOTTLE")) return inventoryData.plasticBottle;
            if (selected_item.name.Equals("TIN")) return inventoryData.tin;
        }
        return 0;
    }
    private void UpdateQtyLabel()
    {
        if (selected_item != null)
        {
            TextMeshProUGUI qtyLabel = selected_item.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            if (selected_item.name.Equals("BEACH BALL")) { qtyLabel.text = inventoryData.beachBall.ToString(); print("b"); }
            if (selected_item.name.Equals("UMBRELLA")) { qtyLabel.text = inventoryData.umbrella.ToString(); print("u"); }
            if (selected_item.name.Equals("PLASTIC BOTTLE")) { qtyLabel.text = inventoryData.plasticBottle.ToString(); print("p"); }
            if (selected_item.name.Equals("TIN")) { qtyLabel.text = inventoryData.tin.ToString(); print("t"); }
        }
    }
}