using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverUI, PauseUI, Player;
    [SerializeField] UnityEngine.UI.Button PauseBtn, PlayBtn, CloseBtn, MusicBtn, PausePlayBtn;
    [SerializeField] Sprite PauseSprite, PlaySprite, musicOnSprite, musicOffSprite;
    [SerializeField] TextMeshProUGUI survivalTXT, scoreTXT, countdownTXT, scoreUI;
    [SerializeField] Rigidbody playerRigidbody;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TrainScript trainScript;
    [SerializeField] SoundManager soundManager;
    [SerializeField] AudioSource playerBackgroundMusic;
    [SerializeField] GameObject panel, particle;

    public bool isMusicOn = true, isGamePaused = false, isGameOver = false, scoreSaved = false;
    float score = 0f;
    int index = 3;

    void Start()
    {
        CloseBtn.onClick.AddListener(() => SceneManager.LoadScene("Lobby"));
        PlayBtn.onClick.AddListener(() => SceneManager.LoadScene("Storymode"));
        MusicBtn.onClick.AddListener(ToggleMusic);
        PauseBtn.onClick.AddListener(TogglePause);
        PausePlayBtn.onClick.AddListener(TogglePause);

        PlayerPrefs.SetFloat("Score", 0);
    }

    void Update()
    {
        if (!isGameOver)
        {
            UpdateScoreText();
            UpdateSurvivalText();
        }
        else if (!scoreSaved)
        {
            HandleGameOver();
        }

        if(PlayerPrefs.GetInt("checkpoint", 0).Equals(1)) particle.SetActive(false);
    }

    void UpdateScoreText()
    {
        int checkpoint = PlayerPrefs.GetInt("checkpoint", 0);
        if (checkpoint.Equals(0))
        {
            score = Mathf.Floor(Player.transform.position.z) - 10;
            scoreTXT.text = $"Total Score: {score}";
            scoreUI.text = score.ToString();
            Debug.Log("Score updated for cp: 0");
        }
        else if (checkpoint.Equals(1))
        {
            score = Mathf.Floor(Player.transform.position.z - 105);
            scoreTXT.text = $"Total Score: {score}";
            scoreUI.text = score.ToString();
            Debug.Log("Score updated for cp: 1");
        }
    }

    void UpdateSurvivalText()
    {
        survivalTXT.text = $"Survival Time: {ConvertSeconds((int)Mathf.Floor(trainScript.timer))}";
    }

    void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        MusicBtn.image.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
        soundManager.isSoundOn = isMusicOn;
        playerBackgroundMusic.enabled = isMusicOn;
    }

    void TogglePause()
    {
        isGamePaused = !isGamePaused;
        PauseBtn.image.sprite = isGamePaused ? PlaySprite : PauseSprite;
        PauseUI.SetActive(isGamePaused);
        playerMovement.IsGamePaused = isGamePaused;
    }

    void HandleGameOver()
    {
        if (PauseUI.activeInHierarchy)
        {
            playerMovement.IsGamePaused = true;
            PauseUI.SetActive(false);
        }

        PlayerPrefs.SetFloat("Score", score + PlayerPrefs.GetFloat("Score", 0));
        PlayerPrefs.Save();
        scoreSaved = true;
    }

    string ConvertSeconds(int seconds)
    {
        int hours = seconds / 3600, minutes = (seconds % 3600) / 60, remainingSeconds = seconds % 60;
        return $"{(hours > 0 ? $"{hours}hr " : "")}{(minutes > 0 ? $"{minutes}m " : "")}{remainingSeconds}s".Trim();
    }

    void CountdownMessage()
    {
        if (index > 0)
        {
            countdownTXT.gameObject.SetActive(true);
            countdownTXT.text = (--index).ToString();
            Invoke(nameof(CountdownMessage), 1f);
        }
        else
        {
            countdownTXT.gameObject.SetActive(false);
        }
    }
}
