using UnityEngine;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private Canvas Gamewon, Gamelost;
    public UIState currentUIState;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        currentUIState = UIState.GAME;
    }

    public void changeUIState(UIState uIState)
    {
        currentUIState = uIState;
        GetGameObject(uIState).enabled = true;

        if (uIState.Equals(UIState.GAMELOST))
        {
            GameManager.Instance.gameData.TotalTimesLost++;
            Subject.Instance.NotifyAllObserver(ObserverEnum.GAME_OVER);
        }
        if (uIState.Equals(UIState.GAMEWON))
        {
            GameManager.Instance.gameData.TotalTimesWon++;
            GameManager.Instance.boat.GetComponent<Rigidbody>().isKinematic = true;
            Subject.Instance.NotifyAllObserver(ObserverEnum.GAME_OVER);
        }
        if (uIState.Equals(UIState.GAME)) GameManager.Instance.gameData.TotalTimesGamePlayed++;

    }

    private Canvas GetGameObject(UIState obj)
    {
        return obj switch
        {
            UIState.GAMEWON => Gamewon,
            UIState.GAMELOST => Gamelost,
            _ => null
        };
    }
}