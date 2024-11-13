using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public int enemiesKilled = 0;
    public int enemiesPassed = 0;

    void Awake() {
        if(SceneManager.GetActiveScene().Equals("Start")) {
            PlayerPrefs.SetInt("wave", 0);
            Debug.Log("Scene set to [0]");
        }
    }
    void Start()
    {
    }
    void Update()
    {
        
    }
}