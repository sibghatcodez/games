using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnItemSelected : MonoBehaviour
{
    #region Singleton
    public static OnItemSelected Instance { get; private set; }
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
    private GameObject selected_item;
    public void Item(GameObject obj)
    {
        if (selected_item == null) SelectItem(obj);
        else
        {
            DeselectItem();
            SelectItem(obj);
        }
    }
    private void SelectItem(GameObject obj)
    {
        selected_item = obj;
        Image img_opacity = selected_item.transform.GetChild(0).GetComponent<Image>();
        Image img = selected_item.transform.GetChild(1).GetComponent<Image>();

        img.enabled = false;
        SetOpacity(img_opacity, 255);
        ShowName();
        ItemPriceCalculator.Instance.CalculatePrice(selected_item);
    }
    public void DeselectItem()
    {
        if (selected_item != null)
        {
            Image img_opacity = selected_item.transform.GetChild(0).GetComponent<Image>();
            SetOpacity(img_opacity, 150);
            HideName();
        }
    }
    private void SetOpacity(Image img, byte opacity)
    {
        img.color = new Color32(
        (byte)(img.color.r * 255),
        (byte)(img.color.g * 255),
        (byte)(img.color.b * 255),
        opacity);
    }
    private void HideName()
    {
        GameObject label = selected_item.transform.GetChild(1).gameObject;
        label.SetActive(false);
    }
    private void ShowName()
    {
        Image img = selected_item.transform.GetChild(1).GetComponent<Image>();
        img.enabled = true;
        img.gameObject.SetActive(true);
    }
}