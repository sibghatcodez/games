using UnityEngine;

public interface ISubject
{
    void AddObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyAllObserver(ObserverEnum observerEnum);
}