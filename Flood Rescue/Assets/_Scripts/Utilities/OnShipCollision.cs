using UnityEngine;

public class OnShipCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Boat"))
        {
            CheckObjective();
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Boat"))
        {
            CheckObjective();
        }
    }

    private void CheckObjective()
    {
        if (Objective.Instance.peopleRescued >= Objective.Instance.peopleToRescue)
        {
            UIManager.Instance.changeUIState(UIState.GAMEWON);
        }
    }
}