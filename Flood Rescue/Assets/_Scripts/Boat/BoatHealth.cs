using TMPro;
using UnityEngine;

public class BoatHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI health;

    private float targetHealth = 100f;
    private float currentHealth = 0f;
    private float lerpSpeed = 2f;
    private bool IsGameOver = false;
    private void FixedUpdate()
    {
        if (!Mathf.Approximately(currentHealth, targetHealth))
        {
            currentHealth = Mathf.Lerp(currentHealth, targetHealth, Time.deltaTime * lerpSpeed);

            health.text = Mathf.RoundToInt(currentHealth).ToString();
        }
    }

    public void TakeDamage(float damage)
    {
        targetHealth = Mathf.Max(0, targetHealth - damage);
        if (targetHealth <= 0 && !IsGameOver && !UIManager.Instance.currentUIState.Equals(UIState.GAMEWON))
        {
            Subject.Instance.NotifyAllObserver(ObserverEnum.BOAT_EXPLODE);
            IsGameOver = true;
        }
    }
    public void Heal(float heal)
    {
        targetHealth = Mathf.Min(200, targetHealth + heal);
    }
}