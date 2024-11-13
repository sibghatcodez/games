using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerStart : MonoBehaviour
{
    [SerializeField] Button startBtn;
    [SerializeField] TextMeshProUGUI bestScore;
    int highScore = 0;
    void Start()
    {
        startBtn.onClick.AddListener(() => SceneManager.LoadScene(1));
    }

    // Update is called once per frame
    void Update()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        bestScore.text = "BEST: " + highScore.ToString();
    }
}
