using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Rescue : MonoBehaviour
{
    public static NPC_Rescue Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [SerializeField] private Image rescueBarFill;
    [SerializeField] private InventoryData inventoryData;
    private float rescueTime = 2f; // Time to complete the rescue (3 seconds)
    public bool IsRescuing = false;
    public Transform NPC; // Which NPC is getting rescued
    private float progress = 0;

    private void FixedUpdate()
    {
        if (IsRescuing)
        {
            progress += Time.deltaTime;

            // Calculate the fill amount directly as the ratio of progress to rescue time.
            rescueBarFill.fillAmount = Mathf.Clamp01(progress / rescueTime);

            // Show the rescue bar if not already active
            if (!rescueBarFill.gameObject.activeInHierarchy)
                rescueBarFill.transform.parent.gameObject.SetActive(true);

            if (rescueBarFill.fillAmount > 0.99f)
            {
                CompleteRescue();
            }
        }
        else if (rescueBarFill.gameObject.activeInHierarchy)
        {
            rescueBarFill.transform.parent.gameObject.SetActive(false);
        }
    }

    private void CompleteRescue()
    {
        // AddNPCToInventory(NPC.gameObject);
        CheckScore();
        GameManager.Instance.gameData.TotalTimesRescued++;
        Objective.Instance.peopleRescued++;
        Objective.Instance.SetObjectiveText();
        progress = 0;
        NPC.GetComponent<NPC_GrowShrink>().ShrinkAndDeactivate();
        NPC_SitPositions.Instance.SitManager(NPC.gameObject); //Make the player sit on the boat.
        IsRescuing = false;
        NPC = null;
    }
    private void CheckScore()
    {
        bool doesPlayerDeservesQuickRescuePoints = NPC.GetComponent<NPC_Timer>().timerImg.fillAmount <= 0.25;
        if (doesPlayerDeservesQuickRescuePoints)
        {
            Scoring.Instance.IncreaseScore(ScoreSystem.QUICK_RESCUE_POINTS);
        }
        else Scoring.Instance.IncreaseScore(ScoreSystem.RESCUE_POINTS);
    }
}