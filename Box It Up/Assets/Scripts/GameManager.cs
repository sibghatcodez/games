using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI text, highScoreText;
    [SerializeField] public GameObject scorecard;
    [SerializeField] public Text scoretext;
    [SerializeField] public Button playBtn;
    [SerializeField] public GameObject background;
    [SerializeField] public AudioSource audio;
    [SerializeField] public AudioClip[] audioClip;
    public int score = 0;
    public int highScore = 0;
    public bool IsPlayerDead = false;
    void Start()
    {
        playBtn.onClick.AddListener(() => SceneManager.LoadScene(1));
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Display the high score at the start
        if (highScoreText != null)
        {
            highScoreText.text = "BEST: " + highScore.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerDead)
        {
            scorecard.SetActive(true);
            scoretext.text = "SCORE: " + score.ToString();


            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore); // Save the new high score
                PlayerPrefs.Save();

                if (highScoreText != null)
                {
                    highScoreText.text = "BEST: " + highScore.ToString();
                }
            }
        }
        else scorecard.SetActive(false);

        text.text = "SCORE: " + score.ToString();
    }

    public void TapSound () {
        audio.clip = audioClip[6];
        audio.volume = 1f;
        audio.Play();
    }
    public void Signal(string signal, bool isGameOver)
    {
        if (signal != null)
        {
            System.Random rand = new System.Random();
            int randomNumber = rand.Next(0, 6);

            switch (signal)
            {
                case "bounce":
                    audio.clip = audioClip[randomNumber];
                    audio.Play();
                    if(isGameOver) audio.volume = 0.3f;
                    break;
            }
        }
    }
}
