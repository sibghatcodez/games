using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnItemSelect : MonoBehaviour
{
    public void OnItemSelectClicked()
    {
        AudioManager.Instance.PlayAudio(AudioName.SELECT);
        OnItemSelected.Instance.Item(gameObject);
        print("test");
    }
}