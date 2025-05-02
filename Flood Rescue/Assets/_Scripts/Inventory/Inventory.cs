using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region 
    public static Inventory Instance { get; private set; }
    private void Awake()
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
    [SerializeField] private Sprite beachBall, umbrella, plasticBottle, tin;
    [SerializeField] private GameObject item;
    [SerializeField] private Transform inventoryContent;

    private void OnEnable() => ClearInventory();
    private void Start()
    {
        AddItems();
    }
    public void AddItems()
    {
        AddItemsToInventory("BEACH BALL", inventoryData.beachBall, beachBall);
        AddItemsToInventory("UMBRELLA", inventoryData.umbrella, umbrella);
        AddItemsToInventory("PLASTIC BOTTLE", inventoryData.plasticBottle, plasticBottle);
        AddItemsToInventory("TIN", inventoryData.tin, tin);
    }
    public void ClearInventory()
    {
        foreach (Transform child in inventoryContent)
        {
            Destroy(child.gameObject);
        }
    }
    private void AddItemsToInventory(string item_name, float count, Sprite sprite)
    {
        if (count <= 0) return;

        GameObject newItem = Instantiate(item, inventoryContent);

        Image itemImage = newItem.transform.GetChild(0).GetComponent<Image>();
        TextMeshProUGUI itemText = newItem.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        Button btn = newItem.transform.GetChild(0).GetComponent<Button>();
        TextMeshProUGUI itemName = newItem.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        newItem.name = item_name;

        itemImage.sprite = sprite;
        itemText.text = count.ToString();
        itemName.text = item_name;

        itemImage.enabled = true;
        btn.enabled = true;
    }
    public void RemoveItem(GameObject objToRemove)
    {
        Destroy(objToRemove);
    }
}