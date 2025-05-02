using UnityEngine;

public class OnResetButton : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    public void OnResetButtonCalled()
    {
        Subject.Instance.NotifyAllObserver(ObserverEnum.RESET_MENU_SCENE_TEXTS);

        gameData.TotalTimesBoatExploded = 0;
        gameData.TotalTimesGamePlayed = 0;
        gameData.TotalTimesLost = 0;
        gameData.TotalTimesWon = 0;
        gameData.TotalTimesRescued = 0;
        gameData.TotalTimesTutorialPlayed = 0;

        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("stage", 1);

        PlayerPrefs.Save();
    }
}