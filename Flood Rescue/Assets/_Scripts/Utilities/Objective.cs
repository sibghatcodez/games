
using System.Collections.Generic;
using System.Linq;
using System.Text; // Required for StringBuilder
using TMPro;
using UnityEngine;

public class Objective : MonoBehaviour, IObserver
{

    #region Singleton
    public static Objective Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion


    #region variables
    public int stage;
    public int peopleToRescue = 2;
    public int peopleRescued = 0;
    public int givenRescueTime = 0;
    public int givenBoatDistance = 30;

    // UI references
    [SerializeField] private TextMeshProUGUI objectiveText, stageText;
    [SerializeField] private GameObject challenge;

    #endregion

    #region unity
    void Start()
    {
        stage = PlayerPrefs.GetInt("stage", 1);
        SetObjectiveByStage();
    }
    #endregion

    #region functionality

    private void LateUpdate()
    {
        if (peopleToRescue <= peopleRescued)
        {
            if (!challenge.activeInHierarchy) challenge.SetActive(true);
            SetObjectiveText();
        }
        if (challenge.activeInHierarchy) SetObjectiveTextToEmpty();
    }
    private void SetObjectiveTextToEmpty()
    {
        if (!objectiveText.text.Equals(string.Empty)) objectiveText.text = string.Empty;
    }
    public void ProgressToNextStage()
    {
        peopleRescued = 0;
        peopleToRescue = 0;

        stage += 1;
        PlayerPrefs.SetInt("stage", stage);
        PlayerPrefs.Save();
        SetObjectiveByStage();
    }
    public void SetObjectiveText()
    {
        objectiveText.text = $"Rescued \n{peopleRescued} / {peopleToRescue}";
        stageText.text = "Stage: " + stage;
    }

    public void SetObjectiveByStage()
    {
        int basePeopleToRescue = 1;
        float baseBoatDistance = 50f;
        float baseRescueTime = 35f;

        if (stage <= 3)
        {
            // Increase difficulty gradually
            peopleToRescue = Mathf.CeilToInt(basePeopleToRescue * Mathf.Pow(1.05f, stage));
            peopleToRescue = Mathf.Min(peopleToRescue, 7); // Cap to 7

            givenBoatDistance = (int)(baseBoatDistance * Mathf.Pow(1.03f, stage));
        }
        else
        {
            peopleToRescue = Mathf.CeilToInt(basePeopleToRescue * Mathf.Pow(1.12f, stage));
            peopleToRescue = Mathf.Min(peopleToRescue, 7);

            givenBoatDistance = (int)(baseBoatDistance * Mathf.Pow(1.07f, stage - 10));
        }

        givenBoatDistance = Mathf.Min(givenBoatDistance, 200);

        if (stage <= 3)
        {
            givenRescueTime = (int)(baseRescueTime - (stage * 0.5f));
        }
        else
        {
            givenRescueTime = (int)(baseRescueTime - (5 * 0.5f) - ((stage - 10) * 0.7f));
        }
        // Ensure rescue time doesn't go below 10
        givenRescueTime = (int)Mathf.Max(givenRescueTime, 10f);

        // Update the UI text
        SetObjectiveText();
    }
    #endregion



    #region Design Pattern
    private void OnEnable()
    {
        Invoke("AddObserver", 1f);
    }
    void AddObserver() => Subject.Instance.AddObserver(this);
    public void OnNotify(ObserverEnum observerEnum)
    {
    }
    private void OnDisable()
    {
        Subject.Instance.RemoveObserver(this);
    }
    #endregion
}