using UnityEngine;
using UnityEngine.SceneManagement;

public class OnNextRound : MonoBehaviour
{
    public void OnNextRoundCalled()
    {
        Objective.Instance.ProgressToNextStage();
        SceneManager.LoadScene("Game Scene");
    }
}