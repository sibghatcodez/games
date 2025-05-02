using UnityEngine;
using UnityEngine.SceneManagement;

public class OnMenuScene : MonoBehaviour
{
    public void OnMenuSceneCalled() => SceneManager.LoadScene("Menu Scene");
}
