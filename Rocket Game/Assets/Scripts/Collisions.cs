using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisions : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI round;
    [SerializeField] public TextMeshProUGUI attempts;
    [SerializeField] public Movement movementScript;
    public int RoundAttempts;
    public int currentSceneIndex;

    public int TotalObstaclesHit = 0;
    public int TotalAttempts = 0;

    void Start()
    {
        RoundAttempts = PlayerPrefs.GetInt("Attempts", 0);
        TotalAttempts = PlayerPrefs.GetInt("TotalAttempts", 0);
        TotalObstaclesHit = PlayerPrefs.GetInt("ObstaclesHit", 0);
    }
    void OnCollisionEnter(Collision col)
    {

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        switch (col.gameObject.tag)
        {
            case "Finish":
                movementScript.PlaySound(movementScript.SuccessSound);
                movementScript.enabled = false;
                movementScript.player.useGravity = false;
                Invoke("PlayNextRoundWrapper", 1f);
                Debug.Log("ROUND WON");
                break;
            case "Ground":
            case "Obstacle":
                movementScript.PlaySound(movementScript.LoseSound);
                movementScript.enabled = false;
                movementScript.player.useGravity = false;
                Invoke("ReplayRoundWrapper", 1f);
                TotalAttempts++;
                TotalObstaclesHit++;
                PlayerPrefs.SetInt("ObstaclesHit", TotalObstaclesHit);
                PlayerPrefs.SetInt("TotalAttempts", TotalAttempts);
                break;
            case "Start":
                round.text = "Round: " + (currentSceneIndex);
                break;
        }
    }

    void PlayNextRoundWrapper()
    {
        if((currentSceneIndex+1) <= 8){
            PlayNextRound((currentSceneIndex + 1));
        }
    }
    void PlayNextRound(int index)
    {
        SceneManager.LoadScene(index);
        movementScript.enabled = true;
        Debug.Log("Round : " + index);
        round.text = "Round: " + index;
        attempts.text = "Attempts: 0";
        RoundAttempts = 0;
        PlayerPrefs.DeleteKey("Attempts");
        SceneManager.LoadScene(index);
    }
    void ReplayRoundWrapper()
    {
        ReplayRound(currentSceneIndex);
    }
    void ReplayRound(int index)
    {
        RoundAttempts++;
        PlayerPrefs.SetInt("Attempts", RoundAttempts);
        PlayerPrefs.Save();
        SceneManager.LoadScene(index);
        attempts.text = "Attempts: " + PlayerPrefs.GetInt("Attempts");
        Debug.Log(PlayerPrefs.GetInt("Attempts"));
    }
}