using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;  // Array of enemy objects
        int enemiesAllowed = 0;

    // Populate pool and spawn an enemy on game start
    void Awake()
    {
        StartCoroutine(PopulateWorld());
        SpawnEnemy();
    }

    IEnumerator PopulateWorld()
    {

        int wave = PlayerPrefs.GetInt("wave");
        if(wave.Equals(1)) enemiesAllowed = 5;
        else if(wave.Equals(2)) enemiesAllowed = 4;
        else if(wave.Equals(3)) enemiesAllowed = 3;

        // foreach (GameObject item in enemies)
        // {
        //     if(wave.Equals(1))
        //     item.gameObject.SetActive(false);
        //     yield return null;
        // }

        Debug.Log("ENEMIS ALLOWED: "+enemiesAllowed);
        for (int i = 0; i < enemiesAllowed; i++)
        {
            enemies[i].gameObject.SetActive(false);
            Debug.Log("ENEMY COUNT: "+ i);
            yield return null;
        }
    }

    // Enable the next available object in the pool
  public IEnumerator EnableObjectInPool()
    {
        int wave = PlayerPrefs.GetInt("wave");
        if(wave.Equals(1)) enemiesAllowed = 5;
        else if(wave.Equals(2)) enemiesAllowed = 5;
        else if(wave.Equals(3)) enemiesAllowed = 4;

        for (int i = 0; i < enemiesAllowed; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                enemies[i].SetActive(true);
                yield return new WaitForSeconds(1f);
            }
        }
    }
    public void SpawnEnemy()
    {
        StartCoroutine(EnableObjectInPool());
    }
}