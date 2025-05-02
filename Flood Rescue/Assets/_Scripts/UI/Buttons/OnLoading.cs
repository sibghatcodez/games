using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnLoading : MonoBehaviour
{
    private float loadingProgress = 0;
    private float loadingTimer = 3f;
    private bool IsLoadingEnabled = false;
    private float timeSpentLoading = 0f;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private Canvas loadingCanvas, menuCanvas;
    private AsyncOperation asyncLoad;

    // Cache the string value to avoid frequent allocations
    private string loadingPercentageText;

    IEnumerator LoadAsyncScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync("Game Scene", LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            timeSpentLoading += Time.deltaTime;
            loadingProgress = Mathf.Clamp01(timeSpentLoading / loadingTimer);

            loadingSlider.value = Mathf.Lerp(0, 1, loadingProgress);

            loadingPercentageText = Mathf.Floor(loadingSlider.value * 100) + "%";
            loadingText.text = loadingPercentageText;

            if (timeSpentLoading >= loadingTimer && asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        Debug.Log("Scene loading complete.");
        IsLoadingEnabled = false;
        EndLoading();
    }
    public void StartLoading()
    {
        if (!IsLoadingEnabled)
        {
            loadingProgress = 0;
            timeSpentLoading = 0;
            IsLoadingEnabled = true;

            loadingCanvas.enabled = true;
            menuCanvas.enabled = false;

            StartCoroutine(LoadAsyncScene());
        }
    }
    public void EndLoading()
    {
        if (!IsLoadingEnabled)
        {
            loadingCanvas.enabled = false;
            menuCanvas.enabled = true;
        }
    }
}