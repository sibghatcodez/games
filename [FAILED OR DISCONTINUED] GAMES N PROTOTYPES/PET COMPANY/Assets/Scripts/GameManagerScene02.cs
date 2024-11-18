using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScene02 : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI[] count;
    int fruitCount = 4;
    public List<int> itemsPicked;
    int currentLevel = 0;

    void Start()
    {
        LoadItemsPicked();
        SetItemCount(); // Call to update UI with loaded data
        currentLevel = PlayerPrefs.GetInt("level", 0);
    }

    void Update()
    {
    }

    public void LoadItemsPicked()
    {
        string itemsPickedString = PlayerPrefs.GetString("ItemsPicked", "0,0,0,0");
        itemsPicked = itemsPickedString.Split(',').Select(int.Parse).ToList();
    }

    void SetItemCount()
    {
        for (int i = 0; i < fruitCount; i++)
        {
            count[i].text = itemsPicked[i].ToString(); // Set directly without +=
        }
    }

    public void RedirectToSceneZero () {
        if (int.Parse(count[currentLevel].text) > 0) {
            Debug.LogWarning("FEED THE BABY ALL THE BANANANANAS FIRST");
        } else {
            SceneManager.LoadScene(0);
        }
    }
}