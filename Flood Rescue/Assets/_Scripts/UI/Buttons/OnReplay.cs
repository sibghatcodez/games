using UnityEngine;
using UnityEngine.SceneManagement;

public class OnReplay : MonoBehaviour
{
    public void OnReplayCalled()
    {
        SceneManager.LoadScene("Game Scene");
    }
}