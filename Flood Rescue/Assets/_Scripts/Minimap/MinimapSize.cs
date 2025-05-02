using UnityEngine;

public enum MinimapState
{
    GROW,
    IDLE,
}

public class MinimapSize : MonoBehaviour
{
    public MinimapState minimapState = MinimapState.IDLE;
    public float minimapIdleSize;
    public float minimapGrowSize;
    [SerializeField] private Camera minimapCamera;
    private float lerpSpeed = 1f, lerpElapsed = 0f;

    private void Start()
    {
        minimapIdleSize = GameManager.Instance.gameData.minimapIdleSize;
        minimapGrowSize = GameManager.Instance.gameData.minimapGrowSize;
    }
    private void LateUpdate()
    {
        if (minimapState == MinimapState.GROW && minimapCamera.orthographicSize < minimapGrowSize)
        {
            lerpElapsed += Time.deltaTime;
            minimapCamera.orthographicSize = Mathf.Lerp(minimapIdleSize, minimapGrowSize, lerpElapsed / lerpSpeed);

            if (lerpElapsed >= lerpSpeed) Reset();
        }
        else if (minimapState == MinimapState.IDLE && minimapCamera.orthographicSize > minimapIdleSize)
        {
            lerpElapsed += Time.deltaTime;
            minimapCamera.orthographicSize = Mathf.Lerp(minimapGrowSize, minimapIdleSize, lerpElapsed / lerpSpeed);

            if (lerpElapsed >= lerpSpeed) Reset();
        }
    }

    public void ChangeMinimapState()
    {
        if (minimapState == MinimapState.IDLE)
        {
            minimapState = MinimapState.GROW;
            Subject.Instance.NotifyAllObserver(ObserverEnum.MINIMAP_EXPAND);
        }
        else
        {
            minimapState = MinimapState.IDLE;
            Subject.Instance.NotifyAllObserver(ObserverEnum.MINIMAP_SHRINK);
        }

        lerpElapsed = 0f;
    }

    private void Reset()
    {
        lerpElapsed = 0f;
    }
}
