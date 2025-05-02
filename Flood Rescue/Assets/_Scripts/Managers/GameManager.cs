using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour, IObserver
{
    public static GameManager Instance { get; private set; }
    [SerializeField] public GameData gameData;
    private Stopwatch _gameTime;
    public float GameTime
    {
        get
        {
            return _gameTime.ElapsedMilliseconds / 1000f; // Convert milliseconds to seconds
        }
    }

    //Common References:
    [SerializeField] public Transform boat;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _gameTime = new Stopwatch();
    }

    #region Timer
    private void OnEnable()
    {
        _gameTime.Start();
        Invoke("AddObserver", 1f);
    }
    private void AddObserver() => Subject.Instance.AddObserver(this);
    private void OnDisable()
    {
        _gameTime.Stop();
        Subject.Instance.RemoveObserver(this);
    }
    private void StopTimer() { _gameTime.Stop(); _gameTime.Reset(); }
    private void StartTimer() { _gameTime.Reset(); _gameTime.Start(); }
    #endregion

    #region Design Pattern 
    public void OnNotify(ObserverEnum observerEnum)
    {
        if (observerEnum.Equals(ObserverEnum.BOAT_EXPLODE))
        {
            StopTimer();
        }
    }
    #endregion
}