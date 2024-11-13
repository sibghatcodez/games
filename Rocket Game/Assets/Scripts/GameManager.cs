using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] public TextMeshProUGUI ObstaclesHit;
    [SerializeField] public TextMeshProUGUI Attempts;
    [SerializeField] public TextMeshProUGUI RecordsLbl;
    [SerializeField] public TextMeshProUGUI GameLbl;
    [SerializeField] public TextMeshProUGUI Gamemode;

    [SerializeField] public Animator RecordAnimation;
    bool IsRecordVisible = false;

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Attempts");
    }
    void Update () {
        if(IsRecordVisible) {
        ObstaclesHit.text = "Obstacles Hit: " + PlayerPrefs.GetInt("ObstaclesHit"); 
        Attempts.text = "Total Attempts: " + PlayerPrefs.GetInt("TotalAttempts");
        Debug.Log(PlayerPrefs.GetInt("ObstaclesHit"));
        }

        if(Input.GetKey(KeyCode.Return)) {
            Application.Quit();
            PlayerPrefs.DeleteKey("Attempts");
        }
    }
    public void RecordsVisibility (bool visibility) {
        RecordsLbl.gameObject.SetActive(visibility);
        ObstaclesHit.gameObject.SetActive(visibility);
        Attempts.gameObject.SetActive(visibility);
        IsRecordVisible = visibility;
    }
    public void GameModeVisibility (bool visibility) {
        GameLbl.gameObject.SetActive(visibility);
        Gamemode.gameObject.SetActive(visibility);
    }
}