using UnityEngine;
using UnityEngine.UI;

public class NPC_Timer : MonoBehaviour
{
    [SerializeField] public Image timerImg;
    private int timerDuration = 0;
    private float timerProgress = 0;
    private bool HasTimerStarted = false;
    public void StartTimer(int seconds)
    {
        timerDuration = seconds;
        HasTimerStarted = true;
    }

    [SerializeField] private Color twenty_five, fifty, seventy_five, hundred;
    private void FixedUpdate()
    {
        if (HasTimerStarted)
        {
            timerProgress += Time.deltaTime;
            timerImg.fillAmount = Mathf.Clamp01(Mathf.Lerp(1, 0, timerProgress / timerDuration));
            if (timerImg.fillAmount <= 0.25f) timerImg.color = twenty_five;
            else if (timerImg.fillAmount <= 0.50f) timerImg.color = fifty;
            else if (timerImg.fillAmount <= 0.75f) timerImg.color = seventy_five;
            else if (timerImg.fillAmount <= 1f) timerImg.color = hundred;
        }
        if (GameManager.Instance.GameTime > 3 && timerImg.fillAmount < 0.01)
        {
            GetComponent<NPC_GrowShrink>().ShrinkAndDeactivate();
        }
    }
}