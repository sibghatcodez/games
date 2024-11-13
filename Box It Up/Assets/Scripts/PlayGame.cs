using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    [SerializeField] Text bestScore;
    [SerializeField] Button playBtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Add listener only once when the game starts
        playBtn.onClick.AddListener(doit);
        Debug.Log("wtf");

        // Update best score text
        bestScore.text = "BEST: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void doit()
    {
        Debug.Log("wtf");
        // Add code here to load the scene or do other actions
        // For example:
        // SceneManager.LoadScene("YourSceneName");
    }
}
