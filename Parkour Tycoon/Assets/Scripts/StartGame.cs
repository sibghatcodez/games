using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] byte secondsToWait = 3;
    [SerializeField] TextMeshPro records;
    TextMeshPro textLabel;
    bool isStartButtonClicked = false;

    void Start()
    {
        textLabel = GetComponent<TextMeshPro>();
        UpdateRecords();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isStartButtonClicked)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) && hit.transform == transform)
            {
                isStartButtonClicked = true;
                onCountDownStart();
            }
        }
    }

    void UpdateRecords()
    {
        float score = PlayerPrefs.GetFloat("Score", 0);
        records.text = score == 0 ? "RECORDS\n\n - No records to show" : $"RECORDS\n\n - Total Score: {score}";
    }

    void onCountDownStart()
    {
        if (secondsToWait > 0)
        {
            textLabel.text = $"Teleporting in {secondsToWait--}s...";
            Invoke(nameof(onCountDownStart), 1f);
        }
        else
        {
            textLabel.text = "Enjoy! >:D";
            SceneManager.LoadScene("Storymode");
        }
    }
}