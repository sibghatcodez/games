using TMPro;
using UnityEngine;

public enum ScoreSystem
{
    RESCUE_POINTS = 50,
    QUICK_RESCUE_POINTS = RESCUE_POINTS * 2, // Multiply instead of adding RESCUE_POINTS to itself
    PICKUP_POINTS = 25,
}

public class Scoring : MonoBehaviour
{
    #region Singleton
    public static Scoring Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion

    [SerializeField] private TextMeshProUGUI scoreText;
    private float scoreIncreaseDuration = 1f;
    private float scoreIncreaseProgress = 0f;
    private float currentScore = 0f, targetScore = 0f;
    private void FixedUpdate()
    {
        if (currentScore < targetScore)
        {
            scoreIncreaseProgress += Time.deltaTime;
            currentScore = Mathf.Lerp(currentScore, targetScore, scoreIncreaseProgress / scoreIncreaseDuration);
            scoreText.text = Mathf.FloorToInt(currentScore).ToString();

            if (scoreIncreaseProgress >= scoreIncreaseDuration)
            {
                currentScore = targetScore;
                scoreIncreaseProgress = 0f;
            }
        }
    }

    public void IncreaseScore(ScoreSystem scoreSystem)
    {
        targetScore += (int)scoreSystem;
        scoreIncreaseProgress = 0f;
        AudioManager.Instance.PlayAudio(AudioName.COINS);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score", 0) + int.Parse(scoreText.text));
        PlayerPrefs.Save();
    }
}