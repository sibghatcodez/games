using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject[] path;  // Path for enemy to follow
    [SerializeField] int hitpoints = 25; //total hitpoints
    [SerializeField] int enemyHealth = 25; //enemy health
    [SerializeField] int damagePerHit = 0;
    [SerializeField] GameObject objFailed;
    [SerializeField][Range(0f, 10f)] float speed = 5f; //Enemy speed
    [SerializeField] ObjectPool objPool; //total enemy objects will inside be objPool
    [SerializeField] Button tryAgainButton;
    EnemyTarget[] enemytargetScript;

    int towersAllowed = 3;
    int enemiesToKill = 0;
    int totalEnemies = 0;

    [SerializeField] TextMeshProUGUI objective;
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] TextMeshProUGUI tryAgainTxt;

    int wave;

    GameManager gameManager; //GameManager script

    void OnEnable()
    {
        ResetEnemyHealth();
        StartCoroutine(MoveEnemy());
    }
    void ResetEnemyHealth()
    {
        enemyHealth = wave > 0 ? hitpoints : 1000;
    }
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        enemytargetScript = FindObjectsOfType<EnemyTarget>();
        tryAgainButton.onClick.AddListener(onTryAgainClick);
    }

    void Awake()
    {
        wave = PlayerPrefs.GetInt("wave");
        CheckWave();
    }
    void Update()
    {
        wave = PlayerPrefs.GetInt("wave");
        CheckWave();
        if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
    void CheckWave()
    {
        if (wave.Equals(0))
        {
            damagePerHit = 1;

        }
        else if (wave.Equals(1))
        {
            damagePerHit = 5;
            speed = 1;
            towersAllowed = 3;
            enemiesToKill = 3;
            totalEnemies = 5;
            objective.text = $"Objective: \nKill atleast {enemiesToKill} Enemies\n Towers Given: {towersAllowed}";
        }
        else if (wave.Equals(2))
        {
            damagePerHit = 3;
            speed = 0.7f;
            towersAllowed = 4;
            enemiesToKill = 3;
            totalEnemies = 4;

            objective.text = $"Objective: \nKill atleast {enemiesToKill} Enemies\n Towers Given: {towersAllowed}";
        }
        else if (wave.Equals(3))
        {
            damagePerHit = 4;
            speed = 2;
            towersAllowed = 5;
            enemiesToKill = 2;
            totalEnemies = 3;

            objective.text = $"Objective: \nKill atleast {enemiesToKill} Enemies\n Towers Given: {towersAllowed}";
        }
        PlayerPrefs.SetInt("towersAllowed", towersAllowed);
    }
    IEnumerator MoveEnemy()
    {
        foreach (GameObject tile in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = tile.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return null;
            }
        }

        if (wave != 0)
        {
            gameManager.enemiesPassed++;
            gameObject.SetActive(false);
        }
            CheckRound(gameManager.enemiesPassed, gameManager.enemiesKilled);
    }
    void OnParticleCollision(UnityEngine.GameObject other)
    {
        if (wave != 0)
        {
            if (enemyHealth > 0) enemyHealth -= damagePerHit;
            else
            {
                transform.gameObject.SetActive(false);
                gameManager.enemiesKilled++;
                CheckRound(gameManager.enemiesPassed, gameManager.enemiesKilled);
            }
        }
    }
    void CheckRound(int a, int b)
    {
            Debug.Log(a + b + " -" + totalEnemies + " WAVE:" + wave);
            if (a + b == totalEnemies)
            {
            if(CheckObjective()) {
                int wave = PlayerPrefs.GetInt("wave");

                if (wave < 3)
                {
                    PlayNextLevel();
                }
                else {
                    SceneManager.LoadScene("End");
                    PlayerPrefs.DeleteAll();
                }
            }
       }
    }
    bool CheckObjective() {

        if(gameManager.enemiesKilled < enemiesToKill) {
            txt.text = "Objective Failed...";
            tryAgainTxt.text = "PLAY AGAIN";
            objFailed.SetActive(true);
            objective.gameObject.SetActive(false);
            return false;
        } else return true;
    }
        void PlayNextLevel() {
            objFailed.SetActive(true);
            Debug.Log("objfailed is set to true");
            txt.text = "Objective Cleared!";
            objective.gameObject.SetActive(false);
            tryAgainTxt.text = "NEXT LEVEL";
    }
    void onTryAgainClick () {
        if(txt.text.Equals("Objective Cleared!")) {
            wave = wave+1;
            PlayerPrefs.SetInt("wave", wave);
            SceneManager.LoadScene("Level 1");
        } else {
            SceneManager.LoadScene("Level 1");
        }
    }
}