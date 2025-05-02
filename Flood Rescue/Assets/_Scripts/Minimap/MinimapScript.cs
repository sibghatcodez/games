using UnityEngine;

public class MinimapScript : MonoBehaviour, IObserver
{
    public Transform boat;
    public Transform[] enemies;
    public Camera minimapCamera;
    private UnityEngine.Vector3 newPos = UnityEngine.Vector3.zero;
    public float minimapCamSize;

    private void Start()
    {
        minimapCamSize = GameManager.Instance.gameData.minimapIdleSize;
    }
    void LateUpdate()
    {
        newPos = boat.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        foreach (var enemy in enemies)
        {
            if (enemy.transform.gameObject.activeInHierarchy)
            {
                UpdateEnemyIconPosition(enemy, minimapCamSize);
            }
        }
    }

    private void UpdateEnemyIconPosition(Transform enemy, float minimapRadius)
    {
        Vector3 direction = enemy.transform.parent.transform.parent.position - boat.position;

        if (direction.magnitude > minimapRadius)
        {
            Vector3 clampedDirection = direction.normalized * (minimapRadius - 10);
            enemy.position = boat.position + clampedDirection;
        }
        else
        {
            enemy.localPosition = Vector3.zero;
        }
    }



    #region Design Pattern
    private void OnEnable()
    {
        Invoke("AddObserver", 1f);
    }
    private void AddObserver() => Subject.Instance.AddObserver(this);
    public void OnNotify(ObserverEnum observerEnum)
    {
        if (observerEnum.Equals(ObserverEnum.MINIMAP_EXPAND))
        {
            minimapCamSize = GameManager.Instance.gameData.minimapGrowSize;
        }
        if (observerEnum.Equals(ObserverEnum.MINIMAP_SHRINK))
        {
            minimapCamSize = GameManager.Instance.gameData.minimapIdleSize;
        }
    }
    private void OnDisable()
    {
        Subject.Instance.RemoveObserver(this);
    }
    #endregion
}