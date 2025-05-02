using System.Collections;
using TMPro;
using UnityEngine;

public class NPC_DropTimer : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI timer;
    private float dropTimerDuration = 60f; // 60 seconds to drop NPC to the ship
    private Coroutine coroutine;

    void OnEnable()
    {
        Invoke("AddObserver", 1f);

        if (coroutine == null)
            coroutine = StartCoroutine(DropTimer());
    }

    private IEnumerator DropTimer()
    {
        while (dropTimerDuration > 0)
        {
            dropTimerDuration -= 1;
            timer.text = FormatTime(dropTimerDuration);
            yield return new WaitForSeconds(1);
        }

        timer.text = "00:00";
        coroutine = null;

        if (dropTimerDuration == 0) UIManager.Instance.changeUIState(UIState.GAMELOST);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }

    void OnDisable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        Subject.Instance.RemoveObserver(this);
    }
    private void AddObserver() => Subject.Instance.AddObserver(this);

    public void OnNotify(ObserverEnum observerEnum)
    {
        if (observerEnum.Equals(ObserverEnum.GAME_OVER))
        {
            if (coroutine != null) StopCoroutine(coroutine);
        }
    }
}