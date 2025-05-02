using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour, ISubject
{
    public static Subject Instance { get; private set; }
    private readonly List<IObserver> _observers = new List<IObserver>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void AddObserver(IObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }
    public void NotifyAllObserver(ObserverEnum observerEnum)
    {
        foreach (var observer in _observers)
        {
            observer?.OnNotify(observerEnum);
        }
    }
}