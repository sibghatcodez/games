using TMPro;
using UnityEngine;

public class MenuScript : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI _globalScore;
    [SerializeField] private TextMeshProUGUI _totalRescuedNPCs;
    [SerializeField] private GameData gameData;
    private void OnEnable()
    {
        _globalScore.text = "Score: " + PlayerPrefs.GetInt("Score", 0).ToString();
        _totalRescuedNPCs.text = "Rescued NPCs: " + gameData.TotalTimesRescued.ToString();

        Invoke("AddObserver", 1f); //Delay bc this sometimes doesnt get initiated(Subject.Instace)
    }


    #region Design Pattern
    private void AddObserver() => Subject.Instance.AddObserver(this);
    public void OnNotify(ObserverEnum observerEnum)
    {
        if (observerEnum.Equals(ObserverEnum.RESET_MENU_SCENE_TEXTS))
        {
            _globalScore.text = "Score: 0";
            _totalRescuedNPCs.text = "Rescued NPCs: 0";
        }
    }

    private void OnDisable() => Subject.Instance.RemoveObserver(this);
    #endregion
}