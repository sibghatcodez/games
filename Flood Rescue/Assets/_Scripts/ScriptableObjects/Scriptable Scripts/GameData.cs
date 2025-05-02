using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Game Data", menuName = "Game Data")]
public class GameData : ScriptableObject
{
    // Important Values To Be Used All Over
    [Range(1, 15)][SerializeField] private float loadScreenTimer = 2;

    // Boat Movement
    [Header("Boat Movement")]
    [Range(1, 15)][SerializeField] private float moveSpeed = 5f;
    [Range(1, 25)][SerializeField] private float turnSpeed = 10f;
    [Range(1, 50)][SerializeField] private float maxBoatSpeed = 25f;
    [Range(-10, 10)][SerializeField] private float turnBreakPower = 0.025f;
    [SerializeField] private int dropTimerSeconds = 60;

    // Miscellaneous
    [Header("Miscellaneous")]
    [Range(1, 500)][SerializeField] private int pickupDistance = 20;
    [Range(0.1f, 1)][SerializeField] private float rescueTime = 1;
    [Range(1f, 500)][SerializeField] public float minimapGrowSize = 175;
    [Range(1f, 500)][SerializeField] public float minimapIdleSize = 100;

    // Stats
    [Header("Stats")]
    [SerializeField] private int totalTimesRescued = 0;
    [SerializeField] private int totalTimesGamePlayed = 0;
    [SerializeField] private int totalTimesTutorialPlayed = 0;
    [SerializeField] private int totalTimesBoatExploded = 0;
    [SerializeField] private int totalTimesWon = 0;
    [SerializeField] private int totalTimesLost = 0;
    [SerializeField] private int maximumCoins = 0;

    [Header("Camera")]
    [SerializeField] private Vector3 tpp = new Vector3(-7, 2.25f, 0);
    [SerializeField] private Vector3 fpp = new Vector3(-2f, 1f, 0f);
    [Range(0, 100)][SerializeField] private int rotationLagSpeed = 10;
    [Range(0, 100)][SerializeField] private int positionLagSpeed = 8;

    // Public Getters (Optional)
    public float LoadScreenTimer => loadScreenTimer;
    public float MoveSpeed => moveSpeed;
    public float TurnSpeed => turnSpeed;
    public float MaxBoatSpeed => maxBoatSpeed;
    public float TurnBreakPower => turnBreakPower;
    public int DropTimerSeconds => dropTimerSeconds;
    public int PickupDistance => pickupDistance;
    public float RescueTime => rescueTime;
    public Vector3 TPP => tpp;
    public Vector3 FPP => fpp;
    public int RotationLagSpeed => rotationLagSpeed;
    public int PositionLagSpeed => positionLagSpeed;

    // Stats That Can Be SET
    public int TotalTimesRescued
    {
        get => totalTimesRescued;
        set => totalTimesRescued = Mathf.Max(0, value); // Prevents negative values
    }

    public int TotalTimesGamePlayed
    {
        get => totalTimesGamePlayed;
        set => totalTimesGamePlayed = Mathf.Max(0, value); // Prevents negative values
    }

    public int TotalTimesTutorialPlayed
    {
        get => totalTimesTutorialPlayed;
        set => totalTimesTutorialPlayed = Mathf.Max(0, value); // Prevents negative values
    }

    public int TotalTimesBoatExploded
    {
        get => totalTimesBoatExploded;
        set => totalTimesBoatExploded = Mathf.Max(0, value); // Prevents negative values
    }

    public int TotalTimesWon
    {
        get => totalTimesWon;
        set => totalTimesWon = Mathf.Max(0, value); // Prevents negative values
    }

    public int TotalTimesLost
    {
        get => totalTimesLost;
        set => totalTimesLost = Mathf.Max(0, value); // Prevents negative values
    }

    public int MaximumCoins
    {
        get => maximumCoins;
        set => maximumCoins = Mathf.Max(0, value); // Prevents negative values
    }
}
