using NUnit.Framework;
using UnityEngine;

public class BoatExplosion : MonoBehaviour, IObserver
{
    [SerializeField] private GameObject explosion;
    private void OnEnable()
    {
        Invoke("AddObserver", 1f); //Delay because sometimes Subject.Instance isnt initialized...
    }
    private void AddObserver() => Subject.Instance.AddObserver(this);
    private void OnDisable()
    {
        Subject.Instance.RemoveObserver(this);
    }
    public void OnNotify(ObserverEnum observerEnum)
    {
        if (observerEnum.Equals(ObserverEnum.BOAT_EXPLODE))
        {
            GameManager.Instance.gameData.TotalTimesBoatExploded++;
            Invoke("ShowGameLost", 1f); //Delay so the player watches the explosion
            explosion.SetActive(true);
        }
    }
    private void ShowGameLost() => UIManager.Instance.changeUIState(UIState.GAMELOST);
}