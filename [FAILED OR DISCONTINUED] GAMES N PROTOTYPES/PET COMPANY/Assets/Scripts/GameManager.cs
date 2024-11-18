using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.ComponentModel.Design;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject parent, plane, inGameUI;
    [SerializeField] GameObject[] fruits;
    [SerializeField] int spawnCount = 25;

    [SerializeField] public TextMeshProUGUI challengeUI, timeUI, pickedItemsUI, bagCapacityUI;
    int timeLimit, bagCapacityLimit;
    public int timeLeft, bagCapacityLeft;
    public string ItemToPick;
    public int itemsToBePicked = 15;
    public int level = 0, fruitsSpawned = 0;
    public bool gameStatus = false;
    public List<String> items = new List<string>() { "Bananas", "Apples", "Mangoes", "Melons" };
    public List<int> itemsPicked = new List<int>() { 0, 0, 0, 0 };

    int height = 50, width = 50;
    float fruitSpacing = 2f;

    private List<Vector3> spawnPositions = new List<Vector3>();
    private List<GameObject> spawnedFruits = new List<GameObject>();

    void Start()
    {
        SetInGameUIVisibility(false);
        SetChallengeUIText();
        Invoke("HideChallengeUI", 5f);
    }
    void SetLevels()
    {
        PlayerPrefs.SetInt("level", level);
        switch (level)
        {
            case 0:
                bagCapacityLimit = 25;
                timeLimit = 20;
                bagCapacityLeft = bagCapacityLimit;
                timeLeft = timeLimit;
                break;
        }
    }
    public void SaveItemsPicked()
{
    string itemsPickedString = string.Join(",", itemsPicked);
    PlayerPrefs.SetString("ItemsPicked", itemsPickedString);
    Debug.Log(itemsPickedString);
    PlayerPrefs.Save();
}

    void SetItem()
    {
        ItemToPick = items[level];
    }
    void Update()
    {
        CheckAndSetInGameUI();
    }
    void StartTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= 1;
            Invoke("StartTimer", 1f);
        }
        else
        {
            gameStatus = false;
            SaveItemsPicked ();
            SceneManager.LoadScene(1);
            // SetLevels ();
        }
    }
    void CheckAndSetInGameUI()
    {
        if (inGameUI.activeInHierarchy)
        {
            timeUI.text = "TIME LEFT: " + timeLeft;
            bagCapacityUI.text = "BAG CAPACITY: " + bagCapacityLeft;
            pickedItemsUI.text = $"PICKED {items[level]}'s: " + itemsPicked[level];
        }
    }
    void SetInGameUIVisibility(bool visibility)
    {
        inGameUI.SetActive(visibility);
    }

    void HideChallengeUI()
    {
        challengeUI.gameObject.SetActive(false);
        SetInGameUIVisibility(true);

        gameStatus = true;
        SetItem();
        SetLevels();
        SpawnFruits ();
        Invoke("StartTimer", 1f);
    }
    void SetChallengeUIText()
    {
        challengeUI.text = $"PICK 15 {items[level]} \nIN 30 SECONDS";
    }
    void SetSize()
    {
        plane.transform.localScale = new Vector3(height, 1, width);
    }
    public void IncrementItem(string itemTag)
    {
        switch (itemTag)
        {
            case "Banana":
                bagCapacityLeft -= 1;
                itemsPicked[0] += 1;
                break;
            case "Apple":
                bagCapacityLeft -= 1;
                itemsPicked[1] += 1;
                break;
            case "Mango":
                bagCapacityLeft -= 1;
                itemsPicked[2] += 1;
                break;
            case "Melon":
                bagCapacityLeft -= 1;
                itemsPicked[3] += 1;
                break;
        }
        Debug.Log($"Bananas: {itemsPicked[0]} Apples: {itemsPicked[1]} Mangoes: {itemsPicked[2]} Melons: {itemsPicked[3]}");
    }
    void SpawnFruits()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition;
            int attempts = 0;
            do
            {
                spawnPosition = GetRandomPositionWithinPlane();
                attempts++;
            }
            while (!IsPositionValid(spawnPosition) && attempts < 100);


            if (attempts < 100)
            {
                spawnPositions.Add(spawnPosition);
                if (fruitsSpawned > 14) SpawnFruits(spawnPosition);
                else SpawnSpecificFruit(spawnPosition);
            }
        }
    }

    Vector3 GetRandomPositionWithinPlane()
    {

        float xPos = UnityEngine.Random.Range((-width + 2f) / 2f, (width - 2f) / 2f);
        float zPos = UnityEngine.Random.Range((-height + 2f) / 2f, (height - 2f) / 2f);
        return new Vector3(xPos, 0, zPos);
    }

    bool IsPositionValid(Vector3 position)
    {

        foreach (var pos in spawnPositions)
        {
            if (Vector3.Distance(pos, position) < fruitSpacing)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnFruits(Vector3 position)
    {
        int randomFruit;
        do {
             randomFruit = UnityEngine.Random.Range(0, fruits.Length);
        } while (randomFruit == level);

        GameObject fruitPrefab = fruits[randomFruit];
        GameObject fruitInstance = Instantiate(fruitPrefab, position, Quaternion.identity, parent.transform);
        fruitInstance.transform.localPosition = position;

        fruitsSpawned ++;
        Debug.Log(randomFruit);
        spawnedFruits.Add(fruitInstance);
    }
        void SpawnSpecificFruit(Vector3 position)
    {

        GameObject fruitPrefab = fruits[level];
        GameObject fruitInstance = Instantiate(fruitPrefab, position, Quaternion.identity, parent.transform);
        fruitInstance.transform.localPosition = position;

        fruitsSpawned ++;
        Debug.Log(fruits[level] + "" + level);
        spawnedFruits.Add(fruitInstance);
    }

    public void ClearFruits()
    {
        foreach (GameObject fruit in spawnedFruits)
        {
            Destroy(fruit);
        }
        spawnedFruits.Clear();
        spawnPositions.Clear();
    }
}